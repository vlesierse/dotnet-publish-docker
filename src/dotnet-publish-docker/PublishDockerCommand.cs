using System.IO;

namespace DotNet.Tools.Docker
{
    public class PublishDockerCommand
    {
        private readonly string _baseImage;
        private readonly string _projectPath;
        private readonly string _publishFolder;
        
        public PublishDockerCommand(string publishFolder, string projectPath, string baseImage)
        {
            _baseImage = baseImage;
            _projectPath = projectPath;
            _publishFolder = publishFolder;
        }
        
        public int Run()
        {
            var applicationBasePath = GetApplicationBasePath();
            var applicationName = GetApplicationName(applicationBasePath);
            
            var dockerfile = new DockerfileWriter(applicationName, _baseImage).Write();
            
            File.WriteAllText(Path.Combine(_publishFolder, "Dockerfile"), dockerfile);
            
            var exitCode = new DockerCommand(_publishFolder, applicationName).Run();
            
            return exitCode;
        }

        private string GetApplicationBasePath()
        {
            if (!string.IsNullOrEmpty(_projectPath))
            {
                var fullProjectPath = Path.GetFullPath(_projectPath);

                return Path.GetFileName(fullProjectPath) == "project.json"
                    ? Path.GetDirectoryName(fullProjectPath)
                    : fullProjectPath;
            }

            return Directory.GetCurrentDirectory();
        }
        
        private string GetApplicationName(string applicationBasePath)
        {
            return new DirectoryInfo(applicationBasePath).Name;
        }
    }
}