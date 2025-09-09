using System.Runtime.Serialization;

namespace Shared.Dtos.Report
{
    [DataContract]
    public class ClassStatsDto
    {
        [DataMember(Order = 1)]
        public int ClassId { get; set; }

        [DataMember(Order = 2)]
        public string ClassName { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public int TotalStudents { get; set; }
    }
}
