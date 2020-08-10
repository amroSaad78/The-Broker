using System.Collections.Generic;

namespace WebClientAgg.Model.DTO
{
    public class ApartmentSubLists
    {
        public IEnumerable<BasicOwner> Owners { get; set; }
        public IEnumerable<Bedrooms> Bedrooms { get; set; }
        public IEnumerable<Countries> Countries { get; set; }
        public IEnumerable<Furnishings> Furnishings { get; set; }
        public IEnumerable<Periods> Periods { get; set; }
    }
}
