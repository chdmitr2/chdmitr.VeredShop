using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeredShopBL.Model
{
    public class Manager
    {
        static Manager uniqueInstance;
        string managerEmail = string.Empty;
        string managerPassword = string.Empty;

       public Manager()
        {

        }

        public static Manager Instance()
        {
            if(uniqueInstance == null)
            {
                uniqueInstance = new Manager();
            }

            return uniqueInstance;
        }

        public void ManagerOperation()
        {
            managerEmail = "admin";
            managerPassword = "admin";
        }

        public string GetManagerEmail()
        {
           return managerEmail;
        }

        public string GetManagerPassword()
        {
            return managerPassword;            
        }


    }
}
