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

                Console.WriteLine("Try to initialize a new system with depth as {0}.", depth);

                IGatedNodeCreator nodeCreator = new GatedNodeCreator();

                IGatedTree tree = new GatedTree(depth, nodeCreator);
                IGatedTreeController controller = new GatedTreeController(tree);

                do
                {
                    Console.WriteLine("The initial state of the system:");
                    Console.WriteLine(tree.ToString());

                    Console.WriteLine("Let's predict which contianer will not receive a ball, the contianer should be:");
                    int predicatedEmptyContainer = controller.PredictEmptyContainer();
                    Console.WriteLine(predicatedEmptyContainer);

                    Console.WriteLine("Now pass all the balls through the system:");
                    controller.RunBalls();
                    Console.WriteLine("Done!");

                    Console.WriteLine("The contianer that did not receive a ball is:");
                    int actualEmptyContainer = controller.CheckEmptyContainer();
                    Console.WriteLine(actualEmptyContainer);

                    Console.WriteLine("Prediction matches outcome?");
                    Console.WriteLine(actualEmptyContainer == predicatedEmptyContainer);

                    Console.WriteLine("The state of the system after all balls passed through:");
                    Console.WriteLine(tree.ToString());

                    controller.Reset();

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
