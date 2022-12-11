using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Label[,] field;
        Shape shape;
        int fieldWeight, fieldHight;
        public Form1()
        {
            InitializeComponent();
            fieldWeight = 20;
            fieldHight = 40;
            createField(fieldWeight, fieldHight);
            timer1.Interval = 300;
            shape = new Shape(fieldWeight / 2);
            timer1.Enabled = true;
        }
        private void createField(int x, int y)
        {
            lblScore = new Label();
            lblScore.Text = "Score: 0";
            lblScore.Location = new Point(20, 10);
            lblScore.BackColor = Color.Black;
            lblScore.ForeColor = Color.White;
            lblScore.AutoSize = true;
            this.Controls.Add(lblScore);

            this.BackColor = Color.Black;
            field = new Label[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    field[i, j] = new Label();
                    field[i, j].Width = 15;
                    field[i, j].Height = 15;
                    field[i, j].Location = new Point(10 + 16 * i, 30 + 16 * j);
                    field[i, j].BackColor = Color.White;
                    this.Controls.Add(field[i, j]);
                }
            }
            this.Width = 35 + x * 16;
            this.Height = 80 + y * 16;
        }

        private Shape getNewShape()
        {
            Shape shape = new Shape(fieldWeight / 2);
            return shape;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && shape.getX() != 0)
            {
                clearTrace(shape);
                shape.moveLeft();
                drawShapeOnField(shape);
            }
            if (e.KeyCode == Keys.Right && shape.getX() != (fieldWeight - shape.getMatrix().GetLength(0)))
            {
                //bool isPossible = true;
                //bool[,] temp = shape.getMatrix();
                //for (int i = 0; i < shape.getMatrix().GetLength(1); i++)
                //    if (temp[i,shape.getMatrix().GetLength(0) - 1] && field[shape.getX() + i, shape.getY() + j + 1].BackColor != Color.White)
                clearTrace(shape);
                shape.moveRight();
                drawShapeOnField(shape);
            }
            if (e.KeyCode == Keys.Up)
            {
                clearTrace(shape);
                shape.turnShape();
                drawShapeOnField(shape);
            }
            if (e.KeyCode == Keys.Down)
            {
                timer1.Interval = 30;
            }
        }

        private void drawShapeOnField(Shape shape)
        {
            if ((shape.getX() + shape.getMatrix().GetLength(0)) > fieldWeight)
            {
                //проверяется, выходит ли за границы поля фигура после поворота. Если да, то смещается влево до тех пор,
                // пока не перестанет выходит за границы поля
                shape.moveLeft();
                drawShapeOnField(shape);
            }
                
            //bool[,] temp = new bool[2, 2];
            bool[,] temp = shape.getMatrix();
            for (int i = 0; i < shape.getMatrix().GetLength(0); i++)
                for (int j = 0; j < shape.getMatrix().GetLength(1); j++)
                {
                    if (temp[i, j])
                        field[shape.getX() + i, shape.getY() + j].BackColor = shape.getColor();
                }
        }

        private void clearTrace(Shape shape)
        {
            bool[,] temp = shape.getMatrix();
            for (int i = 0; i < shape.getMatrix().GetLength(0); i++)
                for (int j = 0; j < shape.getMatrix().GetLength(1); j++)
                    if (temp[i,j])
                        field[shape.getX() + i, shape.getY() + j].BackColor = Color.White;
        }

        private bool checkDown()
        {
            bool[,] temp = new bool[shape.getMatrix().GetLength(0), shape.getMatrix().GetLength(1)];
            temp = shape.getMatrix();
            //Console.WriteLine($"{field[shape.getX(), shape.getY() + 1].BackColor} {shape.getY()}");
            if (shape.getY() > (field.GetLength(1) - shape.getMatrix().GetLength(1) - 1))
                return false;
            Console.WriteLine(field.GetLength(1) - shape.getMatrix().GetLength(1) - 1);

            for (int i = 0; i < temp.GetLength(0); i++)
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    Console.WriteLine(shape.getY() + 1);
                    if (temp[i, j] && field[shape.getX() + i, shape.getY() + j + 1].BackColor != Color.White)
                        return false;
                }
            return true;
        }

        private bool checkLine(int y)
        {
            bool temp = true;
            for (int i = 0; i < fieldWeight; i++)
                temp = temp & (field[i, y].BackColor != Color.White);
            if (temp)
            {
                //Console.WriteLine(y);
                addScore();
                for (int i = 0; i < fieldWeight; i++)
                    field[i, y].BackColor = Color.White;
                for (int j = y; j > 0; j--)
                    for (int i = 0; i < fieldWeight; i++)
                        field[i, j].BackColor = field[i, j - 1].BackColor;
                return true;
            }
            return false;
                
        }

        private void checkLines()
        {
            for (int i = fieldHight - 1; i > 5; i--)
                if (checkLine(i))
                    i++;
        }

        private void addScore()
        {
            string[] temp = lblScore.Text.Split(' ');
            lblScore.Text = "Score: " + (Convert.ToInt32(temp[1]) + fieldWeight).ToString();
        }

        private bool checkGameOver()
        {
            for (int i = 0; i < shape.getMatrix().GetLength(0); i++)
                for (int j = 0; j < shape.getMatrix().GetLength(1); j++)
                {
                    if (field[shape.getX() + i, shape.getY() + j].BackColor != Color.White)
                        return true;
                }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            clearTrace(shape);
            if (checkDown())
            {
                shape.moveDown();
                drawShapeOnField(shape);
            }
            else
            {
                drawShapeOnField(shape);
                checkLines();
                shape = new Shape(fieldWeight / 2);
                if (checkGameOver())
                {
                    timer1.Enabled = false;
                    MessageBox.Show("LOL, YOU ARE LOOSER");
                }
                timer1.Interval = 300;
            }
        }
    }
}
