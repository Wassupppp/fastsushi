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

namespace fastsushi
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Server=REVISION-PC;Database=fast_sushi;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
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

            WriteToConsole("Mot de passe chiffré : " + hashedPassword);

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Connexion réussie !");
                //Ajoutez ici le code pour afficher votre formulaire principale
                this.Hide();
                //Affichage de Accueil (nouvel fenêtre)
                Form w = new Form2();
                w.ShowDialog(); // Bloquant jusqu'a la fermeture du form
                w = null;
                this.Show();

            }
            else
            {
                MessageBox.Show("Identifiants incorrects. Veuillez réessayer.");
            }
        }
        private void WriteToConsole(string message)
        {
            txtConsole.AppendText(message + Environment.NewLine);
        }

        private string HashPassword(string password)
        {
            // On va utilisez ici l'algorithme de hachage souhaité Agron2i)
            //Argon2i
            var argon2 = new Argon2i(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 8,
                MemorySize = 65536,
                Iterations = 4
            };
            byte[] hash = argon2.GetBytes(32);

            string encodeHash = Convert.ToBase64String(hash);
            string hashedPassword = "$argon2i$v=19$m=65536,t=4,p=1${"+encodeHash+"}";
            return hashedPassword;
        }

        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Utilisateurs WHERE emailUtilisateur = @username AND mdpUtilisateur = @password";

                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();

                    return count > 0;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
