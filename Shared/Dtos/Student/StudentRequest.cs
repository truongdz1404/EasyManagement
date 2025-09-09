using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Dtos.Student
{
    [DataContract]
    public class StudentRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }

    [DataContract]
    public class StudentCodeRequest
    {
        [DataMember(Order = 1)]
        public string StudentCode { get; set; } = string.Empty;
    }
}
