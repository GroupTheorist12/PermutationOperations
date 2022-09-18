#pragma warning disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PermutationOperations
{
    public class IntMatrix
    {
        private int[,] InternalRep = null;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public IntMatrix(int Order)
        {
            this.Rows = Order;
            this.Columns = Order;

            InternalRep = new int[Rows, Columns];
            Zero();
        }

        public void Zero()
        {
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    InternalRep[rowCount, colCount] = 0;
                }
            }
        }

        public static IntMatrix UnitalMatrix(int Order)
        {

            IntMatrix im = new IntMatrix(Order);
            for (int rowCount = 0; rowCount < im.Rows; rowCount++)
            {
                for (int colCount = 0; colCount < im.Columns; colCount++)
                {
                    im.InternalRep[rowCount, colCount] = 1;
                }
            }

            return im;
        }

        public void SetValue(int row, int col, int value)
        {
            this.InternalRep[row, col] = value;
        }
        public int GetValue(int row, int col)
        {
            return this.InternalRep[row, col];
        }

        public bool IsUnital()
        {
            bool ret = true;
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    if (InternalRep[rowCount, colCount] != 1)
                    {
                        return false;

                    }
                }
            }

            return ret;

        }

        public bool HasZeros()
        {
            bool ret = false;
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    if (InternalRep[rowCount, colCount] == 0)
                    {
                        return true;
                    }
                }
            }

            return ret;

        }

        public bool IsOffDiagonal()
        {
            bool ret = true;
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    if (rowCount == colCount && InternalRep[rowCount, colCount] != 0)
                    {
                        return false;

                    }
                }
            }

            return ret;

        }

        public IntMatrix Clone()
        {
            IntMatrix cloner = new IntMatrix(this.Rows);
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    cloner.InternalRep[rowCount, colCount] = InternalRep[rowCount, colCount];
                }
            }

            return cloner;
        }

        public int this[int i, int j]
        {
            get
            {
                return GetValue(i, j);
            }
            set
            {
                SetValue(i, j, value);
            }
        }
        public IntMatrix SwapRow(int RowA, int RowB)
        {
            IntMatrix SwapMatrix = this.Clone();

            int ra = RowA - 1; // indexer
            int rb = RowB - 1; // indexer
            for (int i = 0; i < Columns; i++)
            {
                SwapMatrix.SetValue(ra, i, this.GetValue(rb, i));
                SwapMatrix.SetValue(rb, i, this.GetValue(ra, i));

            }
            return SwapMatrix;

        }

        public static IntMatrix SetRowFromVec(IntMatrix matrixIn, List<int> Vec, int Row)
        {
            IntMatrix matrixOut = matrixIn.Clone();
            for (int i = 0; i < matrixIn.Columns; i++)
            {
                matrixOut.SetValue(Row, i, Vec[i]);
            }
            return matrixOut;
        }

        public static IntMatrix SetColumnFromVec(IntMatrix matrixIn, List<int> Vec, int Column)
        {
            IntMatrix matrixOut = matrixIn.Clone();
            for (int i = 0; i < matrixIn.Columns; i++)
            {
                matrixOut.SetValue(i, Column, Vec[i]);
            }
            return matrixOut;
        }

        public IntMatrix Transpose()
        {
            IntMatrix matrixOut = new IntMatrix(this.Rows);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrixOut.SetValue(j, i, this.GetValue(i, j));
                }

            }

            return matrixOut;

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int rowCount = 0; rowCount < Rows; rowCount++)
            {
                for (int colCount = 0; colCount < Columns; colCount++)
                {
                    sb.AppendFormat("{0}\t", InternalRep[rowCount, colCount]);
                }

                sb.Append("\r\n");
            }

            return sb.ToString();
        }


        public static IntMatrix Product(IntMatrix A, IntMatrix B)
        {
            IntMatrix matrixOut = new IntMatrix(A.Rows);
            for (int i = 0; i < matrixOut.Rows; i++)
            {
                for (int j = 0; j < matrixOut.Columns; j++)
                {
                    for (int k = 0; k < matrixOut.Columns; k++)
                    {
                        int val = matrixOut.GetValue(i, j);
                        val += A.GetValue(i, k) * B.GetValue(k, j);
                        matrixOut.SetValue(i, j, val);
                    }
                }
            }

            return matrixOut;
        }

        public IntMatrix MulScalar(int ScalarValue)
        {
            IntMatrix matrixOut = this.Clone();
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    int val = matrixOut.GetValue(i, j) * ScalarValue;
                    matrixOut.SetValue(i, j, val);
                }
            }

            return matrixOut;
        }

        public static IntMatrix Addition(IntMatrix A, IntMatrix B)
        {
            IntMatrix matrixOut = A.Clone();
            for (int i = 0; i < A.Rows; i++)
            {
                for (int j = 0; j < A.Columns; j++)
                {
                    matrixOut.SetValue(i, j, A.GetValue(i, j) + B.GetValue(i, j));
                }
            }

            return matrixOut;
        }

        public static IntMatrix operator +(IntMatrix A, IntMatrix B)
        {
            return Addition(A, B);
        }

        public static IntMatrix operator *(IntMatrix A, IntMatrix B)
        {
            return Product(A, B);
        }

        public bool Equals(IntMatrix matrix)
        {
            if (ReferenceEquals(matrix, null))
            {
                return false;
            }

            if (ReferenceEquals(this, matrix))
            {
                return true;
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (this.GetValue(i, j) != matrix.GetValue(i, j))
                    {
                        return false;
                    }
                }

            }

            return true;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as IntMatrix);
        }

        public static bool operator !=(IntMatrix first, IntMatrix second)
        {
            return !(first == second);
        }

        public static bool operator ==(IntMatrix first, IntMatrix second)
        {
            if (ReferenceEquals(first, null)
               || ReferenceEquals(second, null))
            {
                return ReferenceEquals(first, second);
            }

            return first.Equals(second);
        }

        public bool Identity()
        {
            bool ret = true;
            for (int i = 0; i < this.Rows; i++)
            {
                if (this.GetValue(i, i) != 1)
                {
                    ret = false;
                    break;
                }

            }

            return ret;
        }
        public static IntMatrix IdentityMatrix(int Order)
        {
            IntMatrix matrix = new IntMatrix(Order);

            for (int i = 0; i < matrix.Rows; i++)
            {
                matrix.SetValue(i, i, 1);
            }

            return matrix;
        }

        public static IntMatrix PermMatrix(List<int> br) //get from bottom row of permutation
        {
            IntMatrix matrix = new IntMatrix(br.Count());

            br.ForEach(delegate (int i)
            {
                int colN = br[i] - 1;

                matrix.SetValue(colN, i, 1); //perm value is row, i is column
            });

            return matrix;
        }

        public List<int> GetRow(int Row)
        {
            List<int> vecRow = new List<int>();
            for (int i = 0; i < this.Columns; i++)
            {
                vecRow.Add(this.GetValue(Row, i));
            }

            return vecRow;
        }
        public List<int> GetColumn(int Column)
        {
            List<int> vecRow = new List<int>();
            for (int i = 0; i < this.Columns; i++)
            {
                vecRow.Add(this.GetValue(i, Column));
            }

            return vecRow;
        }



        //Overriding the GetHashCode method
        //GetHashCode method generates hashcode for the current object
        public override int GetHashCode()
        {
            //Performing BIT wise OR Operation on the generated hashcode values
            //If the corresponding bits are different, it gives 1.
            //If the corresponding bits are the same, it gives 0.
            return this.Rows.GetHashCode() ^ this.Columns.GetHashCode();
        }



    }
}