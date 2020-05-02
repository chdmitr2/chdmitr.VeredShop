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
    public partial class ClientForm : Form
    {
        public Client Client { get; set; }
        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(Client client) : this()
        {
            Client = client;
            textBox1.Text = client.Name;
        }
            

        private void ClientForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = Client ?? new Client();
            c.Name = textBox1.Text;
            Close();

        }
    }
}
