using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource1.LastName;
            lblFirstName.Text = Resource1.Firstname;
            button1.Text = Resource1.Add;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text
            };
            users.Add(u);
        }
    }
}