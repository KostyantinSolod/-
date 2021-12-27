using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace Zmeika
{
    public partial class Form1 : Form
    {
        static int sidelength = 40;
        Brush brushes = Brushes.Red ;
        Zmeya zmeya = new Zmeya();
        public Form1()
        {
            this.BackColor = Color.DarkGreen;
            InitializeComponent();
            Cell.Inicialization();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void AddGrafics()
        {
            int x, y,x1,xris;
            x = Convert.ToInt32((this.Width -sidelength * 10) / 2.0)+sidelength*10;
            y = Convert.ToInt32((sidelength * 8+10));
            x1= Convert.ToInt32((this.Width  - sidelength * 10) / 2.0)-sidelength;
            for (int i = 7; i >= 0; i--)
            {
                 int start, stop, step;
                if ( i % 2 == 0) { start = 1; stop = 11; step = 1; xris = x1; } 
                else { start = 10; stop = 0; step =- 1; xris = x; }
                for (int j = start;j!=stop ; j += step)
                {
                    GenerateCell(xris - sidelength * j*step*-1, y - sidelength * i);
                    if (Cell.pole[i * 10 + j] is Drabina) { GenerateDrabina(xris - sidelength * j * step * -1, y - sidelength * i, i * 10 + j, x1); }
                    else if (Cell.pole[i * 10 + j] is ZmeyaCell)
                    {
                        GenerateZmeya(i, j, xris, step, x, y, (Cell.pole[i * 10 + j] as ZmeyaCell).cords);   
                    }
                    
                    if (Zmeya.cords.Contains(i * 10 + j) && Zmeya.cords[Zmeya.cords.Count - 1]==i*10+j) { GenerateStop(xris - sidelength * j * step * -1, y - sidelength * i,Color.Blue);
                         }
                }
            }
        }
        public void GenerateStop(int x,int y,Color color)
        {
            Graphics graphics = this.CreateGraphics();
            Pen pen = new Pen(color);
            graphics.DrawEllipse(pen,new RectangleF(x-sidelength/4,y-sidelength/4,sidelength/2,sidelength/2));
            Zmeya.point = new Point(x, y);
        }
        public void GenerateZmeya(int i, int j, int xris, int step, int x, int y,int[] array)
        {
            int nom = i * 10 + j;
            if (array.Length == 4)
            {
                int xcor = 0, ycor = 0;
                int left = 0;
                int up = 0;
                int xmnoz = 1;
                up = ((array[3] - 1) / 10).CompareTo((array[2] - 1) / 10);
                if (up == 0) { left = (array[2]).CompareTo(array[3]); }
                if ((array[3] - 1) / 10 % 2 == 1) { left *= -1; xmnoz = -1; } else { xmnoz = 1; }
                DrawZmeya(xris - sidelength * (j + xcor * xmnoz) * step * -1, y - sidelength * (i + ycor), Color.Red, up, left);
                xcor += left; ycor += up;
                for (int q = 2; q > 0; q--)
                {
                    up = ((array[q] - 1) / 10).CompareTo((array[q + 1] - 1) / 10);
                    if (up == 0) { left = (array[q + 1]).CompareTo(array[q]); } 
                    if ((array[q] - 1) / 10 % 2 == 1) { left *= -1; xmnoz = -1; } else { xmnoz = 1; }
                    DrawZmeya(xris - sidelength * (j + xcor * xmnoz) * step * -1, y - sidelength * (i + ycor), Color.Red, up, left);
                    left = 0;
                    up = ((array[q] - 1) / 10).CompareTo((array[q - 1] - 1) / 10);
                    if (up == 0) { left = (array[q - 1]).CompareTo(array[q]); }
                    if ((array[q] - 1) / 10 % 2 == 1) { left *= -1; xmnoz = -1; } else { xmnoz = 1; }
                    DrawZmeya(xris - sidelength * (j + xcor * xmnoz) * step * -1, y - sidelength * (i + ycor), Color.Red, up, left);
                    xcor += left; ycor += up;
                }
                up = ((array[0] - 1) / 10).CompareTo((array[1] - 1) / 10);
                if (up == 0) { left = (array[1]).CompareTo(array[0]); }
                DrawZmeya(xris - sidelength * (j + xcor * xmnoz) * step * -1, y - sidelength * (i + ycor), Color.Red, 1, 0);

            }
        }
            private void GenerateCell(int x, int y)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.DrawPolygon(Pens.Gray, GetCurrPoints(x, y,sidelength));

        }
        private void DrawZmeya(int x,int y,Color color,int up, int left)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.FillPolygon(brushes, GetCurrPoints(x-5, y-5,10));
            graphics.FillPolygon(brushes, GetCurrPoints(x, y,up,left));

        }
        private void GenerateDrabina(int x, int y,int nom, int x1) 
        {
            Pen pen = new Pen(Color.SaddleBrown);
            Graphics graphics = this.CreateGraphics();
            graphics.DrawLine(pen, new Point(x - sidelength/2 + 5, y - sidelength/2), new Point(x - sidelength/2 + 5, y + sidelength/2));
            graphics.DrawLine(pen, new Point(x + sidelength/2 - 5, y - sidelength/2), new Point(x + sidelength/2 - 5, y + sidelength/2));
            int[] array = (Cell.pole[nom] as Drabina).cords;
            int col = 0;
            foreach (var item in array)
            {
                col += 1;
                int i = item / 10 - 1;
                int step = i % 2 == 0 ? 1 : -1;
                graphics.DrawLine(pen, new Point(x - sidelength / 2 + 5, y - sidelength / 2-col*sidelength+2), new Point(x - sidelength / 2 + 5, y + sidelength / 2-col*sidelength+2));
                graphics.DrawLine(pen, new Point(x + sidelength / 2 - 5, y - sidelength / 2-col*sidelength+2), new Point(x + sidelength / 2 - 5, y + sidelength / 2-col*sidelength+2));
            }
            int y1 = y - sidelength / 2-(col)*sidelength;
            for (int j = 10; j < sidelength * (col + 1) - 5; j += 5)
            {
                graphics.DrawLine(pen, new Point(x + sidelength / 2 - 5, y1 + j), new Point(x - sidelength / 2 + 5, y1 + j));
            }
            pen.Color = Color.DarkGreen;
            pen.Width = 3;
            graphics.DrawLine(pen, new Point(x + sidelength / 2-3, y - sidelength / 2 - col * sidelength+3), new Point(x - sidelength / 2 + 5, y - sidelength / 2 - col * sidelength+3));
            

        }
        private System.Drawing.Point[] GetCurrPoints(int x, int y,int sidelength)
        {
            return new System.Drawing.Point[4]
            {
                new System.Drawing.Point(Convert.ToInt32(x-sidelength/2),  Convert.ToInt32(y-sidelength/2)),
                new System.Drawing.Point(Convert.ToInt32(x - sidelength/2), Convert.ToInt32(y + sidelength/2)),
                new System.Drawing.Point(Convert.ToInt32(x + sidelength/2),  Convert.ToInt32(y+sidelength/2)),
                new System.Drawing.Point(Convert.ToInt32(x + sidelength/2),  Convert.ToInt32(y - sidelength/2)),
            };
        }
        private System.Drawing.Point[] GetCurrPoints(int x, int y,int up, int left)
        {
            int xcor = sidelength/4,ycor=sidelength/4;

            if (up==1) { ycor = sidelength / 2; }
            else if (up == -1) { ycor = -sidelength / 2; }
            if (left == 1) { xcor = sidelength / 2; }
            else if (left == -1) { xcor = -sidelength / 2; }
            return new System.Drawing.Point[4]
            {
                new System.Drawing.Point(Convert.ToInt32(x-xcor),  Convert.ToInt32(y-ycor)),
                new System.Drawing.Point(Convert.ToInt32(x - xcor), Convert.ToInt32(y )),
                new System.Drawing.Point(Convert.ToInt32(x ),  Convert.ToInt32(y)),
                new System.Drawing.Point(Convert.ToInt32(x ),  Convert.ToInt32(y - ycor)),
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Graphics graphics = this.CreateGraphics();
            graphics.Clear(Color.DarkGreen);
            zmeya.ryh();
            textBox1.Text = Zmeya.brosoc.ToString();
            textBox1.Refresh();
            for (int i = 0; i < Zmeya.cords.Count; )
            {
                GenerateStop(Zmeya.point.X, Zmeya.point.Y, this.BackColor);
                AddGrafics();
                System.Threading.Thread.Sleep(100);
                Zmeya.cords.Remove(Zmeya.cords[Zmeya.cords.Count-1]);
            }
            Zmeya.cords.Clear();
        }
    }
}
