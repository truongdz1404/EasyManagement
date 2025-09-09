using EasyMN.Shared.Dtos.Student;
using EasyMN.Shared.Entities;
using EasyMN.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Configuration;
using System.ServiceModel;
using EasyMN.Shared.Dtos;
namespace EasyMN.Shared.IServices
{
        [ServiceContract]
        public interface IStudentGrpcService
        {
            [OperationContract]
            Task<ResponseWrapper<PagedResult<StudentDto>>> GetAllStudentsAsync(PagedRequest request);

            [OperationContract]
            Task<ResponseWrapper<StudentDto?>> GetStudentByIdAsync(StudentRequest request);

            [OperationContract]
            Task<ResponseWrapper<StudentDto?>> GetStudentByCodeAsync(StudentCodeRequest request);

            [OperationContract]
            Task<ResponseWrapper<int>> AddStudentAsync(CreateStudentRequest request);

            [OperationContract]
            Task<ResponseWrapper<bool>> UpdateStudentAsync(UpdateStudentRequest request);

            [OperationContract]
            Task<ResponseWrapper<bool>> DeleteStudentAsync(StudentRequest request);
        }
    }
