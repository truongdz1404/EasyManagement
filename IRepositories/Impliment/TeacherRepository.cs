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
            var tx = _session.BeginTransaction();
            var id = (int)_session.Save(teacher);
            tx.Commit();
            return Task.FromResult(id);
        }

        public Task<bool> DeleteAsync(Teacher teacher)
        {
            var tx = _session.BeginTransaction();
            _session.Delete(teacher);
            tx.Commit();
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<Teacher>> GetAllAsyncs()
        {
            return await _session.Query<Teacher>().ToListAsync();
        }

        public Task<Teacher?> GetByCodeAsync(string code)
        {
            var teacher = _session.Query<Teacher>()
                                  .FirstOrDefault(t => t.TeacherCode == code);
            return Task.FromResult(teacher);
        }

        public Task<Teacher?> GetByIdAsync(int id)
        {
            var teacher = _session.Get<Teacher>(id);
            return Task.FromResult(teacher);
        }

        public Task<bool> UpdateAsync(Teacher teacher)
        {
            var tx = _session.BeginTransaction();
            _session.Update(teacher);
            tx.Commit();
            return Task.FromResult(true);
        }
    }
}
