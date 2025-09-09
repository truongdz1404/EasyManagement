using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EasyMN.Shared.Dtos.Report
{
    [DataContract]
    public class ClassStatsRequest
    {
        [DataMember(Order = 1)]
        public int ClassId { get; set; }
    }
}