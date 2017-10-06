using System.Collections.Generic;

namespace Ideas.Domain.Common.Models
{
    public class ItemsResult<T>
    {
        public List<T> Items { get; set; }
    }
}
