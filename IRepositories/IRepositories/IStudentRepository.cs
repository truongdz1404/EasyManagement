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
        Task<PagedResult<Student>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? keyword = null, bool? isSortByName = null);
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByCodeAsync(string code);
        Task<int> AddAsync(Student student);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Student student);
        Task<bool> ExistsAsync(string studentCode);
    }
}
