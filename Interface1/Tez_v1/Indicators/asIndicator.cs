using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Tez_v1
{
    class asIndicator
    {
                   
        PictureBox indicatorBox;
        Image back, igne;
        Int32 Xkoor, Ykoor;
        Double OranWidth, OranHeight, ImageWidth, ImageHeight;
        public Double airspeed;
        RectangleF ImageRectangleF;
        Matrix rotateMatrix;


        /*Constructor*/
        public asIndicator(System.Windows.Forms.PictureBox panel1)
        {
            indicatorBox = panel1;
            /*Gösterge için gerekli image dosyalarını al*/
            back = Image.FromFile("images\\asback.gif");
            igne = Image.FromFile("images\\igne2.gif");
            airspeed = 30;

            OranWidth = (float)(indicatorBox.Width / 400.0);
            OranHeight = (float)(indicatorBox.Height / 400.0);

            ImageRectangleF.Width = Convert.ToSingle(ImageWidth = back.Width * OranWidth);
            ImageRectangleF.Height = Convert.ToSingle(back.Height * OranHeight);

            ImageRectangleF.X = Convert.ToSingle(-((ImageRectangleF.Width - indicatorBox.Width) / 2.0));
            ImageRectangleF.Y = Convert.ToSingle(-((ImageRectangleF.Height - indicatorBox.Height) / 2.0));

            rotateMatrix = new Matrix();
            rotateMatrix.RotateAt(0, new PointF((float)(indicatorBox.Width / 2.0), (float)(indicatorBox.Height / 2.0)));


        }

        /* PaintBox Olayına yazılacak fonksiyon*/
        public void Paint_Olayi(System.Windows.Forms.PaintEventArgs e)
        {

            if (airspeed < 30)
            {
                airspeed = 30;
            }

            if (airspeed > 170)
            {
                airspeed = 170;
            }

            rotateMatrix.RotateAt((float)((airspeed - 30.0) * 2.25), new PointF((float)(indicatorBox.Width / 2.0), (float)(indicatorBox.Height / 2.0))); //back için
            e.Graphics.DrawImage(back, ImageRectangleF.X, ImageRectangleF.Y, ImageRectangleF.Width, ImageRectangleF.Height);
            e.Graphics.Transform = rotateMatrix;
            e.Graphics.DrawImage(igne, ImageRectangleF.X, ImageRectangleF.Y, ImageRectangleF.Width, ImageRectangleF.Height);

            rotateMatrix.RotateAt((float)(-((airspeed - 30.0) * 2.25)), new PointF((float)(indicatorBox.Width / 2.0), (float)(indicatorBox.Height / 2.0))); //back için


        }

        /*yeni değerlerden sonra göstergeti tazele*/
        public void Refresh()
        {
            indicatorBox.Refresh();
        }

    }
}
