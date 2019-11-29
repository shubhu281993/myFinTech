using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myFinTech.Api.Domain.Entities
{
    public class WatchListDto
    {
        public string Id { get; set; }

        public string CustomerID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<WatchListDetailsDto> Details { get; set; }

    }
    public class WatchListDetailsDto
    {
        public string InstrumentName { get; set; }
        public string InstrumentId { get; set; }
        public string SortID { get; set; }
    }
}
