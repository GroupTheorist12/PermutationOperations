using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PermutationOperations
{
    public class PermutationList : List<Permutation>
    {
        private static void Swap(List<int> set, int index1, int index2)
        {
            int temp = set[index1];
            set[index1] = set[index2];
            set[index2] = temp;
        }

        private static int Factorial(int n)
        {
            int res = 1;
            while (n != 1)
            {
                res = res * n;
                n = n - 1;
            }
            return res;
        }

        private static List<int> GetSet(int Order)
        {
            List<int> set = new List<int>();

            for (int i = 0; i < Order; i++)
            {
                set.Add(i + 1);
            }
            return set;
        }

        public List<int> ImutableSet { get; private set;}
        public int PermutationOrder { get; private set;}
        public int SetOrder { get; private set;}


        private int Max { get; }

        public PermutationList()
        {
            ImutableSet = new List<int>();   
        }
        public PermutationList(int Order)
        {
            this.SetOrder = Order;
            this.ImutableSet = GetSet(Order);
            this.PermutationOrder = Factorial(Order);

            this.Max = this.PermutationOrder / Order;

        }

        public void GetPermutations(List<int> set)
        {
            int ind1 = 1;
            int ind2 = 2;
            for (int i = 0; i < this.Max; i++)
            {
                Swap(set, ind1, ind2);
                this.Add(new Permutation(set.ToArray()));

                ind1 = ind1 + 1;
                ind2 = ind2 + 1;

                if (ind2 > this.SetOrder - 1)
                {
                    ind1 = 1;
                    ind2 = 2;
                }

            }
        }

        public void Permute()
        {
            if (this.SetOrder == 2)
            {
                this.Add(new Permutation(new int[] { 1, 2 }));
                this.Add(new Permutation(new int[] { 2, 1 }));
                return;

            }

            List<int> set = GetSet(this.SetOrder);

            for (int i = 0; i < this.SetOrder; i++)
            {
                GetPermutations(new List<int>(set));

                if (i < this.SetOrder - 1)
                {
                    Swap(set, 0, i + 1);
                }
            }

        }

        private string GetTopRowString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.SetOrder; i++)
            {
                sb.AppendFormat("{0}\t", this.ImutableSet[i]);
            }

            return sb.ToString();

        }
        private string GetBottomRowString(Permutation perm)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.SetOrder; i++)
            {
                sb.AppendFormat("{0}\t", perm[i]);
            }

            return sb.ToString();

        }

        private string CreatePermRow(List<Permutation> perms)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Permutation perm in perms)
            {
                sb.Append(GetTopRowString());
                sb.Append("\t");
            }

            sb.Append($"{Environment.NewLine}");

            foreach (Permutation perm in perms)
            {
                sb.Append(GetBottomRowString(perm));
                sb.Append("\t");
            }


            return sb.ToString();
        }

       public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int ind = 0;
            List<Permutation> tmp = new List<Permutation>();


            foreach (Permutation perm in this)
            {
                tmp.Add(perm);

                if (ind >= this.SetOrder - 1)
                {
                    sb.Append(CreatePermRow(tmp));
                    sb.Append($"{Environment.NewLine}");
                    sb.Append($"{Environment.NewLine}");
                    ind = 0;
                    tmp.Clear();
                    continue;

                }

                ind++;
            }

            if (tmp.Count() > 0)
            {
                sb.Append(CreatePermRow(tmp));
                sb.Append($"{Environment.NewLine}");
                sb.Append($"{Environment.NewLine}");

            }
            return sb.ToString();
        }



    }
}