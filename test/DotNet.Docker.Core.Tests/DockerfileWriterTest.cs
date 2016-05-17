using Xunit;

namespace DotNet.Tools.Docker.Tests
{
    public class DockerfileWriterTest
    {
        [Fact]
        public void ShouldUseBaseImage()
        {
            var writer = new DockerfileWriter("testapp", "baseimage");
            var result = writer.Write();
            Assert.StartsWith("FROM baseimage", result);
        }
        
        [Fact]
        public void ShouldUseApplicationName()
        {
            var writer = new DockerfileWriter("testapp", null);
            var result = writer.Write();
            Assert.Contains("ENTRYPOINT [\"dotnet\", \"/app/testapp.dll\"]", result);
        }
        
        [Fact]
        public void ShouldFallbackDefaultBaseImage()
        {
            var writer = new DockerfileWriter("testapp", null);
            var result = writer.Write();
            Assert.StartsWith("FROM microsoft/dotnet", result);
        }
    }
}