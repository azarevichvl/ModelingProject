using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCourseWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //initialization();
        }

        private MyDraw md;

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(button1, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double cilRad, cilVis, pirRad;
            int n;

            cilRad = double.Parse(fcilRad.Text);
            cilVis = double.Parse(fcilVis.Text);
            pirRad = double.Parse(fpirRad.Text);
            n = (int)numericUpDown1.Value;

            md = new MyDraw(cilRad, cilVis, pirRad, n, 
                pDraw.CreateGraphics(),
                new PointF((float)(pDraw.Size.Width / 2),
                (float)(pDraw.Size.Height / 2)));

            md.Draw();//Draw

        }

        public void initialization(object sender, EventArgs e)
        {
            double cilRad, cilVis, pirRad;
            int n;

            cilRad = double.Parse(fcilRad.Text);
            cilVis = double.Parse(fcilVis.Text);
            pirRad = double.Parse(fpirRad.Text);
            n = (int)numericUpDown1.Value;

            md = new MyDraw(cilRad, cilVis, pirRad, n,
                pDraw.CreateGraphics(),
                new PointF((float)(pDraw.Size.Width / 2),
                (float)(pDraw.Size.Height / 2)));

            md.Draw();//Draw

        }



        private void bMove_Click(object sender, EventArgs e)
        {
            md.MoveAll(
                int.Parse(tbMoveX.Text),
                int.Parse(tbMoveY.Text),
                int.Parse(tbMoveZ.Text));
        }

        private void bRotate_Click(object sender, EventArgs e)
        {
            md.RotateAll(
                int.Parse(tbRotX.Text),
                int.Parse(tbRotY.Text),
                int.Parse(tbRotZ.Text));
        }

        private void bScale_Click(object sender, EventArgs e)
        {
            md.ScaleAll(
                (double)nudScaleX.Value,
                (double)nudScaleY.Value,
                (double)nudScaleZ.Value);
        }

        private void drop_Click(object sender, EventArgs e)
        {
            double cilRad = 80, cilVis = 200, pirRad = 60;
            int n = 4;

            fcilRad.Text = cilRad.ToString();
            fcilVis.Text = cilVis.ToString();
            fpirRad.Text = pirRad.ToString();
            numericUpDown1.Value = n;
            tbRotX.Text = "15";
            tbRotY.Text = "15";
            tbRotZ.Text = "0";
            tbMoveX.Text = "0";
            tbMoveY.Text = "-15";
            tbMoveZ.Text = "0";
            nudScaleX.Value = 1.0M;
            nudScaleY.Value = 1.0M;
            nudScaleZ.Value = 1.0M;


            md = new MyDraw(cilRad, cilVis, pirRad, n,
                pDraw.CreateGraphics(),
                new PointF((float)(pDraw.Size.Width / 2),
                (float)(pDraw.Size.Height / 2)));

            md.Draw();//Draw
        }

        private void initialization()
        {

        }

        private void cbPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (cbPro.SelectedIndex)
            {
                case 0:
                    gbAks.Hide();
                    gbKos.Hide();
                    gbPer.Hide();
                    md.ProFrontAll();
                    break;
                case 1:
                    gbAks.Hide();
                    gbKos.Hide();
                    gbPer.Hide();
                    md.ProGorAll();
                    break;
                case 2:
                    gbAks.Hide();
                    gbKos.Hide();
                    gbPer.Hide();
                    md.ProProfAll();
                    break;
                case 3:
                    gbAks.Show();
                    gbKos.Hide();
                    gbPer.Hide();   
                    break;
                case 4:
                    
                    gbAks.Hide();
                    gbKos.Show();
                    gbPer.Hide();                 
                    break;
                case 5:
                    gbAks.Hide();
                    gbKos.Hide();
                    gbPer.Show();
                    break;

            }
        }

        private void bDrawKos_Click(object sender, EventArgs e)
        {
           md.ProKosAll(double.Parse(tbL.Text), double.Parse(tbAlfa.Text));
        }

        private void bDrawAks_Click(object sender, EventArgs e)
        {
           md.ProAksAll(double.Parse(tbFiA.Text), double.Parse(tbPsi.Text));
        }

        private void bDrawPer_Click(object sender, EventArgs e)
        {
           if (double.Parse(tbRo.Text) != 0) {
               md.ProPerAll(double.Parse(tbD.Text), double.Parse(tbTetta.Text), double.Parse(tbFiPer.Text), double.Parse(tbRo.Text));
           } else {
               md.ProPerAll(double.Parse(tbD.Text), double.Parse(tbTetta.Text), double.Parse(tbFiPer.Text), double.Parse(tbRo.Text) + 0.1);
           }
        }

        

    }
}
