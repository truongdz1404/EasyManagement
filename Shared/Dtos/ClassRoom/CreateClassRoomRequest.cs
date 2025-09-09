using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.ClassRoom
{
    [DataContract]
    public class CreateClassRoomRequest
    {
        [DataMember(Order = 1)]
        public string ClassCode { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string ClassName { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Subject { get; set; } = string.Empty;

        [DataMember(Order = 4)]
        public int TeacherId { get; set; }
    }
}
