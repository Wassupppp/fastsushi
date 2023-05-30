using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastsushi
{
    public partial class Form10 : Form
    {
        private string nomIngredient;
        private decimal prixIngredient;
        private int stock;

        public string NomIngredient
        {
            get { return textBox1.Text; }   
        }
        public decimal PrixIngredient
        {
            get { return decimal.Parse(textBox2.Text); }
        }
        public int Stock
        {
            get { return int.Parse(textBox3.Text); }
        }
        public Form10()
        {
            InitializeComponent();
        }
        public Form10(string nomIngredient, decimal prixIngredient, int Stock)
        {
            InitializeComponent();

            // Stockez les paramètres dans les champs de la classe
            this.nomIngredient = nomIngredient;
            this.prixIngredient = prixIngredient;
            this.stock = Stock;

            // Affichez les valeurs actuelles dans les contrôles de la fenêtre de modification
            textBox1.Text = nomIngredient;
            textBox2.Text = prixIngredient.ToString();
            textBox3.Text = stock.ToString();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Vérifier si les nouvelles valeurs sont valides
            if (!string.IsNullOrEmpty(textBox1.Text) && decimal.TryParse(textBox2.Text, out _))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nom , un prix et un stock valides pour le produit.");
            }
        }
    }
}
