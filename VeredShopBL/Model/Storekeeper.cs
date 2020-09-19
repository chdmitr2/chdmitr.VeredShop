using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Storekeeper
    {
        public int StorekeeperId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Salary { get; set; }

        public ICollection<Product> Products { get; set; }

        #region toString
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
        #endregion
    }
}
