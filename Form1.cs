using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1001_графический_редактор
{
    public partial class Form1 : Form
    {
        //Переменные//
        string mode; 
        bool isErasing = false; 
        bool fill = false; 
        bool border = false; 
        int x = 0, y = 0, x1 = 0, y1 = 0; 
        Point[] p; 
        Pen pen = new Pen(Color.Black, 1f); 
        Brush brush= new SolidBrush(Color.Black); 
        Bitmap bitmap; 
        Graphics canvas;

        public Form1()
        {
            InitializeComponent();
            // Автоматически выбирается режим рисования карандашом и задаются наконечники карандаша
            mode = "pen";
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height); 

            //Фильтры для сохранения и загузки изоражений
            saveFileDialog1.Filter = "Изображения (*.png)|*.png|Все файлы (*.*)|*.*";
            openFileDialog1.Filter = "Изображения (*.png)|*.png|Все файлы (*.*)|*.*";
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            label6.Text = $"X:{e.X}, Y:{e.Y}";
            

            if(mode == "pen") 
            {
                if (e.Button == MouseButtons.Left) 
                {
                    if (isErasing) pen.Color = pictureBox4.BackColor; 
                    else pen.Color = pictureBox3.BackColor; 
                    canvas = Graphics.FromImage(bitmap); 
                    canvas.DrawLine(pen, x, y, e.X, e.Y); 
                    pictureBox1.Image = bitmap;
                    
                }
                x = e.X; y = e.Y; 
            }
            x1 = e.X; y1 = e.Y; 
            pictureBox1.Invalidate(); 
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            brush = new SolidBrush(pictureBox3.BackColor);
            if (mode == "line")
            {
                canvas = Graphics.FromImage(bitmap); 
                canvas.DrawLine(pen, x, y, x1, y1); 
                pictureBox1.Image = bitmap; 
            }
            if(mode == "rectangle") 
            {
                canvas = Graphics.FromImage(bitmap); 
                if (fill) 
                {              
                    canvas.FillRectangle(brush, x, y, x1 - x, y1 - y); 
                    if (border) 
                    {                        
                        canvas.DrawRectangle(new Pen(pictureBox2.BackColor, trackBar1.Value), x, y, x1 - x, y1 - y); 
                    }
                }
                else
                {
                    canvas.DrawRectangle(pen, x, y, x1 - x, y1 - y); 
                }
                pictureBox1.Image = bitmap; 
            }
            if(mode == "circle") 
            {
                canvas = Graphics.FromImage(bitmap); 
                if (fill) 
                {
                    canvas.FillEllipse(brush, x, y, x1 - x, y1 - y);
                    if (border) 
                    {

                        canvas.DrawEllipse(new Pen(pictureBox2.BackColor, trackBar1.Value), x, y, x1 - x, y1 - y);
                    }
                }
                else
                {
                    canvas.DrawEllipse(pen, x, y, x1 - x, y1 - y);
                }
                pictureBox1.Image = bitmap;
            }
            if(mode == "polygon") 
            {
                p = new Point[trackBar2.Value]; 
                setPoints(trackBar2.Value, x, y, x1, y1);
                canvas = Graphics.FromImage(bitmap);
                if (fill) 
                {
                    canvas.FillPolygon(brush, p);
                    if (border) 
                    {

                        canvas.DrawPolygon(new Pen(pictureBox2.BackColor, trackBar1.Value), p);
                    }
                }
                else
                {
                    canvas.DrawPolygon(pen, p);
                   
                } 
                pictureBox1.Image = bitmap;
            }
        }

        //Толщина//
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
            pen.Width = trackBar1.Value;
        }

        //Палитра//

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox3.BackColor = pen.Color = colorDialog1.Color;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox4.BackColor = pen.Color = colorDialog1.Color;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox5.BackColor;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox6.BackColor;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox7.BackColor;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox8.BackColor;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox9.BackColor;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox10.BackColor;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pen.Color = pictureBox3.BackColor = pictureBox11.BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = "pen";
            isErasing = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = "pen";
            isErasing = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = "line";
            isErasing = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mode = "rectangle";
            isErasing = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mode = "circle";
            isErasing = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mode = "polygon";
            isErasing = false;
        }
        // Кол-во углов многоугольника
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar2.Value.ToString();
        }

        //Кнопки меню//

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
                pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                bitmap.Save(saveFileDialog1.FileName);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (fill)
            {
                fill = false;
            }
            else 
            {
                fill = true;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (border)
            {
                border = false;
            }
            else
            {
                border = true;
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox2.BackColor = colorDialog1.Color;
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);
            using (canvas = Graphics.FromImage(bitmap))
            {
                canvas.Clear(Color.White);
            }
            pictureBox1.Image = bitmap;
        }

        //Доп.Функции//

        public void setPoints(int n, int x, int y, int x1, int y1)
        {
            int R = (y1 - y) / 2; 
            for(int i = 0;i < n;i++)
            {
                p[i].X = ((x1 + x) / 2) + (int) (R * Math.Cos(2 * Math.PI * i/ n));
                p[i].Y = ((y1 + y) / 2) + (int)(R * Math.Sin(2 * Math.PI * i / n));
            }
        }
    }
}
