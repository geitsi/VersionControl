using MNB_5.Entities;
using MNB_5.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
            chart1.DataSource = Rates;
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
        public string Req()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBoxCurrency?.SelectedItem?.ToString() ?? "EUR",
                startDate = dateTimePickerStart.Value.ToString("yyyy-MM-dd"),
                endDate = dateTimePickerEnd.Value.ToString("yyyy-MM-dd")
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            return result;
        }
        public void RefreshData()
        {
            Rates.Clear();

            string result = Req();
            xml(result);

            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        public void xml(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                XmlElement childElement = ((XmlElement)element.ChildNodes[0]);

                if (childElement == null)
                {
                    continue;
                }

                decimal unit = decimal.Parse(childElement.GetAttribute("unit"));

                NumberFormatInfo numberFormatWithComma = new NumberFormatInfo();
                numberFormatWithComma.NumberDecimalSeparator = ",";

                decimal value = Math.Round(decimal.Parse(childElement.InnerText, numberFormatWithComma), 2);

                RateData rate = new RateData
                {
                    Date = Convert.ToDateTime(element.GetAttribute("date")),
                    Currency = childElement.GetAttribute("curr"),
                    Value = unit != 0 ? value / unit : value,
                };

                Rates.Add(rate);
            }
        }
    }
}
