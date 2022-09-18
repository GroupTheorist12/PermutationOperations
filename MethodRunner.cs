#pragma warning disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace PermutationOperations
{
    public class MethodRunner
    {
        private static Hashtable htTestFuncs = new Hashtable();

        public static int RunIt(string hashEntry)
        {
            MethodInfo mi = (MethodInfo)htTestFuncs[hashEntry];
            return (int)mi.Invoke(null, null);
        }

        static MethodRunner()
        {

            // get all public static methods of MethodRunner type
            MethodInfo[] methodInfos = typeof(MethodRunner).GetMethods(BindingFlags.Public |
                                                                BindingFlags.Static);
            // sort methods by name
            Array.Sort(methodInfos,
                    delegate (MethodInfo methodInfo1, MethodInfo methodInfo2)
                    { return methodInfo1.Name.CompareTo(methodInfo2.Name); });

            // write method names to hash
            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.Name.IndexOf("Test_") == -1)
                {
                    continue;
                }

                string miKey = methodInfo.Name.Replace("Test_", "");
                //Console.WriteLine(miKey);

                htTestFuncs[miKey] = methodInfo;
            }


        }

        public static int Test_IntMatrix()
        {
            IntMatrix im = new IntMatrix(3);
            Console.WriteLine($"{im}");
            return 0;
        }

        public static int Test_IntMatrixGetterSetter()
        {
            IntMatrix im = new IntMatrix(3);
            im.SetValue(0, 0, 1);
            Console.WriteLine($"{im}");

            Console.WriteLine($"row 0 column 0 = {im.GetValue(0, 0)}");
            return 0;
        }
        public static int Test_IntMatrixAccessor()
        {
            IntMatrix im = new IntMatrix(3);
            im[0, 0] = 1;
            Console.WriteLine($"{im}");

            Console.WriteLine($"row 0 column 0 = {im[0, 0]}");
            return 0;
        }
        public static int Test_IntMatrixUnital()
        {
            IntMatrix im = new IntMatrix(3);
            im[0, 0] = 1;
            Console.WriteLine($"{im}");

            Console.WriteLine($"is unital? {im.IsUnital()}");

            Console.WriteLine();
            IntMatrix imUnital = IntMatrix.UnitalMatrix(3);
            Console.WriteLine($"{imUnital}");
            Console.WriteLine($"is unital? {imUnital.IsUnital()}");

            return 0;
        }


        public static int Test_IntMatrixIsOffDiagnal()
        {
            IntMatrix im = IntMatrix.IdentityMatrix(3);

            Console.WriteLine($"{im}");
            Console.WriteLine($"Is off diagnal? {im.IsOffDiagonal()}");
            return 0;

        }

        public static int Test_Permutation()
        {
            Permutation perm = new Permutation(5, 3, 1, 2, 4);
            Console.WriteLine($"{perm}");
            return 0;
        }

        public static int Test_PermutationList()
        {
            PermutationList permutations = new PermutationList(5);
            permutations.Permute();

            Console.WriteLine($"{permutations}{Environment.NewLine}");

            return 0;
        }


    }
}