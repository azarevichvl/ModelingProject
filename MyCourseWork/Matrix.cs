using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCourseWork
{
    public class Matrix
    {
        readonly double[,] matrix;
        readonly int rows;
        readonly int columns;

        public Matrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            matrix = new double[rows, columns];
        }

        // set/get elements of array of matrix
        public double this[int i, int j]
        {
            set { matrix[i, j] = value; }
            get { return matrix[i, j]; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }
      
        public static Matrix Multiply(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.Rows, B.Columns);
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < B.Columns; j++)
                {
                    double tmp = C[i, j];
                    for (int k = 0; k < B.Rows; k++)
                    {
                        tmp += A[i, k] * B[k, j];
                    }
                    C[i, j] = tmp;
                }
            }
            return C;
        }
    }
}
