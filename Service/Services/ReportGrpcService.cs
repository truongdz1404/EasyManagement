using EasyMN.Shared.Dtos;
using Microsoft.Extensions.Logging;
using Repositories.IRepositories;
using Shared.Dtos;
using Shared.Dtos.Report;
using Shared.IServices;
using System.Linq;

namespace Service.Services
{
    public class ReportGrpcService : IReportGrpcService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<ReportGrpcService> _logger;

        public ReportGrpcService(
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ITeacherRepository teacherRepository,
            ILogger<ReportGrpcService> logger)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public async Task<ResponseWrapper<DashboardStatsDto>> GetDashboardStatsAsync()
        {
            try
            {
                var totalStudents = await _studentRepository.CountAllAsync();

                var totalClasses = await _classRepository.CountAllAsync();

                var totalTeachers = await _teacherRepository.CountAllAsync();

                var stats = new DashboardStatsDto
                {
                    TotalStudents = totalStudents,
                    TotalClasses = totalClasses,
                    TotalTeachers = totalTeachers
                };

                return new ResponseWrapper<DashboardStatsDto>("Success", stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard stats");
                return new ResponseWrapper<DashboardStatsDto>("Error getting dashboard stats", null);
            }
        }

        public async Task<ResponseWrapper<ClassStatsDto>> GetClassStatsAsync(int classId)
        {
            try
            {
                var classRoom = await _classRepository.GetByIdAsync(classId);
                if (classRoom == null)
                {
                    return new ResponseWrapper<ClassStatsDto>("Class not found", null);
                }

                var totalStudents = await _studentRepository.CountByClassAsync(classId);

                var stats = new ClassStatsDto
                {
                    ClassId = classId,
                    ClassName = classRoom.ClassName,
                    TotalStudents = totalStudents
                };

                return new ResponseWrapper<ClassStatsDto>("Success", stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting class stats for class {ClassId}", classId);
                return new ResponseWrapper<ClassStatsDto>($"Error getting class stats for class {classId}", null);
            }
        }
    }
}
