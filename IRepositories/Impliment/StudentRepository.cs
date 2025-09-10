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
using EasyMN.Shared.Dtos;
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

        public Task<PagedResult<Student>> GetAllAsync(PagedRequest request)
        {
            var query = _session.Query<Student>();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(s => s.Name.Contains(request.Keyword) || s.StudentCode.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.SortField))
            {
                Console.WriteLine($"Repository - Sorting by: {request.SortField}, Ascending: {request.SortAscending}");
                switch (request.SortField)
                {
                    case "StudentCode":
                        query = request.SortAscending == true ?
                            query.OrderBy(s => s.StudentCode) :
                            query.OrderByDescending(s => s.StudentCode);
                        break;
                    case "Name":
                        query = request.SortAscending == true ?
                            query.OrderBy(s => s.Name) :
                            query.OrderByDescending(s => s.Name);
                        break;
                    default:
                        Console.WriteLine($"Unhandled sort field: {request.SortField}");
                        break;
                }
            }

            var totalRecords = query.Count();
            var students = query.Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)

                                .ToList();

            var pagedResult = new PagedResult<Student>
            {
                Items = students,
                TotalItems = totalRecords,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
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

        public Task<int> CountAllAsync()
        {
            return Task.FromResult(_session.Query<Student>().Count());
        }

        public Task<int> CountByClassAsync(int classId)
        {
            return Task.FromResult(_session.Query<Student>()
                                         .Count(s => s.ClassRoom.Id == classId));
        }
    }

}
