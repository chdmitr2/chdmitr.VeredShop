using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeredBl.Model;

namespace VeredInterface
{
    public partial class Main : Form
    {
        VeredContext db;
        public Main()
        {
            InitializeComponent();
            db = new VeredContext();
        }

        

       
        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products);
            catalogProduct.Show();
        }
    }
}
