using EasyMN.Shared.Dtos;
using EasyMN.Shared.Entities;
using EasyMN.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories

{
    public interface IStudentRepository
    {
        Task<PagedResult<Student>> GetAllAsync(PagedRequest request);
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByCodeAsync(string code);
        Task<int> AddAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Student student);
        Task<bool> ExistsAsync(string studentCode);
        Task<int> CountAllAsync();
        Task<int> CountByClassAsync(int classId);
    }
}
