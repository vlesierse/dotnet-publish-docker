using System.Diagnostics;

namespace DotNet.Tools.Docker
{
    public class DockerCommand
    {
        private readonly string _publishFolder;
        private readonly string _imageName;
        
        public DockerCommand(string publishFolder, string imageName)
        {
            _publishFolder = publishFolder;
            _imageName = imageName;
        }
        public int Run()
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = "docker",
                Arguments = $"build -t {_imageName.ToLower()} {_publishFolder}",
                UseShellExecute = false
            };
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}