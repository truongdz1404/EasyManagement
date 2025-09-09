using Repositories.IRepositories;
using EasyMN.Shared.Dtos;
using EasyMN.Shared.Dtos.Student;
using EasyMN.Shared.Entities;
using EasyMN.Shared.IServices;
using EasyMN.Shared.Models;

namespace Service.Services
{
    public class StudentGrpcService : IStudentGrpcService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentGrpcService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResponseWrapper<PagedResult<StudentDto>>> GetAllStudentsAsync(PagedRequest request)
        {
            var result = await _studentRepository.GetAllAsync(request.PageNumber, request.PageSize, request.Keyword, request.SortByName);
            return new ResponseWrapper<PagedResult<StudentDto>>("Success", new PagedResult<StudentDto>
            {
                Items = result.Items.Select(s => new StudentDto
                {
                    Id = s.Id,
                    StudentCode = s.StudentCode,
                    Name = s.Name,
                    Dob = s.Dob,
                    ClassRoomId = s.ClassRoomId,
                    Address = s.Address,
                    ClassRoomName = s.ClassRoom?.ClassName ?? string.Empty
                }).ToList(),
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            });
        }

        public async Task<ResponseWrapper<StudentDto?>> GetStudentByIdAsync(StudentRequest request)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id);
            if (student == null)
                return new ResponseWrapper<StudentDto?>("Not found", null);

            return new ResponseWrapper<StudentDto?>("Success", MapToDto(student));
        }

        public async Task<ResponseWrapper<StudentDto?>> GetStudentByCodeAsync(StudentCodeRequest request)
        {
            var student = await _studentRepository.GetByCodeAsync(request.StudentCode);
            if (student == null)
                return new ResponseWrapper<StudentDto?>("Not found", null);

            return new ResponseWrapper<StudentDto?>("Success", MapToDto(student));
        }

        public async Task<ResponseWrapper<int>> AddStudentAsync(CreateStudentRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.StudentCode))
                    return new ResponseWrapper<int>("Student code is required", 0);

                if (string.IsNullOrWhiteSpace(request.Name))
                    return new ResponseWrapper<int>("Student name is required", 0);

                // Check if student code already exists
                var existingStudent = await _studentRepository.GetByCodeAsync(request.StudentCode);
                if (existingStudent != null)
                    return new ResponseWrapper<int>("Student code already exists", 0);

                var student = new Student
                {
                    StudentCode = request.StudentCode,
                    Name = request.Name,
                    Dob = request.Dob,
                    Address = request.Address,
                    ClassRoomId = request.ClassRoomId
                };

                var id = await _studentRepository.AddAsync(student);
                return new ResponseWrapper<int>("Created successfully", id);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<int>($"Error: {ex.Message}", 0);
            }
        }

        public async Task<ResponseWrapper<bool>> UpdateStudentAsync(UpdateStudentRequest request)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(request.Id);
                if (student == null)
                    return new ResponseWrapper<bool>("Student not found", false);

                // Validate input
                if (string.IsNullOrWhiteSpace(request.Name))
                    return new ResponseWrapper<bool>("Student name is required", false);

                student.Name = request.Name;
                student.Dob = request.Dob;
                student.Address = request.Address;
                student.ClassRoomId = request.ClassRoomId;

                var success = await _studentRepository.UpdateAsync(student);
                return new ResponseWrapper<bool>(success ? "Updated successfully" : "Update failed", success);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool>($"Error: {ex.Message}", false);
            }
        }


        public async Task<ResponseWrapper<bool>> DeleteStudentAsync(StudentRequest request)
        {
            try
            {
                var success = await _studentRepository.DeleteAsync(request.Id);
                return new ResponseWrapper<bool>(success ? "Deleted successfully" : "Delete failed", success);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool>($"Error: {ex.Message}", false);
            }
        }

        private StudentDto MapToDto(Student s)
        {
            return new StudentDto
            {
                Id = s.Id,
                StudentCode = s.StudentCode,
                Name = s.Name,
                Dob = s.Dob,
                Address = s.Address,
                ClassRoomId = s.ClassRoomId,
                ClassRoomName = s.ClassRoom?.ClassName ?? string.Empty
            };
        }
    }
}
