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
            
            Console.WriteLine("Would you like to play (1) or run statistics (2)");
            string optionSelected = Console.ReadLine();

            if (optionSelected == "1")
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

                    string Winningates = Play(int.Parse(systemDepth), false);

                    Console.WriteLine(string.Format("The Gates that received a ball {0}", Winningates.TrimEnd(',')));

                    Console.WriteLine("Would you like to play again? (Y)");
                    string input = Console.ReadLine();
                    if (input != "Y")
                        break;


                }
            }

            else if (optionSelected == "2")
            {
                string numberOfRunsStr;

                Console.WriteLine("Please enter the number of runs you would like to execute. Note this is running on a system with 4 levels");

                Regex regex = new Regex("^[0-9]+$");
                while (true)
                {
                    numberOfRunsStr = Console.ReadLine();
                    if (regex.Match(numberOfRunsStr).Success)
                        break;

                    Console.WriteLine("Please enter digits only");
                }

                int numberOfRuns = int.Parse(numberOfRunsStr);

                RunStatisticsOnSystem4Levels(numberOfRuns);
            }
            else
            {
                Console.WriteLine("Well.. Next time please choose between options 1 or 2 and select enter.. :)");
            }

        }

        private static string Play(int systemDepth, bool withStatistics)
        {
            double numberOfBalls = Math.Pow(2, systemDepth) - 1;

            Gate tree = new Gate(true, withStatistics);
            tree.BuildTree(systemDepth);

            string Winningates = string.Empty;

            for (int i = 0; i < numberOfBalls; i++)
            {
                Winningates += tree.ReciveBall(withStatistics);
            }

            return Winningates;
        }

        private static void RunStatisticsOnSystem4Levels(int numberOfRuns)
        {
            for (int i = 1; i <= numberOfRuns; i++)
            {
                Play(4, true);
            }

            int minValue = int.MaxValue;
            string containerWithMinValue = string.Empty;
            string alphabeticalIndex = string.Empty;
            for (int i = 0; i < Gate.StaicStatisticeContainerCounterArry.Count(); i++)
            {
                alphabeticalIndex = ((Char)(65 + i)).ToString();
                Console.WriteLine(string.Format("Container '{0}' received a ball {1} times", alphabeticalIndex, Gate.StaicStatisticeContainerCounterArry[i]));

                if (Gate.StaicStatisticeContainerCounterArry[i] < minValue)
                {
                    minValue = Gate.StaicStatisticeContainerCounterArry[i];
                    containerWithMinValue = "'" + alphabeticalIndex + "'";
                }
                else if (Gate.StaicStatisticeContainerCounterArry[i] == minValue)
                    containerWithMinValue += ", '" + alphabeticalIndex + "'";

            }

            Console.WriteLine("\nStatistical outcome:");
            Console.WriteLine(string.Format(@"Container\s {0} received the least amount of balls - {1}", containerWithMinValue, minValue));
            Console.WriteLine(string.Format("Root gate opened left {0} times out of {1}", Gate.StaticAmountOfTimeRootGateOpenedLeft, numberOfRuns));


        }
    }
}
