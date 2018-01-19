using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Drawing.Drawing2D;

namespace TextFromDesc
{
    public partial class ScreenForm : Form
    {
        private bool IsClicked = false;
        private bool IsMouseUp = false;
        private Form parentForm = new Form();
        public ScreenForm(Form form)
        {
            InitializeComponent();
            panel1.Width = this.Width;
            this.parentForm = form;
        } 
         
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            parentForm.Visible = true;
        }
        private void ScreenForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
              
                IsMouseUp = false;
                ScreenForm screenForm = sender as ScreenForm;
                if (screenForm == null)
                    return;
                Utils.OnMouseDown(screenForm, Cursor.Position.X, Cursor.Position.Y);
                IsClicked = true;
            }
        }

        private void ScreenForm_MouseMove(object sender, MouseEventArgs e)
        { 
            if (IsClicked && !IsMouseUp)
            {
                ScreenForm screenForm = sender as ScreenForm;
                if (screenForm == null)
                    return;
               
                Utils.OnMouseMove(screenForm, Cursor.Position.X, Cursor.Position.Y);
            }
        } 
        private void ScreenForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseUp = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Converter convert = new Converter(this.progressBar1, label1); 
            convert.Run(Utils.Cut());
            this.label1.Visible = true;
        }
    }
}
