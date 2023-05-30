using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fastsushi
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        public string NomIngredient
        {
            get { return textBox1.Text; }
        }
        public decimal PrixIngredient
        {
            get { return decimal.Parse(textBox2.Text);}
        }
        public int Stock
        {
            get { return int.Parse(textBox3.Text); }
        }
        public string imagePath
        {
            get;
            private set;
        }
        public string nameImage
        {
            get;
            private set;
        }
            

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers image (*.jpg, *jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Sélectionner une image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                nameImage = "img/" + openFileDialog.SafeFileName;


                // Enregistrer l'image dans un emplacement de votre choix
                string destinationPath = "C:\\Users\\admin\\PhpstormProjects\\fastsushi_project\\public\\img\\" + openFileDialog.SafeFileName;

                //Vérifier si le fichier de destination existe déja
                if (File.Exists(destinationPath))
                {
                    MessageBox.Show("Le fichier existe déja.");
                }
                else
                {
                    // Copier le fichier vers le dossier de destination
                    File.Copy(imagePath, destinationPath);
                    // Charger l'image dans le PictureBox
                    Image image = Image.FromFile(imagePath);
                    pictureBox1.Image = image;


                    //Ajustez le mode d'affichage de l'image dans le PictureBox
                    if (image.Width > pictureBox1.Width || image.Height > pictureBox1.Height)
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Vérifier si les valeurs saisies sont valides
            if (!string.IsNullOrEmpty(textBox1.Text) && decimal.TryParse(textBox2.Text, out _))
            {
                //Vérifier si une image a été sélectionnée
                if (!string.IsNullOrEmpty(imagePath))
                {
                    // Fermer la fenêtre en indiquant que l'action a réussi
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une image pour l'ingredient.");
                }

            }
            else
            {
                // Afficher un message d'erreur si les valeurs saisies sont invalides
                MessageBox.Show("Veuillez saisir un nom , un prix et le stock  valides pour l'ingredient.");
            }
        }
    }
}
