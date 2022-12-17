using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week07
{
    public partial class Form1 : Form
    {
        List<Tick> Ticks;

        PortfolioEntities context = new PortfolioEntities();

        //List<PortfolioItem> Portfolio = new List<PortfolioItem>();

        List<decimal> Nyereségek = new List<decimal>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Tick.ToList();

            dataGridView1.DataSource = Ticks;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
