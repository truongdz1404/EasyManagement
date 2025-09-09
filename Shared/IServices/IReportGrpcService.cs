using EasyMN.Shared.Dtos;
using Shared.Dtos;
using Shared.Dtos.Report;
using System.ServiceModel;

namespace Shared.IServices
{
    [ServiceContract]
    public interface IReportGrpcService
    {
        [OperationContract]
        Task<ResponseWrapper<DashboardStatsDto>> GetDashboardStatsAsync();

        [OperationContract]
        Task<ResponseWrapper<ClassStatsDto>> GetClassStatsAsync(int classId);
    }
}
