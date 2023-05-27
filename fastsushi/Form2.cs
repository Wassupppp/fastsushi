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
            // Configurez le ListView pour afficher les détails
            listView1.View = View.Details;
            listView1.Columns.Add("ID Commande");
            listView1.Columns.Add("Statut");
            listView1.Columns.Add("Prix Total");
            listView1.Columns.Add("ID Utilisateur");
            listView1.Columns.Add("Produit");
            listView1.Columns.Add("Quantité");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               string query = "SELECT c.idCommande,c.statut,prixTotal,c.idUtilisateurs, STRING_AGG(p.nomProduit, ', ') as nomProduit, STRING_AGG(lc.nbProduit, ', ') as nbProduit from Commandes c INNER JOIN ligne_Commande lc ON lc.idCommande =  c.idCommande INNER JOIN Produits p ON p.idProduit = lc.idProduit WHERE c.statut = '0' group by c.idCommande, c.statut, prixTotal, c.idUtilisateurs, lc.nbProduit;";
               using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader()) { 
                        //StringBuilder resultBuilder = new StringBuilder();

                        while(reader.Read())
                        {
                            string idCommande = reader["idCommande"].ToString();
                            string statut = reader["statut"].ToString() ;
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Affichage de Form4 (Historique des commandes)
            Form a = new Form4();
            a.ShowDialog();
            a = null;
            this.Show();
        }
    }
}
