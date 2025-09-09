using EasyMN.Shared.Entities;
using Repositories.IRepositories;
using EasyMN.Shared.Models;
using Microsoft.EntityFrameworkCore;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories.Impliment

{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISession _session;
        public StudentRepository(ISession session)
        {
            _session = session;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var tx = _session.BeginTransaction();
            var student = _session.Get<Student>(id);
            _session.Delete(student);
            tx.Commit();
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(string studentCode)
        {
            return Task.FromResult(_session.Query<Student>()
                                                           .Any(s => s.StudentCode == studentCode));
        }

        public Task<PagedResult<Student>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? keyword = null, bool? isSortByName = null)
        {
            var query = _session.Query<Student>();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(s => s.Name.Contains(keyword));
            }
            if (isSortByName.HasValue && isSortByName.Value)
            {
                query = query.OrderBy(s => s.Name);
            }

            var totalRecords = query.Count();
            var students = query.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            var pagedResult = new PagedResult<Student>
            {
                Items = students,
                TotalItems = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Task.FromResult(pagedResult);
        }

        public Task<Student?> GetByCodeAsync(string code)
        {
            return Task.FromResult(_session.Query<Student>()
                                            .FirstOrDefault(s => s.StudentCode == code));
        }

        public Task<Student?> GetByIdAsync(int id)
        {
            return Task.FromResult(_session.Get<Student>(id));
        }


        Task<int> IStudentRepository.AddAsync(Student student)
        {
            var tx = _session.BeginTransaction();
            _session.Save(student);
            tx.Commit();
            return Task.FromResult(student.Id);
        }

        Task<bool> IStudentRepository.DeleteAsync(Student student)
        {
            var tx = _session.BeginTransaction();
            _session.Delete(student);
            tx.Commit();
            return Task.FromResult(true);
        }

        Task<bool> IStudentRepository.UpdateAsync(Student student)
        {
            var tx = _session.BeginTransaction();
            _session.Update(student);
            tx.Commit();
            return Task.FromResult(true);
        }
    }
}
