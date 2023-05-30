using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastsushi
{
    public partial class Form2 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";

        public Form2()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Affichage du form3
            Form z = new Form3();
            z.ShowDialog();
            z = null;
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Affichage de Form4 (Historique des commandes)
            Form a = new Form4();
            a.ShowDialog();
            a = null;
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Affichage de Form5(Gestion des Produits)
            Form b = new Form5();
            b.ShowDialog();
            b = null;
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Affichage de Form8(Gestion des commandes)
            Form b = new Form8();
            b.ShowDialog();
            b = null;
            this.Show();
        }
    }
}
