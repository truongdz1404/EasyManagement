using EasyMN.Shared.Entities;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Helpers
{
    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var session = scope.ServiceProvider.GetRequiredService<ISession>();
            using var tx = session.BeginTransaction();
            if (!session.Query<Teacher>().Any())
            {
                var teachers = new List<Teacher>
                {
                    new Teacher { TeacherCode = "T001", Name = "Nguyễn Văn A" },
                    new Teacher { TeacherCode = "T002", Name = "Trần Thị B" },
                    new Teacher { TeacherCode = "T003", Name = "Lê Văn C" },
                    new Teacher { TeacherCode = "T004", Name = "Phạm Thị D" },
                    new Teacher { TeacherCode = "T005", Name = "Hoàng Thị E" },
                };

                foreach (var teacher in teachers)
                    session.Save(teacher);
            }

            // ===== CLASSROOMS =====
            if (!session.Query<ClassRoom>().Any())
            {
                var teachers = session.Query<Teacher>().ToList();

                var classRooms = new List<ClassRoom>
                {
                    new ClassRoom { ClassCode = "C001", ClassName = "Lớp 10A1", Subject = "Toán", Teacher = teachers[0] },
                    new ClassRoom { ClassCode = "C002", ClassName = "Lớp 10A2", Subject = "Văn", Teacher = teachers[1] },
                    new ClassRoom { ClassCode = "C003", ClassName = "Lớp 11A1", Subject = "Lý", Teacher = teachers[2] },
                    new ClassRoom { ClassCode = "C004", ClassName = "Lớp 11A2", Subject = "Hóa", Teacher = teachers[3] },
                    new ClassRoom { ClassCode = "C005", ClassName = "Lớp 12A1", Subject = "Anh", Teacher = teachers[4] },
                };

                foreach (var classRoom in classRooms)
                    session.Save(classRoom);
            }

            // ===== STUDENTS =====
            if (!session.Query<Student>().Any())
            {
                var classRooms = session.Query<ClassRoom>().ToList();

                var students = new List<Student>
                {
                    new Student { StudentCode = "S001", Name = "Nguyễn Minh 1", Address = "Hà Nội", ClassRoom = classRooms[0] },
                    new Student { StudentCode = "S002", Name = "Nguyễn Minh 2", Address = "Hải Phòng", ClassRoom = classRooms[0] },
                    new Student { StudentCode = "S003", Name = "Nguyễn Minh 3", Address = "Nam Định", ClassRoom = classRooms[1] },
                    new Student { StudentCode = "S004", Name = "Nguyễn Minh 4", Address = "Thanh Hóa", ClassRoom = classRooms[1] },
                    new Student { StudentCode = "S005", Name = "Nguyễn Minh 5", Address = "Hà Nội", ClassRoom = classRooms[2] },
                    new Student { StudentCode = "S006", Name = "Nguyễn Minh 6", Address = "Nghệ An", ClassRoom = classRooms[2] },
                    new Student { StudentCode = "S007", Name = "Nguyễn Minh 7", Address = "Đà Nẵng", ClassRoom = classRooms[3] },
                    new Student { StudentCode = "S008", Name = "Nguyễn Minh 8", Address = "Huế", ClassRoom = classRooms[3] },
                    new Student { StudentCode = "S009", Name = "Nguyễn Minh 9", Address = "Quảng Nam", ClassRoom = classRooms[4] },
                    new Student { StudentCode = "S010", Name = "Nguyễn Minh 10", Address = "TP HCM", ClassRoom = classRooms[4] },
                };

                foreach (var student in students)
                    session.Save(student);
            }

            tx.Commit();
        }
    }
}
