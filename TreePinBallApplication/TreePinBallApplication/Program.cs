using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TreePinBallApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string systemDepth;

                Console.WriteLine("Please enter the depth of your system");

                Regex regex = new Regex("^[0-9]+$");
                while (true)
                {
                    systemDepth = Console.ReadLine();
                    if (regex.Match(systemDepth).Success)
                        break;

                    Console.WriteLine("Please enter digits only");
                }

                Play(int.Parse(systemDepth));

                Console.WriteLine("Would you like to play again? (Y)");
                string input = Console.ReadLine();
                if (input != "Y")
                    break;

                
            }

        }

        private static void Play(int systemDepth)
        {
            double numberOfBalls = Math.Pow(2, systemDepth) - 1;

            Gate tree = new Gate(true);
            tree.BuildTree(systemDepth);

            string Winningates = string.Empty;

            for (int i = 0; i < numberOfBalls; i++)
            {
                Winningates += tree.ReciveBall();
            }

            Console.WriteLine(string.Format("The Gates that received a ball {0}", Winningates.TrimEnd(',')));
        }
    }
}
