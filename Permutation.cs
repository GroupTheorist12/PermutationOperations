using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace PermutationOperations
{
    public class Permutation
    {
        private List<int> tr = new List<int>();
        private List<int> br = new List<int>();

        public int this[int brindex]
        {
            get
            {
                return br[brindex];
            }
        }
        
        public int Order { get; set; }
        public Permutation(params int[] br)
        {
            Order = br.Length;
            for (int i = 0; i < br.Length; i++)
            {
                tr.Add(i + 1);
                this.br.Add(br[i]);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tr.Count(); i++)
            {
                sb.AppendFormat("{0}\t", tr[i]);

            }
            sb.Append($"{Environment.NewLine}");
            for (int i = 0; i < br.Count(); i++)
            {
                sb.AppendFormat("{0}\t", br[i]);

            }

            return sb.ToString();
        }

        public Permutation Product(Permutation a, Permutation b)
        {
            Permutation ret = new Permutation(1);

            List<int> tmp = new List<int>();
            for (int i = 0; i < a.tr.Count(); i++)
            {
                int ib = b.br[i] - 1;
                int ia = a.br[ib];
                tmp.Add(ia);
            }

            ret = new Permutation(tmp.ToArray());
            return ret;
        }

    }
}