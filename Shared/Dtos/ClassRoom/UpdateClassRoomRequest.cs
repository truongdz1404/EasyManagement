using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.ClassRoom
{
    [DataContract]
    public class UpdateClassRoomRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string ClassName { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Subject { get; set; } = string.Empty;

        [DataMember(Order = 4)]
        public int TeacherId { get; set; }
    }
}
