using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;
        Graphics g;
        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);

        int elements;
        int size;
        int wersja;
        int epoki;

        List<Boolean> beginTab;
        List<bool> nextTab;

        int lNeighbour;
        int rNeighbour;

        public Form1()
        {
            InitializeComponent();

            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;
            g = Graphics.FromImage(DrawArea);

            comboBox1.Items.Add(30);
            comboBox1.Items.Add(60);
            comboBox1.Items.Add(90);
            comboBox1.Items.Add(120);
            comboBox1.Items.Add(255);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            g.Clear(Color.Transparent);
            beginTab = new List<bool>();
            nextTab = new List<bool>();
            if (!(string.IsNullOrWhiteSpace(textBox1.Text) && string.IsNullOrWhiteSpace(comboBox1.Text)))
            {
                elements = int.Parse(textBox1.Text);
                epoki = int.Parse(textBox2.Text);
                wersja = int.Parse(comboBox1.Text);

                string wersjaBin = "00000000" + Convert.ToString(wersja, 2);
                size = pictureBox1.Size.Width / elements;

                beginTab = Enumerable.Repeat(false, elements).ToList();
                nextTab = Enumerable.Repeat(false, elements).ToList();

                beginTab[(int)elements / 2] = true;

                for (int k = 0; k < elements; k++)
                {
                    if (beginTab[k] == true)
                        g.FillRectangle(pinkBrush, size * k, 0, size, size);
                }

                for (int j = 0; j < epoki; j++)
                {
                    for (int i = 0; i < elements; i++)
                    {
                        if (i == 0)
                        {
                            lNeighbour = elements - 1;
                            rNeighbour = 1;
                        }
                        else if (i == elements - 1)
                        {
                            lNeighbour = elements - 2;
                            rNeighbour = 0;
                        }
                        else
                        {
                            lNeighbour = i - 1;
                            rNeighbour = i + 1;
                        }

                        string trzyBit = "";
                        trzyBit += beginTab[lNeighbour] ? '1' : '0';
                        trzyBit += beginTab[i] ? '1' : '0';
                        trzyBit += beginTab[rNeighbour] ? '1' : '0';

                        int pozycja = Convert.ToInt32(trzyBit, 2);

                        if (wersjaBin[wersjaBin.Length - pozycja - 1] == '1')
                        {
                            g.FillRectangle(pinkBrush, size * i, size * (j + 1), size, size);
                            nextTab[i] = true;
                        }

                    }
                    for (int r = 0; r < elements; r++)
                    {
                        beginTab[r] = nextTab[r];
                        nextTab[r] = false;
                    }
                    pictureBox1.Image = DrawArea;
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
