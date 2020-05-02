using System;
using System.Linq;
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

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers);
            catalogSeller.Show();
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogClient = new Catalog<Client>(db.Clients);
            catalogClient.Show();
        }

        private void checksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkClient = new Catalog<Check>(db.Checks);
            checkClient.Show();
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new ClientForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                db.Clients.Add(form.Client);
                db.SaveChanges();

            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();

            }
        }
    }
}
