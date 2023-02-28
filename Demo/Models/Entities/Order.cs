using Demo.Models.Common;

namespace Demo.Models.Entities
{
    public class Order: BaseEntity, ICloneable
    {
        public DateTime OrderDate { get; set; }
        public uint ClientId { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime CloseDate { get; set; }
        public Client Client { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
