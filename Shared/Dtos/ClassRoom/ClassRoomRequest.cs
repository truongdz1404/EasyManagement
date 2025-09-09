using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.ClassRoom
{
    [DataContract]
    public class ClassRoomRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
