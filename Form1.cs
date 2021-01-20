using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficacion
{
    public partial class Form1 : Form
    {

        List<Rectangulo> grafico = new List<Rectangulo>();
        Rectangulo obj;
        int x_1, x_2, y_1, y_2;
        private int aumento = 10;
        int W, H;
        int xCentro;
        int yCentro;
        String dx, dy;
        private Bitmap DrawArea;


        public Form1()
        {
            InitializeComponent();
            H = pictureBox1.Height;
            W = pictureBox1.Width;
            DrawArea = new Bitmap(W, H);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtx1.Text = "";
            txtx2.Text = "";
            txty1.Text = "";
            txty2.Text = "";
            d_x.Text = "X";
            d_y.Text = "Y";
            grafico.Clear();
            //Limpia el data GridView
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = null;
                dataGridView1.Rows[i].Cells[1].Value = null;
                dataGridView1.Rows.Clear();

            }
            Draw();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Draw();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            aumento = tbZoom.Value;
            Draw();
            foreach (Rectangulo item in grafico)
            {
                DrawPixel(item.GetX(), item.GetY(), xCentro, yCentro);
            }
        }



      

        private void calcularRecta_Click(object sender, EventArgs e)
        {
            if (txtx1.Text.Length != 0 || txtx2.Text.Length != 0 || txty1.Text.Length != 0 || txty2.Text.Length != 0)
            {
                //Calcular puntos 
                x_1 = int.Parse(txtx1.Text.ToString());
                x_2 = int.Parse(txtx2.Text.ToString());
                y_1 = int.Parse(txty1.Text.ToString());
                y_2 = int.Parse(txty2.Text.ToString());
                d_x.Text = Math.Abs(x_2 - x_1).ToString();
                d_y.Text = Math.Abs(y_2 - y_1).ToString();
                if ((x_2 - x_1) == 0 || (x_1 - x_2) == 0)
                {
                    for (int i = y_1; i <= y_2; i++)
                    {
                        //Añade una nueva columna
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = 0;
                        dataGridView1.Rows[n].Cells[1].Value = i;
                        DrawPixel(0, (int)i, xCentro, yCentro);
                        grafico.Add(obj = new Rectangulo(0, i));
                    }
                }
                else
                {
                    //Pendiente
                    double m = ((x_2 - x_1) / (y_2 - y_1));
                    if (x_1 > x_2)
                    {
                        for (int i = x_2; i <= x_1; i++)
                        {
                            //Añade una nueva columna
                            int n = dataGridView1.Rows.Add();
                            //Ecuacion de la recta
                            double y = (m * (i - x_1) + y_1);
                            //Asigna el valor a x
                            dataGridView1.Rows[+n].Cells[0].Value = i;
                            //Asigna el valor a y
                            dataGridView1.Rows[n].Cells[1].Value = y;
                            DrawPixel(i, (int)y, xCentro, yCentro);
                            grafico.Add(obj = new Rectangulo(i, (int)y));

                        }
                    }
                    else
                    {
                        for (int i = x_1; i <= x_2; i++)
                        {
                            //Añade una nueva columna
                            int n = dataGridView1.Rows.Add();
                            //Ecuacion de la recta
                            double y = (m * (i - x_1) + y_1);
                            //Asigna el valor a x
                            dataGridView1.Rows[+n].Cells[0].Value = i;
                            //Asigna el valor a y
                            dataGridView1.Rows[n].Cells[1].Value = y;
                            DrawPixel(i, (int)y, xCentro, yCentro);
                            grafico.Add(obj = new Rectangulo(i, (int)y));

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("Aegurese de rellenar los datos");
            }

        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void Draw()
        {
            Graphics g = Graphics.FromImage(DrawArea);
            g.Clear(Color.White);
            pictureBox1.Image = DrawArea;
            xCentro = W / 2;
            yCentro = H / 2;
          
            for (int i = aumento / 2; i <= xCentro; i += aumento)
            {
                g.DrawLine(Pens.Silver, xCentro - i, 0, xCentro - i, H);
                g.DrawLine(Pens.Silver, xCentro + i, 0, xCentro + i, H);

            }
            //Cuadricula en y
            for (int i = aumento / 2; i <= yCentro; i += aumento)
            {
                g.DrawLine(Pens.Silver, 0, yCentro - i, W, yCentro - i);
                g.DrawLine(Pens.Silver, 0, yCentro + i, W, yCentro + i);

            }


            //Linea horizontal
            Pen pen = new Pen(Color.Black, 1);
            g.DrawLine(pen, xCentro, 0, xCentro, H);
            //Linea Vertical
            g.DrawLine(pen, 0, yCentro, W, yCentro);
            g.Dispose();
        }

      
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        //contador minimizar,maximizar
        int v = 0;
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (v % 2 == 0)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
            v++;
        }


        private void DrawPixel(int x, int y, int xCentro, int yCentro)
        {
            Graphics g = Graphics.FromImage(DrawArea);
            SolidBrush brush = new SolidBrush(Color.DarkCyan);
            pictureBox1.Image = DrawArea;
            g.FillRectangle(brush, new Rectangle((xCentro + x * aumento + 1) - aumento / 2, (yCentro - y * aumento + 1) - aumento / 2, aumento - 1, aumento - 1));
            // g.Dispose();
        }



    }
}
