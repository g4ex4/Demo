using Demo.Models.Common;
namespace Demo.Models.Entities
{
    public class Client : Person, ICloneable
    {
        public string PhoneNum { get; set; }
        public int OrderAmount { get; set; }
        public DateTime DateAdd { get; set; }
        public ICollection<Order> Orders { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
