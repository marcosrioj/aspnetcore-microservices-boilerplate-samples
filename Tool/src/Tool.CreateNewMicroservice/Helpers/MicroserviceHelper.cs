using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Tool.CreateNewMicroservice.Extensions;

namespace Tool.CreateNewMicroservice.Helpers
{
    public static class MicroserviceHelper
    {
        public static void CreateNewMicroservice(string projectName, bool addProjectsToTheMainSolution)
        {
            var microserviceBaseProjectName = "MicroserviceBaseProject";
            var solutionPath = GetSolutionPath();
            var microserviceBasePath = $"{solutionPath}{Path.DirectorySeparatorChar}{microserviceBaseProjectName}";
            var newMicroserviceBasePath = $"{solutionPath}{Path.DirectorySeparatorChar}{projectName}";

            CopyAndPasteMicroserviceBaseProjectFolder(microserviceBasePath, newMicroserviceBasePath);
            RenameFolders(newMicroserviceBasePath, microserviceBaseProjectName, projectName);
            RenameFiles(newMicroserviceBasePath, microserviceBaseProjectName, projectName);
            ReplaceTextWithinFiles(newMicroserviceBasePath, microserviceBaseProjectName, projectName);
            AddSharedDetails(projectName);

            if (addProjectsToTheMainSolution)
            {
                AddMicroserviceProjectsToTheSolution(projectName, solutionPath);
            }
        }

        static void CopyAndPasteMicroserviceBaseProjectFolder(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                throw new Exception(@"Could not find the MicroserviceBaseProject folder.
                                    Make sure the folder exists or download the entire project again 
                                    at https://github.com/marcoslimacom/aspnetcore-microservices-boilerplate!");
            }

            if (Directory.Exists(destinationPath))
            {
                var destinationFolderName = destinationPath.Split(Path.DirectorySeparatorChar).LastOrDefault();
                throw new Exception($"The folder named {destinationFolderName} already exists in the solution's root folder!");
            }

            try
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not copy MicroserviceBaseProjectFolder folder. Error: {ex.Message}");
            }
        }

        static void RenameFolders(string path, string source, string dest)
        {
            var di = new DirectoryInfo(path);

            foreach (var si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                RenameFolders(@$"{si.Parent.FullName}{Path.DirectorySeparatorChar}{si.Name}", source, dest);

                string strFoldername = si.Name;
                if (strFoldername.Contains(source))
                {
                    strFoldername = strFoldername.Replace(source, dest);
                    string strFolderRoot = $"{si.Parent.FullName}{Path.DirectorySeparatorChar}{strFoldername}";

                    si.MoveTo(strFolderRoot);
                }
            }
        }

        static void RenameFiles(string path, string source, string dest)
        {
            var di = new DirectoryInfo(path);

            var files = di.GetFiles();
            foreach (var file in files)
            {
                File.Move(file.FullName, file.FullName.Replace(source, dest));
            }

            foreach (var si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                RenameFiles(@$"{si.Parent.FullName}{Path.DirectorySeparatorChar}{si.Name}", source, dest);
            }
        }

        static void ReplaceTextWithinFiles(string path, string source, string dest)
        {
            var di = new DirectoryInfo(path);

            var files = di.GetFiles();
            foreach (var file in files)
            {
                var reader = new StreamReader(file.FullName, Encoding.Default);
                string content = reader.ReadToEnd();
                reader.Close();

                content = Regex.Replace(content, source, dest);
                content = Regex.Replace(content, source.ToLowerFirstLetter(), dest.ToLowerFirstLetter());

                var writer = new StreamWriter(file.FullName);
                writer.Write(content);
                writer.Close();
            }

            foreach (var si in di.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                ReplaceTextWithinFiles(@$"{si.Parent.FullName}{Path.DirectorySeparatorChar}{si.Name}", source, dest);
            }
        }

        static void AddSharedDetails(string projectName)
        {
            var di = new DirectoryInfo(GetSolutionPath());

            var sharedCoreFolder = Path.Combine(di.FullName, $"Shared{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}Shared.Core");
            if (!Directory.Exists(sharedCoreFolder))
            {
                throw new Exception("Could not find shared core project folder!");
            }

            var fileSharedConsts = Path.Combine(sharedCoreFolder, "SharedConsts.cs");
            if (!File.Exists(fileSharedConsts))
            {
                throw new Exception("Could not find SharedConsts file!");
            }

            var fileAppConfigurations = Path.Combine(sharedCoreFolder, $"Configuration{Path.DirectorySeparatorChar}AppConfigurations.cs");
            if (!File.Exists(fileAppConfigurations))
            {
                throw new Exception("Could not find AppConfigurations file!");
            }

            var sbText = new StringBuilder();
            using (var reader = new StreamReader(fileSharedConsts, Encoding.Default))
            {
                var line = string.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("SharedConnectionStringName"))
                    {
                        sbText.AppendLine(line);
                        sbText.AppendLine(string.Empty);
                        sbText.AppendLine($"        public const string {projectName}ConnectionStringName = \"{projectName}\";");
                    }
                    else
                    {
                        sbText.AppendLine(line);
                    }
                }
            }

            var writer = new StreamWriter(fileSharedConsts);
            writer.Write(sbText.ToString());
            writer.Close();

            sbText = new StringBuilder();
            using (var reader = new StreamReader(fileAppConfigurations, Encoding.Default))
            {
                var line = string.Empty;
                var firstExec = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("break;") && firstExec)
                    {
                        sbText.AppendLine(line);
                        sbText.AppendLine($"                case \"{projectName}.EntityFrameworkCore.{projectName}DbContext\":");
                        sbText.AppendLine($"                    connectionString = configuration.GetConnectionString(SharedConsts.{projectName}ConnectionStringName);");
                        sbText.AppendLine($"                    break;");
                        firstExec = false;
                    }
                    else
                    {
                        sbText.AppendLine(line);
                    }
                }
            }

            writer = new StreamWriter(fileAppConfigurations);
            writer.Write(sbText.ToString());
            writer.Close();
        }

        static void AddMicroserviceProjectsToTheSolution(string projectName, string solutionPath)
        {
            var spc = Path.DirectorySeparatorChar;
            var commandSlnAdd = $"dotnet sln {solutionPath}{spc}All.sln add";
            var projectsPath = new StringBuilder();
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.Application{spc}{projectName}.Application.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.Core{spc}{projectName}.Core.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.EntityFrameworkCore{spc}{projectName}.EntityFrameworkCore.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.Migrator{spc}{projectName}.Migrator.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.Web.Core{spc}{projectName}.Web.Core.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}src{spc}{projectName}.Web.Host{spc}{projectName}.Web.Host.csproj");
            projectsPath.Append($@" {solutionPath}{spc}{projectName}{spc}test{spc}{projectName}.Tests{spc}{projectName}.Tests.csproj");
            var command = $"{commandSlnAdd}{projectsPath}";
            command.ExecCommand();
        }

        static string GetSolutionPath()
        {
            var programAssemblyDirectoryPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            var di = new DirectoryInfo(programAssemblyDirectoryPath);
            while (!DirectoryContains(di.FullName, "All.sln"))
            {
                if (di.Parent == null)
                {
                    throw new Exception("Could not find solution folder!");
                }

                di = di.Parent;
            }

            return di.FullName;
        }

        static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}