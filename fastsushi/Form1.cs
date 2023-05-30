using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Konscious.Security.Cryptography;
using System.IO;
using BCryptNet = BCrypt.Net.BCrypt;

namespace fastsushi
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";

        
        public Form1()
        {
            InitializeComponent();
        }
        private string GetUserRole(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT roles FROM Utilisateurs WHERE emailUtilisateur = @username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);

                    connection.Open();
                    string role = (string)command.ExecuteScalar();
                    connection.Close();

                    return role;
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //Bouton de Connexion
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            string hashedPassword = HashPassword(password); // chiffrement du mot de passe

            ValidateUser(username, password);
          
        }
    
        private string HashPassword(string password)
        {
            // Bcrypt
            string salt = BCryptNet.GenerateSalt(12);
           
            string hashedPassword = BCryptNet.HashPassword(password, salt);
            return hashedPassword;
        }
        private void ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string query = "SELECT COUNT(*) FROM Utilisateurs WHERE emailUtilisateur = @username AND mdpUtilisateur = @password";
                string query = "SELECT mdpUtilisateur FROM Utilisateurs WHERE emailUtilisateur = @username "; // rajouter le role admin ou prep

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    //command.Parameters.AddWithValue("password", password);

                    connection.Open();
                    //int count = (int)command.ExecuteScalar();
                    string hashedPassword = (string)command.ExecuteScalar();
                    connection.Close();

                    if (!string.IsNullOrEmpty(hashedPassword))
                    {
                        string role = GetUserRole(username);

                        if (BCryptNet.Verify(password, hashedPassword))
                        {
                            MessageBox.Show("Connexion réussie !");
                            // Vérifier le rôle.
                            if (role == "[\"admin\"]")
                            {
                                //Ajoutez ici le code pour afficher votre formulaire principale
                                this.Hide();
                                //Affichage de Accueil (nouvel fenêtre)
                                Form w = new Form2();
                                w.ShowDialog(); // Bloquant jusqu'a la fermeture du form
                                w = null;
                                this.Show();
                            }
                            else if (role == "[\"Preparateur\"]")
                            {
                                //Ajoutez ici le code pour afficher votre formulaire principale
                                this.Hide();
                                //Affichage de Accueil (nouvel fenêtre)
                                Form w = new Form8();
                                w.ShowDialog(); // Bloquant jusqu'a la fermeture du form
                                w = null;
                                this.Show();
                            }
                            else
                            {
                                MessageBox.Show("Pas accés . Veuillez réessayer.");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Identifiants incorrects. Veuillez réessayer.");
                        }

                    }

                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            textBox2.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
