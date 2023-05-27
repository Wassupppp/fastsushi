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
    public partial class Form4 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";
        public Form4()
        {
            InitializeComponent();
            // Configurez le ListView pour afficher les détails
            listView1.View = View.Details;
            listView1.Columns.Add("ID Commande");
            listView1.Columns.Add("Statut");
            listView1.Columns.Add("Prix Total");
            listView1.Columns.Add("ID Utilisateur");
            listView1.Columns.Add("Produit");
            listView1.Columns.Add("Quantité");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT c.idCommande,c.statut,prixTotal,c.idUtilisateurs, STRING_AGG(p.nomProduit, ', ') as nomProduit, STRING_AGG(lc.nbProduit, ', ') as nbProduit from Commandes c INNER JOIN ligne_Commande lc ON lc.idCommande =  c.idCommande INNER JOIN Produits p ON p.idProduit = lc.idProduit WHERE c.statut = '1' group by c.idCommande, c.statut, prixTotal, c.idUtilisateurs, lc.nbProduit;";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //StringBuilder resultBuilder = new StringBuilder();

                        while (reader.Read())
                        {
                            string idCommande = reader["idCommande"].ToString();
                            string statut = reader["statut"].ToString();
                            string prixTotal = reader["prixTotal"].ToString();
                            string idUtilisateur = reader["idUtilisateurs"].ToString();
                            string nomProduit = reader["nomProduit"].ToString();
                            string nbProduit = reader["nbProduit"].ToString();

                            string[] row = { idCommande, statut, prixTotal, idUtilisateur, nomProduit, nbProduit };
                            ListViewItem item = new ListViewItem(row);
                            listView1.Items.Add(item);
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
