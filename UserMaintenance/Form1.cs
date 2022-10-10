using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource1.LastName;
            lblFirstName.Text = Resource1.Firstname;
            button1.Text = Resource1.Add;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}