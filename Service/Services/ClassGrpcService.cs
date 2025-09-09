using Repositories.IRepositories;
using EasyMN.Shared.Dtos;
using EasyMN.Shared.Dtos.ClassRoom;
using EasyMN.Shared.Entities;
using EasyMN.Shared.IServices;
using EasyMN.Shared.Models;

namespace Service.Services
{
    public class ClassGrpcService : IClassGrpcService
    {
        private readonly IClassRepository _classRepository;

        public ClassGrpcService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<ResponseWrapper<PagedResult<ClassRoomDto>>> GetAllClassRoomsAsync(PagedRequest request)
        {
            var result = await _classRepository.GetAllAsyncs(request.PageNumber, request.PageSize);
            return new ResponseWrapper<PagedResult<ClassRoomDto>>("Success", new PagedResult<ClassRoomDto>
            {
                Items = result.Items.Select(c => new ClassRoomDto
                {
                    Id = c.Id,
                    ClassCode = c.ClassCode,
                    ClassName = c.ClassName,
                    Subject = c.Subject,
                    TeacherId = c.TeacherId,
                    TeacherName = c.Teacher?.Name ?? string.Empty
                }).ToList(),
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            });
        }

        public async Task<ResponseWrapper<ClassRoomDto?>> GetClassRoomByIdAsync(ClassRoomRequest request)
        {
            var classroom = await _classRepository.GetByIdAsync(request.Id);
            if (classroom == null)
                return new ResponseWrapper<ClassRoomDto?>("Not found", null);

            return new ResponseWrapper<ClassRoomDto?>("Success", MapToDto(classroom));
        }

        public async Task<ResponseWrapper<ClassRoomDto?>> GetClassRoomByCodeAsync(ClassCodeRequest request)
        {
            var classroom = await _classRepository.GetByCodeAsync(request.ClassCode);
            if (classroom == null)
                return new ResponseWrapper<ClassRoomDto?>("Not found", null);

            return new ResponseWrapper<ClassRoomDto?>("Success", MapToDto(classroom));
        }

        public async Task<ResponseWrapper<int>> AddClassRoomAsync(CreateClassRoomRequest request)
        {
            var classroom = new ClassRoom
            {
                ClassCode = request.ClassCode,
                ClassName = request.ClassName,
                Subject = request.Subject,
                TeacherId = request.TeacherId
            };

            var id = await _classRepository.AddAsync(classroom);
            return new ResponseWrapper<int>("Created", id);
        }

        public async Task<ResponseWrapper<bool>> UpdateClassRoomAsync(UpdateClassRoomRequest request)
        {
            var classroom = await _classRepository.GetByIdAsync(request.Id);
            if (classroom == null)
                return new ResponseWrapper<bool>("Not found", false);

            classroom.ClassName = request.ClassName;
            classroom.Subject = request.Subject;
            classroom.TeacherId = request.TeacherId;

            var success = await _classRepository.UpdateAsync(classroom);
            return new ResponseWrapper<bool>(success ? "Updated" : "Update failed", success);
        }

        // public async Task<ResponseWrapper<bool>> DeleteClassRoomAsync(ClassRoomRequest request)
        // {
        //     var success = await _classRepository.DeleteAsync();
        //     return new ResponseWrapper<bool>(success ? "Deleted" : "Delete failed", success);
        // }

        private ClassRoomDto MapToDto(ClassRoom c)
        {
            return new ClassRoomDto
            {
                Id = c.Id,
                ClassCode = c.ClassCode,
                ClassName = c.ClassName,
                Subject = c.Subject,
                TeacherId = c.TeacherId,
                TeacherName = c.Teacher?.Name ?? string.Empty
            };
        }
    }
}
