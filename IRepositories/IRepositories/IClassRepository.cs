using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyMN.Shared.Entities;
using EasyMN.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories

{
    public interface IClassRepository
    {
        Task<PagedResult<ClassRoom>> GetAllAsyncs(int pageNumber = 1, int pageSize = 10);
        Task<ClassRoom?> GetByIdAsync(int id);
        Task<ClassRoom?> GetByCodeAsync(string code);
        Task<int> AddAsync(ClassRoom classRoom);
        Task<bool> UpdateAsync(ClassRoom classRoom);
        Task<bool> DeleteAsync(ClassRoom classRoom);
    }
}
