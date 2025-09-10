using EasyMN.Shared.Dtos;
using EasyMN.Shared.Dtos.Report;
using System.ServiceModel;

namespace EasyMN.Shared.IServices
{
    [ServiceContract]
    public interface IReportGrpcService
    {
        [OperationContract]
        Task<ResponseWrapper<DashboardStatsDto>> GetDashboardStatsAsync();

        [OperationContract]
        Task<ResponseWrapper<ClassStatsDto>> GetClassStatsAsync(ClassStatsRequest classStatsRequest);
        [OperationContract]
        Task<ResponseWrapper<List<ClassStatsDto>>> GetAllClassStatsAsync();

    }
}
