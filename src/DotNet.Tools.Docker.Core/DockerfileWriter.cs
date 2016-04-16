using System.Text;

namespace DotNet.Tools.Docker
{
    public class DockerfileWriter
    {
        private readonly string _baseImage;
        private readonly string _applicationName;
        
        public DockerfileWriter(string applicationName, string baseImage)
        {
            _baseImage = baseImage;
            _applicationName = applicationName;
        }
        public string Write()
        {
            var baseImage = _baseImage ?? "microsoft/dotnet:0.0.1-alpha";
            var dockerfile = new StringBuilder();
            
            dockerfile.AppendLine($"FROM {baseImage}");
            dockerfile.AppendLine("RUN mkdir -p /app");
            dockerfile.AppendLine("WORKDIR /app");
            dockerfile.AppendLine("COPY . /app");
           
            dockerfile.AppendLine($"ENTRYPOINT [\"dotnet\", \"/app/{_applicationName}.dll\"]");
            
            return dockerfile.ToString();
        }
    }
}