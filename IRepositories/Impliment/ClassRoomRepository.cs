using EasyMN.Shared.Entities;
using Repositories.IRepositories;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
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

        // Thêm mới
        public async Task<int> AddAsync(ClassRoom classRoom)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                var id = (int)await _session.SaveAsync(classRoom);
                await transaction.CommitAsync();
                return id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(ClassRoom classRoom)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                await _session.UpdateAsync(classRoom);
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Xóa
        public async Task<bool> DeleteAsync(ClassRoom classRoom)
        {
            using var transaction = _session.BeginTransaction();
            try
            {
                await _session.DeleteAsync(classRoom);
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Lấy tất cả
        public async Task<IEnumerable<ClassRoom>> GetAllAsyncs()
        {
            var result = _session.Query<ClassRoom>();
            return await Task.FromResult(result.ToList());
        }

        // Lấy tất cả có phân trang
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

        // Lấy theo Id
        public Task<ClassRoom?> GetByIdAsync(int id)
        {
            return _session.GetAsync<ClassRoom>(id);
        }

        // Lấy theo Code
        public async Task<ClassRoom?> GetByCodeAsync(string code)
        {
            return await Task.FromResult(_session.Query<ClassRoom>()
                                            .FirstOrDefault(c => c.ClassCode == code));
        }

        public Task<int> CountAllAsync()
        {
            return Task.FromResult(_session.Query<ClassRoom>().Count());
        }
    }
}
