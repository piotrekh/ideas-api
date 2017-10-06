using System.Collections.Generic;

namespace Ideas.Api.Dtos.Common
{
    public class ItemsResult<T>
    {
        public List<T> Items { get; set; }
    }
}
