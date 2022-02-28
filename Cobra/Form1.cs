using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cobra
{
    public partial class Form1 : Form
    {
        bool direita, esquerda, cima, baixo;
        List<PictureBox> snake = new List<PictureBox>();
        PictureBox comida = new PictureBox();
        int pontos = 0, record = 0;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    if (baixo)
                        break;
                    cima = true; baixo = false; esquerda = false; direita = false;
                    break;
                case Keys.Down:
                case Keys.S:
                    if (cima)
                        break;
                    cima = false; baixo = true; esquerda = false; direita = false;
                    break;
                case Keys.Left:
                case Keys.A:
                    if (direita)
                        break;
                    cima = false; baixo = false; esquerda = true; direita = false;
                    break;
                case Keys.Right:
                case Keys.D:
                    if (esquerda)
                        break;
                    cima = false; baixo = false; esquerda = false; direita = true;
                    break;
                case Keys.P:
                    timer1.Enabled = !timer1.Enabled;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    if (cima)
                        snake[i].Location = new Point(snake[i].Location.X, snake[i].Location.Y - 10);
                    if (baixo)
                        snake[i].Location = new Point(snake[i].Location.X, snake[i].Location.Y + 10);
                    if (esquerda)
                        snake[i].Location = new Point(snake[i].Location.X - 10, snake[i].Location.Y);
                    if (direita)
                        snake[i].Location = new Point(snake[i].Location.X + 10, snake[i].Location.Y);
                    if (snake[i].Location.X < -10)
                        snake[i].Location = new Point((this.Width / 10) * 10, snake[i].Location.Y);
                    if (snake[i].Location.X > this.Width)
                        snake[i].Location = new Point(0, snake[i].Location.Y);
                    if (snake[i].Location.Y < -10)
                        snake[i].Location = new Point(snake[i].Location.Y, (this.Height / 10) * 10);
                    if (snake[i].Location.Y > this.Height)
                        snake[i].Location = new Point(snake[i].Location.Y, 0);
                    for (int j = snake.Count - 1; j >= 1; j--)
                    {
                        if (snake[0].Location.X == snake[j].Location.X && snake[0].Location.Y == snake[j].Location.Y)
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Terminou!!");
                            pontos = 0;
                            for (int k = snake.Count - 1; k >= 1; k--)
                            {
                                this.Controls.Remove(snake[k]);
                                snake.Remove(snake[k]);
                            }
                            timer1.Enabled = true;
                            break;
                        }
                    }
                }
                else
                {
                    snake[i].Location = new Point(snake[i - 1].Location.X, snake[i - 1].Location.Y);
                }
                if (snake[0].Location.X == comida.Location.X && snake[0].Location.Y == comida.Location.Y)
                {
                    Comida();
                    pontos += 10;
                    if (pontos > record)
                        record = pontos;
                    PictureBox corpo = new PictureBox();
                    corpo.Location = new Point(snake[snake.Count - 1].Location.X, snake[snake.Count - 1].Location.Y);
                    corpo.BackColor = Color.Blue;
                    corpo.Size = new Size(10, 10);
                    snake.Add(corpo);
                    this.Controls.Add(corpo);
                    lblPontos.Text = "Pontos: " + pontos + " | Record: " + record;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PictureBox head = new PictureBox();
            head.Location = new Point(100, 100);
            head.Size = new Size(10, 10);
            head.BackColor = Color.Red;
            this.Controls.Add(head);
            snake.Add(head);
            Comida();
        }

        void Comida()
        {
            Random rnd = new Random();
            comida.BackColor = Color.Green;
            comida.Size = new Size(10, 10);
            comida.Location = new Point((rnd.Next(1, this.Width / 10) * 10), (rnd.Next(30, this.Height / 10) * 10));
            this.Controls.Add(comida);
        }
    }
}
