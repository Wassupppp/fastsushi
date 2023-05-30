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
    public partial class Form8 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";

        public Form8()
        {
            InitializeComponent();

            // Configurez le ListView pour afficher les détails
            listView1.View = View.Details;
            listView1.Columns.Add("ID Commande");
            listView1.Columns.Add("Statut");
            listView1.Columns.Add("Prix Total");
            listView1.Columns.Add("Nom");
            listView1.Columns.Add("Prenom");
            listView1.Columns.Add("Produit");
            listView1.Columns.Add("Ingredients");
            listView1.Columns.Add("Quantité");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT c.idCommande, c.statut, prixTotal, u.nomUtilisateur, u.prenomUtilisateur, STRING_AGG(p.nomProduit, ', ') as nomProduit, STRING_AGG(i.nomIngredient, ', ') as Ingredients, lc.nbProduit FROM Commandes c INNER JOIN ligne_Commande lc ON lc.idCommande = c.idCommande INNER JOIN Utilisateurs u ON u.idUtilisateurs = c.idUtilisateurs INNER JOIN Produits p ON p.idProduit = lc.idProduit LEFT JOIN composition_Produit cp ON cp.idLP = lc.idLC AND cp.idIngredient IS NOT NULL LEFT JOIN Ingredients i ON i.idIngredient = cp.idIngredient WHERE c.statut = '0' GROUP BY c.idCommande, c.statut, prixTotal, c.idUtilisateurs, lc.nbProduit, u.nomUtilisateur, u.prenomUtilisateur, i.nomIngredient;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //StringBuilder resultBuilder = new StringBuilder();

                        while (reader.Read())
                        {
                            string idCommande = reader["idCommande"].ToString();
                            string statut = reader["statut"].ToString();
                            string prixTotal = reader["prixTotal"].ToString();
                            string nomUtilisateur = reader["nomUtilisateur"].ToString();
                            string prenomUtilisateur = reader["prenomUtilisateur"].ToString();
                            string nomProduit = reader["nomProduit"].ToString();
                            string ingredient = reader["Ingredients"].ToString();
                            string nbProduit = reader["nbProduit"].ToString();

                            string[] row = { idCommande, statut, prixTotal, nomUtilisateur, prenomUtilisateur, nomProduit, ingredient, nbProduit };
                            ListViewItem item = new ListViewItem(row);
                            listView1.Items.Add(item);
                        }

                    }
                }
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Vérifier si une commande est sélectionnée dans le ListView
            if (listView1.SelectedItems.Count > 0)
            {
                // Récupérer l'ID de la commande sélectionnée
                string idCommande = listView1.SelectedItems[0].SubItems[0].Text;

                // Mettre à jour le statut de la commande dans la base de données
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE Commandes SET statut = 1 WHERE idCommande = @idCommande";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCommande", idCommande);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Commande validée avec succès !");

                            //Supprimer l'élement sélectionné du ListView juste pour l'affichage
                            listView1.SelectedItems[0].Remove();
                        }
                        else
                        {
                            MessageBox.Show("Échec de la validation de la commande.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une commande à valider.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
