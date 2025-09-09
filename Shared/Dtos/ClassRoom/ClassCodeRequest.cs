using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.ClassRoom
{
    [DataContract]
    public class ClassCodeRequest
    {
        [DataMember(Order = 1)]
        public string ClassCode { get; set; } = string.Empty;
    }
}
