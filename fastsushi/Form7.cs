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
    public partial class Form7 : Form
    {
        private string nomProduit;
        private decimal prixProduit;

        public string NomProduit
        {
            get { return textBox1.Text; }
        }

        public decimal PrixProduit
        {
            get { return decimal.Parse(textBox2.Text); }
        }
        public Form7()
        {
            InitializeComponent();
        }

        public Form7(string nomProduit, decimal prixProduit)
        {
            InitializeComponent();

            // Stockez les paramètres dans les champs de la classe
            this.nomProduit = nomProduit;
            this.prixProduit = prixProduit;

            // Affichez les valeurs actuelles dans les contrôles de la fenêtre de modification
            textBox1.Text = nomProduit;
            textBox2.Text = prixProduit.ToString();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Vérifier si les nouvelles valeurs sont valides
            if (!string.IsNullOrEmpty(textBox1.Text) && decimal.TryParse(textBox2.Text, out _)) { 
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nom et un prix valides pour le produit.");
            }
        }
    }
}
