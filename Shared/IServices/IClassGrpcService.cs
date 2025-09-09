using EasyMN.Shared.Dtos;
using EasyMN.Shared.Dtos.ClassRoom;
using EasyMN.Shared.Models;
using System.ServiceModel;

namespace EasyMN.Shared.IServices
{
    [ServiceContract]
    public interface IClassGrpcService
    {
        [OperationContract]
        Task<ResponseWrapper<PagedResult<ClassRoomDto>>> GetAllClassRoomsAsync(PagedRequest request);

        [OperationContract]
        Task<ResponseWrapper<ClassRoomDto?>> GetClassRoomByIdAsync(ClassRoomRequest request);

        [OperationContract]
        Task<ResponseWrapper<ClassRoomDto?>> GetClassRoomByCodeAsync(ClassCodeRequest request);

        [OperationContract]
        Task<ResponseWrapper<int>> AddClassRoomAsync(CreateClassRoomRequest request);

        [OperationContract]
        Task<ResponseWrapper<bool>> UpdateClassRoomAsync(UpdateClassRoomRequest request);

        // [OperationContract]
        // Task<ResponseWrapper<bool>> DeleteClassRoomAsync(ClassRoomRequest request);
    }
}
