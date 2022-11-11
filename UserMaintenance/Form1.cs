using System.ComponentModel;
using UserMaintenance.Entities;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName; // label1
            //label2.Text = Resource1.FirstName; // label2
            button1.Text = Resource1.Add; // button1
            button2.Text = Resource1.WriteFile;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
                //FirstName = textBox2.Text
            };
            users.Add(u);
            listBox1.Items.Clear();
            for (int i = 0; i < users.Count; i++)
            {
                listBox1.Items.Add(users[i].FullName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        myStream.Write(Encoding.ASCII.GetBytes(users[i].ID + "   " + users[i].FullName + "\n"));
                    }
                    myStream.Close();
                }
            }
        }
    }
}