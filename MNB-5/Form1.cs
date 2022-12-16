using MNB_5.Entities;
using MNB_5.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MNB_5
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates;
            comboBoxCurrency.DataSource = Currencies;
            GetData();
        }
        void GetData()
        {
            var client = new MNBArfolyamServiceSoapClient();
            var requestBody = new GetCurrenciesRequestBody();

            var response = client.GetCurrencies(requestBody);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response.GetCurrenciesResult);
            Console.WriteLine(response.GetCurrenciesResult);
            foreach (XmlElement element in xml.DocumentElement.ChildNodes[0])
            {
                Currencies.Add(element.InnerText);
            }
            comboBoxCurrency.DataSource = Currencies;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
