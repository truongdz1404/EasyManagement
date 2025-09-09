using EasyMN.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.IRepositories;

namespace Repositories.Impliment

{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ISession _session;
        public TeacherRepository(ISession session)
        {
            _session = session;
        }
        public Task<int> AddAsync(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsyncs()
        {
           return await _session.Query<Teacher>().ToListAsync();
        }

        public Task<Teacher?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
