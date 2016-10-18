using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Record
{
    public partial class Photo_Viewer : Form
    {
        public Photo_Viewer()
        {
            InitializeComponent();
        }
        public void showPhoto(string picturePath)
        {   
            Image img = Image.FromFile(@picturePath);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;
        }
    }
}
