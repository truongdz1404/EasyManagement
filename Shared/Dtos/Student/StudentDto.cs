using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Dtos.Student
{
    [DataContract]
    public class StudentDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string StudentCode { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Name { get; set; } = string.Empty;

        [DataMember(Order = 4)]
        public DateTime Dob { get; set; }

        [DataMember(Order = 5)]
        public string Address { get; set; } = string.Empty;

        [DataMember(Order = 6)]
        public string ClassRoomName { get; set; } = string.Empty;

        [DataMember(Order = 7)]
        public int ClassRoomId { get; set; }
    }
}
