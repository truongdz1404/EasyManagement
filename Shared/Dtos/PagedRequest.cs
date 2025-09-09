using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Dtos
{
    [DataContract]
    public class PagedRequest
    {
        [DataMember(Order = 1)]
        public int PageNumber { get; set; } = 1;

        [DataMember(Order = 2)]
        public int PageSize { get; set; } = 10;

        [DataMember(Order = 3)]
        public string? Keyword { get; set; } 

        [DataMember(Order = 4)]
        public bool? SortByName { get; set; } 
    }
}
