using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.VeredShopModel
{
    public class Client
    {
        public int ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }

        #region Constructors
        public Client()
        {

        }

        public Client(string firstName, string lastName, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }
        #endregion

        #region toString
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
        #endregion
    }
}