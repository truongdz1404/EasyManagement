using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Dtos.Student
{
    [DataContract]
    public class UpdateStudentRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public DateTime Dob { get; set; }

        [DataMember(Order = 4)]
        public string Address { get; set; } = string.Empty;

        [DataMember(Order = 5)]
        public int ClassRoomId { get; set; }
    }
}
