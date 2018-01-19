using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextFromDesc
{
   public static class Utils
    {
        public static Bitmap btnOrigin = null;
        public static int cropX;
        public static int cropY;
        public static int cursorPosX;
        public static int cursorPosY;
        public static int cropWidth;
        public static int cropHeight;
        public static Pen cropPen;
        public static void OnMouseDown(Form form, int x, int y)
        {
                form.Cursor = Cursors.Cross;
                cropX = x;
                cropY = y;
                cursorPosX = Cursor.Position.X;
                cursorPosY = Cursor.Position.Y;
                cropPen = new Pen(Color.Black, 2);
                cropPen.DashStyle = DashStyle.DashDot;
       
               
        }
        public static void OnMouseMove(Form form, int x, int y)
        {
            form.Refresh();
            cropWidth = x - cropX;
            cropHeight = y - cropY;
            form.CreateGraphics().DrawRectangle(Utils.cropPen, cropX, cropY, cropWidth, cropHeight);
           
        }
        public static void GetScreenScaling()
        {
            using (Graphics gp = Graphics.FromHwnd(IntPtr.Zero))
            {
                float x = gp.DpiX ;
                float y = gp.DpiY;
               // MessageBox.Show($"gp.DpiX is {gp.DpiX}  gp.DpiY is {gp.DpiY}");
            }
           
        }
        public static Bitmap Cut()
        {
            Bitmap btn = new Bitmap((int)(cropWidth * 1.50), (int)(cropHeight * 1.50), PixelFormat.Format32bppArgb);
            Rectangle rec = new Rectangle(new Point((int)(cropX*1.50), (int)(cropY*1.50)), new Size((int)(cropWidth*1.5), (int)(cropHeight*1.50)));
            using (Graphics g = Graphics.FromImage(btn))
            { 
                g.CopyFromScreen((int)(cursorPosX*1.50), (int)(cursorPosY*1.50), 0, 0, rec.Size); 
            } 
            return btn;
        } 
    }
}
