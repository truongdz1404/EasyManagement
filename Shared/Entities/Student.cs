using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Entities
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string StudentCode { get; set; } = string.Empty;
        public virtual string Name { get; set; } = string.Empty;
        public virtual DateTime Dob { get; set; }
        public virtual string Address { get; set; } = string.Empty;
        public virtual int ClassRoomId { get; set; }
        public virtual ClassRoom ClassRoom { get; set; } = null!;
    }
}
