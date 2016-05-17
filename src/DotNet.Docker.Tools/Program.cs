using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace DotNet.Tools.Docker
{
    public class Program
    {
        private ILogger _logger;
        private ILoggerFactory _loggerFactory;
        
        public Program()
        {
            _loggerFactory = new LoggerFactory();
            _loggerFactory.AddProvider(new CommandOutputProvider());
            _logger = _loggerFactory.CreateLogger<Program>();
        }
        
        private int Run(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "dotnet publish-docker",
                FullName = ".NET Core Docker Publisher",
                Description = "Docker Publisher for the .NET Core applications",
            };
            app.HelpOption("-h|--help");
            
            var baseImageOption = app.Option("--base-image|-b", "Docker base image", CommandOptionType.SingleValue);
            var publishFolderOption = app.Option("--publish-folder|-p", "The path to the publish output folder", CommandOptionType.SingleValue);
            var projectPath = app.Argument("<PROJECT>", "The path to the project (project folder or project.json) being published. If empty the current directory is used.");
            
            app.OnExecute(() =>
            {
                var publishFolder = publishFolderOption.Value();
                if (publishFolder == null)
                {
                    app.ShowHelp();
                    return 2;
                }
                
                _logger.LogInformation($"Configuring the following project for use with Docker: ''");

                var exitCode = new PublishDockerCommand(publishFolder, projectPath.Value, baseImageOption.Value()).Run();

                _logger.LogInformation("Configuring project completed successfully");

                return exitCode;
            });

            try
            {
                return app.Execute(args);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                _logger.LogDebug(e.ToString());
            }
            return 1;
        }
        
        public static int Main(string[] args)
        {
            return new Program().Run(args);
        }
    }
}
