using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasyMN.Shared.Models
{
    [DataContract]
    public class PagedResult<T>
    {
        [DataMember(Order = 1)]
        public int TotalItems { get; set; }

        [DataMember(Order = 2)]
        public int PageNumber { get; set; }

        [DataMember(Order = 3)]
        public int PageSize { get; set; }

        [DataMember(Order = 4)]
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
