using System;

namespace GatedTreeSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a integer number for the depth of the system you want.");
            string input = Console.ReadLine();

            try
            {
                //Set depth default as 4;
                int depth = 4;

                if (!Int32.TryParse(input, out depth))
                {
                    Console.WriteLine("Invalid number inputed.");
                    Console.Read();
                    return;
                }

                Console.WriteLine("Try to initialize a new system with depth as {0}", depth);

                IGatedTree gatedTree = new GatedTree(depth);

                do
                {
                    Console.WriteLine("The initial state of the system:");
                    Console.Write(gatedTree.ToString());

                    Console.WriteLine("Let's predict which contianer will not receive a ball, the contianer should be:");
                    int predicatedEmptyContainer = gatedTree.Predict();
                    Console.WriteLine(predicatedEmptyContainer);

                    Console.WriteLine("Now pass through all the balls through the system:");
                    int actualEmptyContainer = gatedTree.RunBalls();
                    Console.WriteLine("Done!");

                    Console.WriteLine("The contianer that did not receive a ball is:");
                    Console.WriteLine(actualEmptyContainer);

                    Console.WriteLine("Prediction matches outcome?:");
                    Console.WriteLine(actualEmptyContainer == predicatedEmptyContainer);

                    Console.WriteLine("The state of the system after all balls passed through:");
                    Console.Write(gatedTree.ToString());

                    gatedTree.Reset();

                    Console.WriteLine("Press Esc then Enter to exit the system, or press any other key to try again:");
                }
                while (Console.ReadKey().Key != ConsoleKey.Escape);

                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("Initlization of system failed because of: {0}", e.ToString());
                Console.Read();
            }
        }
    }
}
