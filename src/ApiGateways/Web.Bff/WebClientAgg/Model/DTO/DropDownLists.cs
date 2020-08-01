using System.Collections.Generic;

namespace WebClientAgg.Model.DTO
{
    public class DropDownLists
    {
        public IEnumerable<Bedrooms> bedrooms { get; set; }
        public IEnumerable<Countries> countries { get; set; }
        public IEnumerable<Furnishings> furnishings { get; set; }
        public IEnumerable<Purpose> purposes { get; set; }
        public IEnumerable<Periods> periods { get; set; }
    }
}
