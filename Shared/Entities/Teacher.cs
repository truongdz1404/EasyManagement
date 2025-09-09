using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Entities
{
    public class Teacher
    {
        public virtual int Id { get; set; }
        public virtual string TeacherCode { get; set; } = string.Empty;
        public virtual string Name { get; set; } = string.Empty;
        public virtual DateTime Dob { get; set; }
        public virtual IList<ClassRoom> Classes { get; set; } = new List<ClassRoom>();
    }
}
