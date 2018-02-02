using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyCourseWork
{
    struct Point3
    {
        public Point3(double inX, double inY, double inZ)
        {
            X = inX;
            Y = inY;
            Z = inZ;
        }

        public double X;
        public double Y;
        public double Z;
    }

    struct Rebro
    {
        public Point3 A;
        public Point3 B;
    }

    class Gran
    {
        public Gran(Color cf)
        {
            Points = new List<Point3>();
            ColorFill = cf;
        }
        public List<Point3> Points;
        public Color Color;
        public Color ColorFill;
    }
    class MyDraw
    {
        private Point3[] upperCircle, lowerCircle, pPir = new Point3[100];
        private Rebro[] verticalRebra, horizontalRebraUpper, horizontalRebraLower, rPir = new Rebro[200];
        private Gran[] gCil, gPir, gTemp = new Gran[100];

        private Point3[] upperCircleBackUp, lowerCircleBackUp, pPirBackUp = new Point3[200];
        private Rebro[] verticalRebraBackUp, horizontalRebraUpperBackUp, horizontalRebraLowerBackUp = new Rebro[400];

        PointF Zero = new PointF(250, 250);
        private Graphics gr;

        double cilRad, cilVis, pirRad;
        double alpha;
        int n;

         
        public MyDraw(double inCirRad, double inCilVis, double inPirRad, int inN, Graphics inGr, PointF inZero)
        {
            Zero = inZero;
            cilRad = inCirRad;
            cilVis = inCilVis;
            pirRad = inPirRad;

            n = inN; // Степень аппроксимирования
            alpha = ((360 / n)* Math.PI) / 180;
            
            gr = inGr;
            //temps
            int i;

            //Prpbably cleaning variables

            upperCircle = new Point3[n];
            lowerCircle = new Point3[n];

            upperCircleBackUp = new Point3[n];
            lowerCircleBackUp = new Point3[n];

            verticalRebra = new Rebro[n];
            horizontalRebraUpper = new Rebro[n+2];
            horizontalRebraLower = new Rebro[n+2];

            verticalRebraBackUp = new Rebro[n];
            horizontalRebraLowerBackUp = new Rebro[n + 2];
            horizontalRebraUpperBackUp = new Rebro[n + 2];

            gCil = new Gran[n+2];
            gPir = new Gran[4];
            gTemp = new Gran[1];

            //Coords Cilindra
            for (i = 0; i < gCil.Length; i++)
                gCil[i] = new Gran(Color.Blue);
            for (i = 0; i < gPir.Length; i++)
                gPir[i] = new Gran(Color.Blue);

            //Points
            for (i = 0; i < n; i++)
            {
                upperCircle[i].X = cilRad * Math.Cos(alpha * i);
                upperCircle[i].Y = cilVis;
                upperCircle[i].Z = cilRad * Math.Sin(alpha * i);

                lowerCircle[i].X = cilRad * Math.Cos(alpha * i);
                lowerCircle[i].Y = 0;
                lowerCircle[i].Z = cilRad * Math.Sin(alpha * i);
            }
            //may be mistake in calculation of pir's coords lower gran
            pPir[0].X = 0; pPir[0].Y = cilVis; pPir[0].Z = 0;
            pPir[1].X = pirRad; pPir[1].Y = 0; pPir[1].Z = 0;
            pPir[2].X = ((double)pirRad/(-3)); pPir[2].Y = 0; pPir[2].Z = (-pirRad*Math.Sqrt(3))/2;
            pPir[3].X = (double)pirRad/(-3); pPir[3].Y = 0; pPir[3].Z = ( pirRad*Math.Sqrt(3))/2;
            
            SetRebra();
            SetGrani();
        }

        private void SetRebra()
        {
            int i;
            for (i = 0; i < n; i++)
            {
                verticalRebra[i].A = upperCircle[i]; verticalRebra[i].B = lowerCircle[i];          
            }
            for (i = 0; i < n-1; i++)
            {
                horizontalRebraUpper[i].A = upperCircle[i]; horizontalRebraUpper[i].B = upperCircle[i+1];
                horizontalRebraLower[i].A = lowerCircle[i]; horizontalRebraLower[i].B = lowerCircle[i+1];
            }
            horizontalRebraUpper[n-1].A = upperCircle[0]; horizontalRebraUpper[n-1].B = upperCircle[n-1];
            horizontalRebraLower[n-1].A = lowerCircle[0]; horizontalRebraLower[n-1].B = lowerCircle[n-1];

            rPir[0].A = pPir[0]; rPir[0].B = pPir[1];
            rPir[1].A = pPir[0]; rPir[1].B = pPir[2];
            rPir[2].A = pPir[0]; rPir[2].B = pPir[3];
            rPir[3].A = pPir[1]; rPir[0].B = pPir[2];
            rPir[4].A = pPir[2]; rPir[0].B = pPir[3];
            rPir[5].A = pPir[3]; rPir[0].B = pPir[1];

        }

        private void SetGrani()
        {
            int i;
            //For piramida
            for (i = 0; i < gPir.Length; i++)
                gPir[i].Points.Clear();

            gPir[0].Points.Add(pPir[0]); gPir[0].Points.Add(pPir[1]);
            gPir[0].Points.Add(pPir[2]);

            gPir[1].Points.Add(pPir[0]); gPir[1].Points.Add(pPir[2]);
            gPir[1].Points.Add(pPir[3]);

            gPir[2].Points.Add(pPir[0]); gPir[2].Points.Add(pPir[1]);
            gPir[2].Points.Add(pPir[3]);

            gPir[3].Points.Add(pPir[1]); gPir[3].Points.Add(pPir[2]);
            gPir[3].Points.Add(pPir[3]);

            //For cilindr
            for (i = 0; i < gCil.Length; i++)
                gCil[i].Points.Clear();

            for (i = 0; i < n - 1; i++)
            {
                gCil[i].Points.Add(upperCircle[i]); gCil[i].Points.Add(upperCircle[i+1]);
                gCil[i].Points.Add(lowerCircle[i + 1]); gCil[i].Points.Add(lowerCircle[i]);
            }
            gCil[n - 1].Points.Add(upperCircle[n - 1]); gCil[n-1].Points.Add(upperCircle[0]);
            gCil[n - 1].Points.Add(lowerCircle[0]); gCil[n - 1].Points.Add(lowerCircle[n - 1]);
            //Add horizontal grans:( dot fogot to add!!!! hello

            for (i = 0; i <n; i++)
            {
                gCil[n].Points.Add(upperCircle[i]);
            }

            //lower circle begin

            gCil[n + 1].Points.Add(lowerCircle[0]);
            gCil[n + 1].Points.Add(pPir[1]);
            gCil[n + 1].Points.Add(pPir[2]);
            gCil[n + 1].Points.Add(pPir[3]);
            gCil[n + 1].Points.Add(pPir[1]);


            for (i = 0; i < n; i++)
            {
                gCil[n].Points.Add(upperCircle[i]);
            }


            //lower circle end
        }

        public void Draw()
        {
            Color col = Color.White;
            gr.Clear(col);
            col = Color.Black;
            //Needs to add some "if" about visuable gran's
            DrawObject(gPir, col);
            DrawObject(gCil, col);
            /*if (lowerCircle.Length > 0)
            {
                gTemp[0].Points.Add(lowerCircle[0]);
                gTemp[0].Points.Add(pPir[1]);
                gTemp[0].Points.Add(lowerCircle[0]);

                col = Color.White;
                DrawObject(gTemp, col);
            }*/
        }

        private void DrawObject(Gran[] obj, Color col)
        {
            foreach (Gran gran in obj)
            {
                                                                                                                                                                            #region commedted didn't finish and lagged
                /*if (isInvDel)
                {
                    gran.Color = isProj ? gran.Color : gran.ColorFill;
                    if (IsVisible(gran))
                    {
                        List<PointF> lP = new List<PointF>();
                        foreach (Point3 point3 in gran.Points)
                        {
                            lP.Add(new PointF(Zero.X + (float)point3.X,
                                              Zero.Y - (float)point3.Y));
                        }
                        if (isColor)
                        {
                            if (isLight)
                                gr.FillPolygon(new SolidBrush(gran.Color), lP.ToArray());
                            else
                                gr.FillPolygon(new SolidBrush(gran.ColorFill), lP.ToArray());
                        }
                        else
                            gr.FillPolygon(new SolidBrush(Color.White), lP.ToArray());
                        if (!isColor || !isLight)
                            gr.DrawPolygon(new Pen(Color.Black, 1), lP.ToArray());
                    }
                }
                else*/
            #endregion
                //{
                    List<PointF> lP = new List<PointF>();
                    foreach (Point3 point3 in gran.Points)
                    {
                        lP.Add(new PointF(Zero.X + (float)point3.X,
                                          Zero.Y - (float)point3.Y));
                    }
                    gr.DrawPolygon(new Pen(col, 1), lP.ToArray());
                //}
            }
        }


        #region Peremeschenie

        public void MoveAll(int disX, int disY, int disZ)
        {
            int i;
            Point3[] tmpP;
            tmpP = Move(disX, disY, disZ, pPir);
            for (i = 0; i < 4; i++)
                pPir[i] = tmpP[i];
            

            tmpP = Move(disX, disY, disZ, upperCircle);
            for (i = 0; i < n; i++)
                upperCircle[i] = tmpP[i];

            tmpP = Move(disX, disY, disZ, lowerCircle);
            for (i = 0; i < n; i++)
                lowerCircle[i] = tmpP[i];
            SetRebra();
            SetGrani();
            
            Draw();
        }

        private Point3[] Move(int disX, int disY, int disZ, Point3[] point3s)
        {
            int i;
            Point3[] outMas = new Point3[point3s.Length];

            Matrix R = new Matrix(4, 4);
            R[0, 0] = 1; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
            R[1, 0] = 0; R[1, 1] = 1; R[1, 2] = 0; R[1, 3] = 0;
            R[2, 0] = 0; R[2, 1] = 0; R[2, 2] = 1; R[2, 3] = 0;
            R[3, 0] = disX; R[3, 1] = disY; R[3, 2] = disZ; R[3, 3] = 1;

            for (i = 0; i < point3s.Length; i++)
            {
                Matrix s = new Matrix(1, 4);
                s[0, 0] = point3s[i].X;
                s[0, 1] = point3s[i].Y;
                s[0, 2] = point3s[i].Z;
                s[0, 3] = 1;

                Matrix outM = Matrix.Multiply(s, R);
                outMas[i].X = outM[0, 0];
                outMas[i].Y = outM[0, 1];
                outMas[i].Z = outM[0, 2];
            }
            return outMas;
        }

        #endregion

        #region Angle
        public void RotateAll(int angleX, int angleY, int angleZ)
        {
            Point3[] tmpP1, tmpP2;
            tmpP1 = RotateX(angleX, pPir);
            tmpP2 = RotateY(angleY, tmpP1);
            pPir = RotateZ(angleZ, tmpP2);
            

            tmpP1 = RotateX(angleX, upperCircle);
            tmpP2 = RotateY(angleY, tmpP1);
            upperCircle = RotateZ(angleZ, tmpP2);

            tmpP1 = RotateX(angleX, lowerCircle);
            tmpP2 = RotateY(angleY, tmpP1);
            lowerCircle = RotateZ(angleZ, tmpP2);

            SetRebra();
            SetGrani();

            Draw();
        }

        private Point3[] RotateX(int ang, Point3[] point3s)
        {
            Point3[] outMas = new Point3[point3s.Length];

            double radians = Math.PI * ang / 180;
            Matrix R = new Matrix(4, 4);
            R[0, 0] = 1;
            R[0, 1] = 0;
            R[0, 2] = 0;
            R[0, 3] = 0;
            R[1, 0] = 0;
            R[1, 1] = Math.Cos(radians);
            R[1, 2] = Math.Sin(radians);
            R[1, 3] = 0;
            R[2, 0] = 0;
            R[2, 1] = -Math.Sin(radians);
            R[2, 2] = Math.Cos(radians);
            R[2, 3] = 0;
            R[3, 0] = 0;
            R[3, 1] = 0;
            R[3, 2] = 0;
            R[3, 3] = 1;

            for (int i = 0; i < point3s.Length; i++)
            {
                Matrix s = new Matrix(1, 4);
                s[0, 0] = point3s[i].X;
                s[0, 1] = point3s[i].Y;
                s[0, 2] = point3s[i].Z;
                s[0, 3] = 1;

                Matrix outM = Matrix.Multiply(s, R);
                outMas[i].X = outM[0, 0];
                outMas[i].Y = outM[0, 1];
                outMas[i].Z = outM[0, 2];
            }

            return outMas;
        }
        private Point3[] RotateY(int ang, Point3[] point3s)
        {
            Point3[] outMas = new Point3[point3s.Length];

            double radians = Math.PI * ang / 180;
            Matrix R = new Matrix(4, 4);
            R[0, 0] = Math.Cos(radians);
            R[0, 1] = 0;
            R[0, 2] = -Math.Sin(radians);
            R[0, 3] = 0;
            R[1, 0] = 0;
            R[1, 1] = 1;
            R[1, 2] = 0;
            R[1, 3] = 0;
            R[2, 0] = Math.Sin(radians);
            R[2, 1] = 0;
            R[2, 2] = Math.Cos(radians);
            R[2, 3] = 0;
            R[3, 0] = 0;
            R[3, 1] = 0;
            R[3, 2] = 0;
            R[3, 3] = 1;

            for (int i = 0; i < point3s.Length; i++)
            {
                Matrix s = new Matrix(1, 4);
                s[0, 0] = point3s[i].X;
                s[0, 1] = point3s[i].Y;
                s[0, 2] = point3s[i].Z;
                s[0, 3] = 1;

                Matrix outM = Matrix.Multiply(s, R);
                outMas[i].X = outM[0, 0];
                outMas[i].Y = outM[0, 1];
                outMas[i].Z = outM[0, 2];
            }
            return outMas;
        }
        private Point3[] RotateZ(int ang, Point3[] point3s)
        {
            Point3[] outMas = new Point3[point3s.Length];

            double radians = Math.PI * ang / 180;
            Matrix R = new Matrix(4, 4);
            R[0, 0] = Math.Cos(radians);
            R[0, 1] = Math.Sin(radians);
            R[0, 2] = 0;
            R[0, 3] = 0;
            R[1, 0] = -Math.Sin(radians);
            R[1, 1] = Math.Cos(radians);
            R[1, 2] = 0;
            R[1, 3] = 0;
            R[2, 0] = 0;
            R[2, 1] = 0;
            R[2, 2] = 1;
            R[2, 3] = 0;
            R[3, 0] = 0;
            R[3, 1] = 0;
            R[3, 2] = 0;
            R[3, 3] = 1;

            for (int i = 0; i < point3s.Length; i++)
            {
                Matrix s = new Matrix(1, 4);
                s[0, 0] = point3s[i].X;
                s[0, 1] = point3s[i].Y;
                s[0, 2] = point3s[i].Z;
                s[0, 3] = 1;

                Matrix outM = Matrix.Multiply(s, R);
                outMas[i].X = outM[0, 0];
                outMas[i].Y = outM[0, 1];
                outMas[i].Z = outM[0, 2];
            }

            return outMas;
        }
        #endregion

        #region Scale

        public void ScaleAll(double scX, double scY, double scZ)
        {
            Point3[] tmpP;
            tmpP = Scale(scX, scY, scZ, pPir);
            for (int i = 0; i < 4; i++)
                pPir[i] = tmpP[i];
            

            tmpP = Scale(scX, scY, scZ, upperCircle);
            for (int i = 0; i < n; i++)
                upperCircle[i] = tmpP[i];

            tmpP = Scale(scX, scY, scZ, lowerCircle);
            for (int i = 0; i < n; i++)
                lowerCircle[i] = tmpP[i];

            SetRebra();
            SetGrani();

            Draw();
        }
        private Point3[] Scale(double scX, double scY, double scZ, Point3[] point3s)
        {
            Point3[] outMas = new Point3[point3s.Length];

            Matrix R = new Matrix(4, 4);
            R[0, 0] = scX; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
            R[1, 0] = 0; R[1, 1] = scY; R[1, 2] = 0; R[1, 3] = 0;
            R[2, 0] = 0; R[2, 1] = 0; R[2, 2] = scZ; R[2, 3] = 0;
            R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

            for (int i = 0; i < point3s.Length; i++)
            {
                Matrix s = new Matrix(1, 4);
                s[0, 0] = point3s[i].X;
                s[0, 1] = point3s[i].Y;
                s[0, 2] = point3s[i].Z;
                s[0, 3] = 1;

                Matrix outM = Matrix.Multiply(s, R);
                outMas[i].X = outM[0, 0];
                outMas[i].Y = outM[0, 1];
                outMas[i].Z = outM[0, 2];
            }
            return outMas;
        }

        #endregion

        #region BackUpsFunction
         private void SetBackup()
         {
             //gtemp add backup
            for (int i = 0; i < pPir.Length; i++)
                pPirBackUp[i] = pPir[i];
            for (int i = 0; i < lowerCircle.Length; i++)
                lowerCircleBackUp[i] = lowerCircle[i];
            for (int i = 0; i < upperCircle.Length; i++)
                upperCircleBackUp[i] = upperCircle[i];

            for (int i = 0; i < verticalRebra.Length; i++)
                verticalRebraBackUp[i] = verticalRebra[i];
            for (int i = 0; i < horizontalRebraUpper.Length; i++)
                horizontalRebraUpperBackUp[i] = horizontalRebraUpper[i];
            for (int i = 0; i < horizontalRebraLower.Length; i++)
                horizontalRebraLowerBackUp[i] = horizontalRebraLower[i];
         }

         private void GetBackup()
         {
             //may be mistake in length, if something happens
             //gtemp add backup
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = pPirBackUp[i];
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = lowerCircleBackUp[i];
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = upperCircleBackUp[i];

             for (int i = 0; i < verticalRebra.Length; i++)
                 verticalRebra[i] = verticalRebraBackUp[i];
             for (int i = 0; i < horizontalRebraUpper.Length; i++)
                 horizontalRebraUpper[i] = horizontalRebraUpperBackUp[i];
             for (int i = 0; i < horizontalRebraLower.Length; i++)
                 horizontalRebraLower[i] = horizontalRebraLowerBackUp[i];
         }

        #endregion

        #region Proection
        //Frontal
         public void ProFrontAll()
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2 ;
             tmpP = ProFront(pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];

             tmpP1 = ProFront(upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP1[i];

             tmpP2 = ProFront(lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP2[i];

             //lightPoint = ProFront(new Point3[] { lightPoint })[0];
             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProFront(Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);
             R[0, 0] = 1; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
             R[1, 0] = 0; R[1, 1] = 1; R[1, 2] = 0; R[1, 3] = 0;
             R[2, 0] = 0; R[2, 1] = 0; R[2, 2] = 0; R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 outMas[i].X = outM[0, 0];
                 outMas[i].Y = outM[0, 1];
                 outMas[i].Z = outM[0, 2];
             }
             return outMas;
         }
        //Horizontal

         public void ProGorAll()
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2;
             tmpP = ProGor(pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];

             tmpP1 = ProGor(upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP1[i];

             tmpP2 = ProGor(lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP2[i];

             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProGor(Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);
             R[0, 0] = 1; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
             R[1, 0] = 0; R[1, 1] = 0; R[1, 2] = 0; R[1, 3] = 0;
             R[2, 0] = 0; R[2, 1] = 0; R[2, 2] = 1; R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 outMas[i].X = outM[0, 0];
                 outMas[i].Y = outM[0, 2];
                 outMas[i].Z = outM[0, 1];
             }
             return outMas;
         }

        //Prof
         public void ProProfAll()
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2;
             tmpP = ProProf(pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];

             tmpP1 = ProProf(upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP1[i];

             tmpP2 = ProProf(lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP2[i];

             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProProf(Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);
             R[0, 0] = 0; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
             R[1, 0] = 0; R[1, 1] = 1; R[1, 2] = 0; R[1, 3] = 0;
             R[2, 0] = 0; R[2, 1] = 0; R[2, 2] = 1; R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 outMas[i].X = outM[0, 2];
                 outMas[i].Y = outM[0, 1];
                 outMas[i].Z = outM[0, 0];
             }
             return outMas;
         }
        //Aksonametric
         public void ProAksAll(double fi, double psi)
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2;
             fi = Math.PI * fi / 180;
             psi = Math.PI * psi / 180;
             tmpP = ProAks(fi, psi, pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];

             tmpP1 = ProAks(fi, psi, upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP1[i];

             tmpP2 = ProAks(fi, psi, lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP2[i];

             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProAks(double fi, double psi, Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);
             R[0, 0] = Math.Cos(psi); R[0, 1] = Math.Sin(fi) * Math.Sin(psi); R[0, 2] = 0; R[0, 3] = 0;
             R[1, 0] = 0; R[1, 1] = Math.Cos(fi); R[1, 2] = 0; R[1, 3] = 0;
             R[2, 0] = Math.Sin(psi); R[2, 1] = -1 * Math.Sin(fi) * Math.Cos(psi); R[2, 2] = 0; R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 outMas[i].X = outM[0, 0];
                 outMas[i].Y = outM[0, 1];
                 outMas[i].Z = outM[0, 2];
             }
             return outMas;
         }

        //Kosougil
         public void ProKosAll(double L, double alpfa)
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2;
             alpfa = Math.PI * alpfa / 180;
             tmpP = ProKos(L, alpfa, pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];

             tmpP1 = ProKos(L, alpfa, upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP1[i];

             tmpP2 = ProKos(L, alpfa, lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP2[i];
             
             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProKos(double L, double alfa, Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);
             R[0, 0] = 1; R[0, 1] = 0; R[0, 2] = 0; R[0, 3] = 0;
             R[1, 0] = 0; R[1, 1] = 1; R[1, 2] = 0; R[1, 3] = 0;
             R[2, 0] = L * Math.Cos(alfa); R[2, 1] = L * Math.Sin(alfa); R[2, 2] = 0; R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = 0; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 outMas[i].X = outM[0, 0];
                 outMas[i].Y = outM[0, 1];
                 outMas[i].Z = outM[0, 2];
             }
             return outMas;
         }
        //Perspect
         public void ProPerAll(double d, double tetta, double fi, double ro)
         {
             SetBackup();
             Point3[] tmpP, tmpP1, tmpP2;
             fi = Math.PI * fi / 180;
             tetta = Math.PI * tetta / 180;
             tmpP = ProPer(d, tetta, fi, ro, pPir);
             for (int i = 0; i < pPir.Length; i++)
                 pPir[i] = tmpP[i];


             tmpP1 = ProPer(d, tetta, fi, ro, lowerCircle);
             for (int i = 0; i < lowerCircle.Length; i++)
                 lowerCircle[i] = tmpP1[i];

             tmpP2 = ProPer(d, tetta, fi, ro, upperCircle);
             for (int i = 0; i < upperCircle.Length; i++)
                 upperCircle[i] = tmpP2[i];

             SetRebra();
             SetGrani();

             Draw();
             GetBackup();
         }
         private Point3[] ProPer(double d, double tetta, double fi, double ro, Point3[] point3s)
         {
             Point3[] outMas = new Point3[point3s.Length];

             Matrix R = new Matrix(4, 4);


             R[0, 0] = Math.Cos(tetta); R[0, 1] = -Math.Cos(fi) * Math.Sin(tetta); R[0, 2] = -Math.Sin(fi) * Math.Sin(tetta); R[0, 3] = 0;
             R[1, 0] = Math.Sin(tetta); R[1, 1] = Math.Cos(fi) * Math.Cos(tetta); R[1, 2] = Math.Sin(fi) * Math.Cos(tetta); R[1, 3] = 0;
             R[2, 0] = 0; R[2, 1] = Math.Sin(fi); R[2, 2] = -Math.Cos(fi); R[2, 3] = 0;
             R[3, 0] = 0; R[3, 1] = 0; R[3, 2] = ro; R[3, 3] = 1;

             for (int i = 0; i < point3s.Length; i++)
             {
                 Matrix s = new Matrix(1, 4);
                 s[0, 0] = point3s[i].X;
                 s[0, 1] = point3s[i].Y;
                 s[0, 2] = point3s[i].Z;
                 s[0, 3] = 1;

                 Matrix outM = Matrix.Multiply(s, R);
                 if (outM[0, 2] == 0)
                 {
                     outM[0, 2] = 0.1;
                 }
                 outMas[i].X = outM[0, 0] * d / outM[0, 2];
                 outMas[i].Y = outM[0, 1] * d / outM[0, 2];
                 outMas[i].Z = outM[0, 2] * d / outM[0, 2];
             }
             return outMas;
         }

        #endregion
    }
}
