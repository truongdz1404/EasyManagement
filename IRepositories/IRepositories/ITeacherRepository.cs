using EasyMN.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories

{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAllAsyncs();
        Task<Teacher?> GetByIdAsync(int id);
        Task<Teacher?> GetByCodeAsync(string code);
        Task<int> AddAsync(Teacher teacher);
        Task<bool> UpdateAsync(Teacher teacher);
        Task<bool> DeleteAsync(Teacher teacher);
    }
}
