using System;
using System.IO;
using System.Threading;
using Tool.CreateNewMicroservice.Extensions;
using Tool.CreateNewMicroservice.Helpers;

namespace Tool.CreateNewMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Enter the name of the new microservice:");
            var projectName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(projectName))
            {
                Console.Write("Error: The project name cannot be empty!");
                Thread.Sleep(3000);
                Main(new string[] { });
            }

            if (!projectName.IsFirstCharacterLetter())
            {
                Console.Write("Error: The first character of the project name must be a letter!");
                Thread.Sleep(3000);
                Main(new string[] { });
            }

            Console.Write("Do you want to add microservice projects to the main solution? (y/N):");
            var addProjectsRead = Console.ReadLine();
            var addProjectsToTheMainSolution = addProjectsRead.ToLower() == "y";

            Console.WriteLine("Creating a new microservice solution...");
            MicroserviceHelper.CreateNewMicroservice(projectName.ToString().ToUpperFirstLetter(), addProjectsToTheMainSolution);
            Console.WriteLine($"Microservice solution {projectName} successfully created!");
        }
    }
}
