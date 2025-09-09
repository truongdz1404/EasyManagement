using System.Runtime.Serialization;

namespace EasyMN.Shared.Dtos.Report
{
    [DataContract]
    public class DashboardStatsDto
    {
        [DataMember(Order = 1)]
        public int TotalTeachers { get; set; }

        [DataMember(Order = 2)]
        public int TotalClasses { get; set; }

        [DataMember(Order = 3)]
        public int TotalStudents { get; set; }
    }
}
