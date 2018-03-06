using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Tez_v1
{
    class attIndicator
    {
        /*RollPitch Indicator Variables */
        PictureBox indicatorBox;
        Image back, front, scala, tirnak, cerceve;
        Int32 Xkoor, Ykoor;
        Double OranWidth, OranHeight, ImageWidth, ImageHeight;
        public Single pitch, roll;
        RectangleF ImageRectangleF;
        Matrix rotateMatrix;
        Matrix rotateMatrix2;

        /*Constructor*/
        public attIndicator(System.Windows.Forms.PictureBox panel1)
        {
            indicatorBox = panel1;
            /*Gösterge için gerekli image dosyalarını al*/
            back = Image.FromFile("images\\back.gif");
            front = Image.FromFile("images\\front.gif");
            scala = Image.FromFile("images\\scala.gif");
            tirnak = Image.FromFile("images\\tirnak.gif");
            cerceve = Image.FromFile("images\\cerceve.gif");
            pitch = 0;
            roll = 0;

            OranWidth = indicatorBox.Width / 280.0;
            OranHeight = indicatorBox.Height / 280.0;

            ImageRectangleF.Width = Convert.ToSingle(back.Width * OranWidth);
            ImageRectangleF.Height = Convert.ToSingle(back.Height * OranHeight);

            ImageRectangleF.X = Convert.ToSingle(-((ImageRectangleF.Width - indicatorBox.Width) / 2.0));
            ImageRectangleF.Y = Convert.ToSingle(-((ImageRectangleF.Height - indicatorBox.Height) / 2.0));

            rotateMatrix = new Matrix();
            rotateMatrix2 = new Matrix();

        }

        /* PaintBox Olayına yazılacak fonksiyon*/
        public void Paint_Olayi(System.Windows.Forms.PaintEventArgs e)
        {
            rotateMatrix.RotateAt((float)(-roll), new PointF((float)(indicatorBox.Width / 2.0), (float)(indicatorBox.Height / 2.0))); //back için
            rotateMatrix2.RotateAt(0.0F, new PointF((float)(indicatorBox.Width / 2.0), (float)(indicatorBox.Height / 2.0)));

            e.Graphics.Transform = rotateMatrix;
            e.Graphics.DrawImage(back, ImageRectangleF.X, Convert.ToSingle(ImageRectangleF.Y + OranHeight * 5 * pitch), ImageRectangleF.Width, ImageRectangleF.Height);
            e.Graphics.DrawImage(tirnak, ImageRectangleF.X, ImageRectangleF.Y, ImageRectangleF.Width, ImageRectangleF.Height);

            e.Graphics.Transform = rotateMatrix2;
            e.Graphics.DrawImage(front, ImageRectangleF.X, ImageRectangleF.Y, ImageRectangleF.Width, ImageRectangleF.Height);
            e.Graphics.DrawImage(scala, ImageRectangleF.X, ImageRectangleF.Y, ImageRectangleF.Width, ImageRectangleF.Height);

            /* Roll değerinin yeni gelen değere direkt gitmesini sağlar*/
            rotateMatrix.RotateAt( roll, new PointF((float)(indicatorBox.Width/2.0),(float)(indicatorBox.Height/2.0)) ); //back için
        }

        /*yeni değerlerden sonra göstergeti tazele*/
        public void Refresh()
        {

            if (pitch > 0)
            {
                indicatorBox.BackColor = Color.Blue;
            }
            else
            {
                indicatorBox.BackColor = Color.Brown;
            }
            indicatorBox.Refresh();
        }


    }
}
