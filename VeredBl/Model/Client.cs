
using System.Collections.Generic;


namespace VeredBl.Model
{
   public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Check> Checks { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
