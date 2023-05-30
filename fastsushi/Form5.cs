using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastsushi
{
    public partial class Form5 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";

        private Form7 form7;
        private Form10 form10;

        public Form5()
        {
            InitializeComponent();
            // Appel de la méthode pour charger les données dans le ListView
            listView1.View = View.Details;
            listView1.Columns.Add("nomProduit");
            listView1.Columns.Add("prixProduit");

            // Appel de la méthode pour charger les données dans le ListView
            listView2.View = View.Details;
            listView2.Columns.Add("nomProduit");
            listView2.Columns.Add("prixProduit");

            // Appel de la méthode pour charger les données dans le ListView
            listView3.View = View.Details;
            listView3.Columns.Add("nomProduit");
            listView3.Columns.Add("prixProduit");

            // Appel de la méthode pour charger les données dans le ListView
            listView4.View = View.Details;
            listView4.Columns.Add("nomIngredients");
            listView4.Columns.Add("prixIngredients");
            listView4.Columns.Add("Stock");


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nomProduit, prixProduit FROM Produits WHERE idCategorie = '1';"; // Entrées

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Récupérer les valeurs des colonnes de la requête
                        string nomProduit = reader["nomProduit"].ToString();    
                        string prixProduit = reader["prixProduit"].ToString() ;

                        string[] row = { nomProduit, prixProduit };
                        // Créer un nouvel objet ListViewItem avec les valeurs récupérées
                        ListViewItem item = new ListViewItem(row);
                        listView1.Items.Add(item);
                    }

                    reader.Close();

                }
                conn.Close();
                string query2 = "SELECT nomProduit, prixProduit FROM Produits WHERE idCategorie = '0';";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        // Récupérer les valeurs des colonnes de la requête
                        string nomProduit = reader["nomProduit"].ToString();
                        string prixProduit = reader["prixProduit"].ToString();

                        string[] row = { nomProduit, prixProduit };
                        // Créer un nouvel objet ListViewItem avec les valeurs récupérées
                        ListViewItem item = new ListViewItem(row);
                        listView2.Items.Add(item);
                    }
                    reader.Close();
                }
                conn.Close ();
                string query3 = "SELECT nomProduit, prixProduit FROM Produits WHERE idCategorie = '2';";

                using (SqlCommand cmd3 = new SqlCommand( query3, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd3.ExecuteReader();
                    while (reader.Read())
                    {
                        // Récupérer les valeurs des colonnes de la requête
                        string nomProduit = reader["nomProduit"].ToString();
                        string prixProduit = reader["prixProduit"].ToString();

                        string[] row = { nomProduit, prixProduit };
                        // Créer un nouvel objet ListViewItem avec les valeurs récupérées
                        ListViewItem item = new ListViewItem(row);
                        listView3.Items.Add(item);
                    }
                    reader.Close();
                }
                conn.Close();

                string query4 = "SELECT nomIngredient, prixIngredient, imgIngredient, nbIngredient FROM Ingredients";

                using (SqlCommand cmd = new SqlCommand(query4, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    string currentIngredient = string.Empty; // Variable pour stocker l'ingrédient actuel

                    while (reader.Read())
                    {
                        string nomIngredient = reader["nomIngredient"].ToString();
                        decimal prixIngredient = (decimal)reader["prixIngredient"];
                        int nbIngredients = (int)reader["nbIngredient"];

                        // Vérifier si l'ingrédient a changé
                        if (nomIngredient != currentIngredient)
                        {
                            currentIngredient = nomIngredient;

                            // Créer un nouvel objet ListViewItem pour l'ingrédient avec le nom comme premier sous-élément
                            ListViewItem ingredientItem = new ListViewItem(nomIngredient);

                            // Ajouter le prix en tant que second sous-élément avec un saut de ligne ('\n') pour afficher sur une nouvelle ligne
                            ingredientItem.SubItems.Add($"{prixIngredient}\n");

                            // Ajouter le stock en tant que troisième sous-élément avec un saut de ligne ('\n') pour afficher sur une nouvelle ligne
                            ingredientItem.SubItems.Add($"{nbIngredients}\n");

                            // Ajouter l'élément à la liste
                            listView4.Items.Add(ingredientItem);
                        }
                        else
                        {
                            // Ajouter uniquement le prix en tant que sous-élément supplémentaire avec un saut de ligne ('\n') pour afficher sur une nouvelle ligne
                            listView4.Items[listView4.Items.Count - 1].SubItems.Add($"{prixIngredient}\n");
                        }
                    }

                    reader.Close();
                }

                conn.Close();
            }
        }

        private Image ResizeImage(Image image, int v1, int v2)
        {
            throw new NotImplementedException();
        }

        private void ActualiserProduits(int idCateg, ListView ls)
        {
            // Effacer les produits actuellement affichés dans le ListView
            ls.Items.Clear();

            // Récupérer les nouveaux produits de la base de données
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nomProduit, prixProduit FROM Produits WHERE idCategorie = '"+idCateg+"';";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string nomProduit = reader["nomProduit"].ToString();
                        string prixProduit = reader["prixProduit"].ToString();

                        string[] row = { nomProduit, prixProduit };
                        ListViewItem item = new ListViewItem(row);
                        ls.Items.Add(item);
                    }

                    reader.Close();
                }

                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Créer une instance de la fenêtre 
            Form6 form6 = new Form6();

            //Afficher la fenêtre en tant que voîte de dialogue modale
            DialogResult result = form6.ShowDialog();

            //Vérifier si l'utilisateur a cliqué sur le bouton "Valider"
            if (result == DialogResult.OK)
            {
                string nomProduit = form6.NomProduit;
                decimal prixProduit = form6.PrixProduit;
                string imagePath = form6.imagePath;
                string nameImage = form6.nameImage;

                // Vérifier si le nom et le prix du produit sont valides
                if (!string.IsNullOrEmpty(nomProduit) && prixProduit > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Produits (nomProduit, imgProduit, prixProduit, idCategorie) VALUES (@nomProduit, @imgProduit, @prixProduit, '1');";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                            cmd.Parameters.AddWithValue("@prixProduit", prixProduit);
                            cmd.Parameters.AddWithValue("@imgProduit", nameImage);

                            //Console.WriteLine("Chemin de l'image :" + imagePath);
                            MessageBox.Show("Chemin de l'image :" + nameImage);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Afficher un message de succés
                            MessageBox.Show("Produit ajouté avec succès !");
                            ActualiserProduits(1, listView1);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un nom et un prix valides pour le produit.");
                }
            }       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Vérifier si un élément est sélectionné dans le ListView ( entrée )
            if (listView1.SelectedItems.Count > 0)
            {
                //Récupérer l'élement sélectionné
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Récuperer les valeurs des sous-éléments (colonnes) de l'élément sélectionné
                string nomProduit = selectedItem.SubItems[0].Text;
                string prixProduit = selectedItem.SubItems[1].Text;
                MessageBox.Show("Nom produit :"+ nomProduit + " Prix Produit : "+ prixProduit);

                // Supprimer le produit de la base de données en utilisant les valeurs récupérées
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Produits WHERE nomProduit = @nomProduit;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                        //cmd.Parameters.AddWithValue("@prixProduit", prixProduit);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            // Suppression réussie
                            // Supprimer également l'élément du ListView
                            listView1.Items.Remove(selectedItem);

                            // Afficher un message de succès
                            MessageBox.Show("Produit supprimé avec succès !");
                        }
                        else
                        {
                            // Échec de la suppression
                            MessageBox.Show("Échec de la suppression du produit.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ( listView1.SelectedItems.Count > 0 )
            {
                // Récupérer le produit sélectionné
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string nomProduit = selectedItem.SubItems[0].Text;
                decimal prixProduit = decimal.Parse(selectedItem.SubItems[1].Text);

                // Instancier la fenêtre de modification
                form7 = new Form7(nomProduit, prixProduit);

                // Afficger la fenetre de modification en tant que boîte de dialogue
                DialogResult result = form7.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Les modifications ont été validées 
                    string nouveauNomProduit = form7.NomProduit;
                    decimal nouveauPrixProduit = form7.PrixProduit;

                  using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string updateQuery = "UPDATE Produits SET nomProduit = @nouveauNomProduit, prixProduit = @nouveauPrixProduit WHERE nomProduit = @ancienNomProduit AND prixProduit = @ancienPrixProduit";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@nouveauNomProduit", nouveauNomProduit);
                            updateCmd.Parameters.AddWithValue("@nouveauPrixProduit", nouveauPrixProduit);
                            updateCmd.Parameters.AddWithValue("@ancienNomProduit", nomProduit);
                            updateCmd.Parameters.AddWithValue("@ancienPrixProduit", prixProduit);

                            updateCmd.ExecuteNonQuery();
                        }
                        ActualiserProduits(1, listView1);
                        conn.Close();
                    }

                }

                // Liberer les ressources de la fenêtre de modification
                form7.Dispose();
                form7 = null;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Créer une instance de la fenêtre 
            Form6 form6 = new Form6();

            //Afficher la fenêtre en tant que voîte de dialogue modale
            DialogResult result = form6.ShowDialog();

            //Vérifier si l'utilisateur a cliqué sur le bouton "Valider"
            if (result == DialogResult.OK)
            {
                string nomProduit = form6.NomProduit;
                decimal prixProduit = form6.PrixProduit;
                string imagePath = form6.imagePath;
                string nameImage = form6.nameImage;

                // Vérifier si le nom et le prix du produit sont valides
                if (!string.IsNullOrEmpty(nomProduit) && prixProduit > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Produits (nomProduit, imgProduit, prixProduit, idCategorie) VALUES (@nomProduit, @imgProduit, @prixProduit, '0');";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                            cmd.Parameters.AddWithValue("@prixProduit", prixProduit);
                            cmd.Parameters.AddWithValue("@imgProduit", nameImage);

                            //Console.WriteLine("Chemin de l'image :" + imagePath);
                            MessageBox.Show("Chemin de l'image :" + nameImage);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Afficher un message de succés
                            MessageBox.Show("Produit ajouté avec succès !");
                            ActualiserProduits(0, listView2);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un nom et un prix valides pour le produit.");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                // Récupérer le produit sélectionné
                ListViewItem selectedItem = listView2.SelectedItems[0];
                string nomProduit = selectedItem.SubItems[0].Text;
                decimal prixProduit = decimal.Parse(selectedItem.SubItems[1].Text);

                // Instancier la fenêtre de modification
                form7 = new Form7(nomProduit, prixProduit);

                // Afficger la fenetre de modification en tant que boîte de dialogue
                DialogResult result = form7.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Les modifications ont été validées 
                    string nouveauNomProduit = form7.NomProduit;
                    decimal nouveauPrixProduit = form7.PrixProduit;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string updateQuery = "UPDATE Produits SET nomProduit = @nouveauNomProduit, prixProduit = @nouveauPrixProduit WHERE nomProduit = @ancienNomProduit AND prixProduit = @ancienPrixProduit";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@nouveauNomProduit", nouveauNomProduit);
                            updateCmd.Parameters.AddWithValue("@nouveauPrixProduit", nouveauPrixProduit);
                            updateCmd.Parameters.AddWithValue("@ancienNomProduit", nomProduit);
                            updateCmd.Parameters.AddWithValue("@ancienPrixProduit", prixProduit);

                            updateCmd.ExecuteNonQuery();
                        }
                        ActualiserProduits(0, listView2);
                        conn.Close();
                    }

                }

                // Liberer les ressources de la fenêtre de modification
                form7.Dispose();
                form7 = null;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            // Vérifier si un élément est sélectionné dans le ListView ( entrée )
            if (listView2.SelectedItems.Count > 0)
            {
                //Récupérer l'élement sélectionné
                ListViewItem selectedItem = listView2.SelectedItems[0];

                // Récuperer les valeurs des sous-éléments (colonnes) de l'élément sélectionné
                string nomProduit = selectedItem.SubItems[0].Text;
                string prixProduit = selectedItem.SubItems[1].Text;
                MessageBox.Show("Nom produit :" + nomProduit + " Prix Produit : " + prixProduit);

                // Supprimer le produit de la base de données en utilisant les valeurs récupérées
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Produits WHERE nomProduit = @nomProduit;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                        //cmd.Parameters.AddWithValue("@prixProduit", prixProduit);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            // Suppression réussie
                            // Supprimer également l'élément du ListView
                            listView2.Items.Remove(selectedItem);

                            // Afficher un message de succès
                            MessageBox.Show("Produit supprimé avec succès !");
                        }
                        else
                        {
                            // Échec de la suppression
                            MessageBox.Show("Échec de la suppression du produit.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Créer une instance de la fenêtre 
            Form6 form6 = new Form6();

            //Afficher la fenêtre en tant que voîte de dialogue modale
            DialogResult result = form6.ShowDialog();

            //Vérifier si l'utilisateur a cliqué sur le bouton "Valider"
            if (result == DialogResult.OK)
            {
                string nomProduit = form6.NomProduit;
                decimal prixProduit = form6.PrixProduit;
                string imagePath = form6.imagePath;
                string nameImage = form6.nameImage;

                // Vérifier si le nom et le prix du produit sont valides
                if (!string.IsNullOrEmpty(nomProduit) && prixProduit > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Produits (nomProduit, imgProduit, prixProduit, idCategorie) VALUES (@nomProduit, @imgProduit, @prixProduit, '2');";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                            cmd.Parameters.AddWithValue("@prixProduit", prixProduit);
                            cmd.Parameters.AddWithValue("@imgProduit", nameImage);

                            //Console.WriteLine("Chemin de l'image :" + imagePath);
                            MessageBox.Show("Chemin de l'image :" + nameImage);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Afficher un message de succés
                            MessageBox.Show("Produit ajouté avec succès !");
                            ActualiserProduits(2, listView3);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un nom et un prix valides pour le produit.");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                // Récupérer le produit sélectionné
                ListViewItem selectedItem = listView3.SelectedItems[0];
                string nomProduit = selectedItem.SubItems[0].Text;
                decimal prixProduit = decimal.Parse(selectedItem.SubItems[1].Text);

                // Instancier la fenêtre de modification
                form7 = new Form7(nomProduit, prixProduit);

                // Afficger la fenetre de modification en tant que boîte de dialogue
                DialogResult result = form7.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Les modifications ont été validées 
                    string nouveauNomProduit = form7.NomProduit;
                    decimal nouveauPrixProduit = form7.PrixProduit;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string updateQuery = "UPDATE Produits SET nomProduit = @nouveauNomProduit, prixProduit = @nouveauPrixProduit WHERE nomProduit = @ancienNomProduit AND prixProduit = @ancienPrixProduit";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@nouveauNomProduit", nouveauNomProduit);
                            updateCmd.Parameters.AddWithValue("@nouveauPrixProduit", nouveauPrixProduit);
                            updateCmd.Parameters.AddWithValue("@ancienNomProduit", nomProduit);
                            updateCmd.Parameters.AddWithValue("@ancienPrixProduit", prixProduit);

                            updateCmd.ExecuteNonQuery();
                        }
                        ActualiserProduits(2, listView3);
                        conn.Close();
                    }

                }

                // Liberer les ressources de la fenêtre de modification
                form7.Dispose();
                form7 = null;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à modifier.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Vérifier si un élément est sélectionné dans le ListView ( entrée )
            if (listView3.SelectedItems.Count > 0)
            {
                //Récupérer l'élement sélectionné
                ListViewItem selectedItem = listView3.SelectedItems[0];

                // Récuperer les valeurs des sous-éléments (colonnes) de l'élément sélectionné
                string nomProduit = selectedItem.SubItems[0].Text;
                string prixProduit = selectedItem.SubItems[1].Text;
                MessageBox.Show("Nom produit :" + nomProduit + " Prix Produit : " + prixProduit);

                // Supprimer le produit de la base de données en utilisant les valeurs récupérées
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Produits WHERE nomProduit = @nomProduit;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomProduit", nomProduit);
                        //cmd.Parameters.AddWithValue("@prixProduit", prixProduit);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            // Suppression réussie
                            // Supprimer également l'élément du ListView
                            listView3.Items.Remove(selectedItem);

                            // Afficher un message de succès
                            MessageBox.Show("Produit supprimé avec succès !");
                            ActualiserProduits(2, listView3);
                        }
                        else
                        {
                            // Échec de la suppression
                            MessageBox.Show("Échec de la suppression du produit.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un produit à supprimer.");
            }
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ActualiserIngredient( ListView ls)
        {
            // Effacer les produits actuellement affichés dans le ListView
            ls.Items.Clear();

            // Récupérer les nouveaux produits de la base de données
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT nomIngredient, prixIngredient, imgIngredient, nbIngredient FROM Ingredients;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string nomIngredient = reader["nomIngredient"].ToString();
                        string prixIngredient = reader["prixIngredient"].ToString();
                        string Stock = reader["nbIngredient"].ToString();

                        string[] row = { nomIngredient, prixIngredient, Stock };
                        ListViewItem item = new ListViewItem(row);
                        ls.Items.Add(item);
                    }

                    reader.Close();
                }

                conn.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Créer une instance de la fenêtre 
            Form9 form9 = new Form9();

            //Afficher la fenêtre en tant que voîte de dialogue modale
            DialogResult result = form9.ShowDialog();

            //Vérifier si l'utilisateur a cliqué sur le bouton "Valider"
            if (result == DialogResult.OK)
            {
                string nomIngredient = form9.NomIngredient;
                decimal prixIngredient = form9.PrixIngredient;
                int stock = form9.Stock;
                string imagePath = form9.imagePath;
                string nameImage = form9.nameImage;

                // Vérifier si le nom et le prix du produit sont valides
                if (!string.IsNullOrEmpty(nomIngredient) && prixIngredient > 0 && stock >=0)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Ingredients (nomIngredient, prixIngredient, imgIngredient, nbIngredient) VALUES (@nomIngredient, @prixIngredient, @imgIngredient, @stock);";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nomIngredient", nomIngredient);
                            cmd.Parameters.AddWithValue("@prixIngredient", prixIngredient);
                            cmd.Parameters.AddWithValue("@stock", stock);
                            cmd.Parameters.AddWithValue("@imgIngredient", nameImage);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            //Afficher un message de succés
                            MessageBox.Show("Ingredient ajouté avec succès !");
                            ActualiserIngredient(listView4);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un nom, un prix et un stock valides pour l'ingredient.");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count > 0)
            {
                // Récupérer le produit sélectionné
                ListViewItem selectedItem = listView4.SelectedItems[0];
                string nomIngredient = selectedItem.SubItems[0].Text;
                decimal prixIngredient = decimal.Parse(selectedItem.SubItems[1].Text);
                int stock = int.Parse(selectedItem.SubItems[2].Text);

                // Instancier la fenêtre de modification
                form10 = new Form10(nomIngredient, prixIngredient, stock);

                // Afficger la fenetre de modification en tant que boîte de dialogue
                DialogResult result = form10.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Les modifications ont été validées 
                    string nouveauNomIngredient = form10.NomIngredient;
                    decimal nouveauPrixIngredient = form10.PrixIngredient;
                    int nouveauStock = form10.Stock;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        string updateQuery = "UPDATE Ingredients SET nomIngredient = @nouveauNomIngredient, prixIngredient = @nouveauPrixIngredient, nbIngredient = @nouveauStock WHERE nomIngredient = @ancienNomIngredient AND prixIngredient = @ancienPrixIngredient";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@nouveauNomIngredient", nouveauNomIngredient);
                            updateCmd.Parameters.AddWithValue("@nouveauPrixIngredient", nouveauPrixIngredient);
                            updateCmd.Parameters.AddWithValue("@nouveauStock", nouveauStock);
                            updateCmd.Parameters.AddWithValue("@ancienNomIngredient", nomIngredient);
                            updateCmd.Parameters.AddWithValue("@ancienPrixIngredient", prixIngredient);

                            updateCmd.ExecuteNonQuery();
                        }
                        ActualiserIngredient(listView4);
                        conn.Close();
                    }

                }

                // Liberer les ressources de la fenêtre de modification
                form10.Dispose();
                form10 = null;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un ingredient à modifier.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Vérifier si un élément est sélectionné dans le ListView ( entrée )
            if (listView4.SelectedItems.Count > 0)
            {
                //Récupérer l'élement sélectionné
                ListViewItem selectedItem = listView4.SelectedItems[0];

                // Récuperer les valeurs des sous-éléments (colonnes) de l'élément sélectionné
                string nomIngredient = selectedItem.SubItems[0].Text;
                string prixIngredient = selectedItem.SubItems[1].Text;
                string Stock = selectedItem.SubItems[2].Text;
                MessageBox.Show("Nom Ingredient :" + nomIngredient + " Prix Ingredient : " + prixIngredient + " Stock : " + Stock);

                // Supprimer le produit de la base de données en utilisant les valeurs récupérées
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Ingredients WHERE nomIngredient = @nomIngredient;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nomIngredient", nomIngredient);
                        

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            // Suppression réussie
                            // Supprimer également l'élément du ListView
                            listView4.Items.Remove(selectedItem);

                            // Afficher un message de succès
                            MessageBox.Show("Ingredients supprimé avec succès !");
                            ActualiserIngredient(listView4);
                        }
                        else
                        {
                            // Échec de la suppression
                            MessageBox.Show("Échec de la suppression de l'ingredient.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un ingredient à supprimer.");
            }
        }
    }
}
