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
            button3.Text = Resource1.Delete;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "FullName";
            listBox1.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,
                //FirstName = textBox2.Text
            };
            users.Add(u);
            
            listboxrefresh();
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
                        
                        myStream.Write(Encoding.Default.GetBytes(users[i].ID + "   " + users[i].FullName + "\n"));
                    }
                    myStream.Close();
                    MessageBox.Show(Resource1.SuccWrite);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                users.Remove(listBox1.SelectedItem as Entities.User);
            }
            listboxrefresh();           
        }
        private void listboxrefresh()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < users.Count; i++)
            {   
                listBox1.Items.Add(users[i]);
            }
            if (listBox1.Items.Count == 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }

            button3.Enabled = false;
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}