using EasyMN.Shared.Entities;
using Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyMN.Shared.Models;

namespace Repositories.Impliment

{
    public class ClassRoomRepository : IClassRepository
    {
        private readonly ISession _session;
        public ClassRoomRepository(ISession session)
        {
            _session = session;
        }
        public Task<int> AddAsync(ClassRoom classRoom)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ClassRoom classRoom)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClassRoom>> GetAllAsyncs()
        {
           return await _session.Query<ClassRoom>().ToListAsync();
        }

        public Task<PagedResult<ClassRoom>> GetAllAsyncs(int pageNumber = 1, int pageSize = 10)
        {
            var query = _session.Query<ClassRoom>();


            var totalRecords = query.Count();
            var classRooms = query.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var pagedResult = new PagedResult<ClassRoom>
            {
                Items = classRooms,
                TotalItems = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Task.FromResult(pagedResult);
        }

        public Task<ClassRoom?> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<ClassRoom?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ClassRoom classRoom)
        {
            throw new NotImplementedException();
        }
    }
}
