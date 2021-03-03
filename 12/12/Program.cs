using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12
{
    class Program
    {
        public static List<int> reshetoEratosfena = new List<int>();
        public static int GetNOD(int operand1, int operand2)
        {
            int difference = 1;

            if (operand1 < operand2)
            {
                int changer = operand1;
                operand1 = operand2;
                operand2 = changer;
            }

            while (difference != 0)
            {
                    difference = operand1 % operand2;
                    operand1 = operand2;
                    operand2 = difference;
            }

            return operand1;
        }

        public static int GetNOD(int operand1, int operand2, int operand3)
        {
            return GetNOD(GetNOD(operand1, operand2), operand3);
        }

        public static List<int> GetSimples(int startOfInterval, int endOfInterval)
        {
            List<int> simples = new List<int>();

            while(reshetoEratosfena.Count <= endOfInterval + 10)
            {
                reshetoEratosfena.Add(reshetoEratosfena.Count);
            }

            for (int i = 2; i <= endOfInterval; i++)
            {
                if (reshetoEratosfena[i] != 0)
                {
                    for (int j = i * 2; j <= endOfInterval; j += i)
                    {
                        reshetoEratosfena[j] = 0;
                    }
                }
            }

            simples = reshetoEratosfena.GetRange(startOfInterval, endOfInterval - startOfInterval).Where(x => x != 0).ToList();

            return simples;

        }

        static void Main(string[] args)
        {
            Console.WriteLine("NOD(1234, 54)");
            Console.WriteLine(GetNOD(1234, 54));

            Console.WriteLine("NOD(51, 102, 48)");
            Console.WriteLine(GetNOD(51, 102, 48));

            Console.WriteLine("Simples(2, 577)");
            List<int> simples = GetSimples(2, 577);
            foreach (var item in simples)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Simples(540, 577)");
            simples = GetSimples(540, 577);
            foreach (var item in simples)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
