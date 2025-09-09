using System;
using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.ClassRoom
{
    [DataContract]
    public class ClassRoomDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string ClassCode { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string ClassName { get; set; } = string.Empty;

        [DataMember(Order = 4)]
        public string Subject { get; set; } = string.Empty;

        [DataMember(Order = 5)]
        public int TeacherId { get; set; }

        [DataMember(Order = 6)]
        public string TeacherName { get; set; } = string.Empty;
    }
}
