using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextFromDesc
{
    public partial class Form1 : Form
    {
        public static Bitmap screenShot = null;
        public Form1()
        {
            InitializeComponent();
            this.Location = new Point(0,0);
        } 
        private void button1_Click(object sender, EventArgs e)
        { 
            ScreenForm screenForm = new ScreenForm(this);
            Rectangle r = Screen.PrimaryScreen.Bounds;
            screenForm.Size =  new Size(r.Width, r.Height); 
            screenForm.Location = new Point(r.X, r.Y);
            screenForm.BackColor = Color.Red;
            screenForm.TransparencyKey = Color.Red;
            screenForm.Show();
            Utils.GetScreenScaling();
            this.Visible = false;
        }
         
    }
}
