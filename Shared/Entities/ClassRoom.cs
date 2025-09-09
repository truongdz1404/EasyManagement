using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Entities
{
    public class ClassRoom
    {
        public virtual int Id { get; set; }
        public virtual string ClassCode { get; set; } = string.Empty;
        public virtual string ClassName { get; set; } = string.Empty;
        public virtual string Subject { get; set; }   = string.Empty;
        public virtual int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; } = null!;
        public virtual IList<Student> Students { get; set; } = new List<Student>();
    }
}
