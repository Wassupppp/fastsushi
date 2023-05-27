using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastsushi
{
    public partial class Form3 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";
        public Form3()
        {
            InitializeComponent();

            // Configurez le ListView pour afficher les détails
            listView1.View = View.Details;
            listView1.Columns.Add("ID Utilisateur");
            listView1.Columns.Add("Nom");
            listView1.Columns.Add("Prenom");
            listView1.Columns.Add("Téléphone");
            listView1.Columns.Add("Email");
            listView1.Columns.Add("Pseudo");


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT idUtilisateurs, nomUtilisateur, prenomUtilisateur, telUtilisateur, emailUtilisateur, pseudonymeUtilisateur FROM Utilisateurs";

                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idUtilisateurs = reader["idUtilisateurs"].ToString();
                            string nom = reader["nomUtilisateur"].ToString();
                            string prenom = reader["prenomUtilisateur"].ToString();
                            string telephone = reader["telUtilisateur"].ToString();
                            string email = reader["emailUtilisateur"].ToString();
                            string pseudo = reader["pseudonymeUtilisateur"].ToString();

                            string[] row = { idUtilisateurs, nom, prenom, telephone, email, pseudo };
                            ListViewItem item = new ListViewItem(row);
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Retour_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
