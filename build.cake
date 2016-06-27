//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// Configuration
//////////////////////////////////////////////////////////////////////

var artifactsDir = GetDirectories("./artifacts");
var buildOutputDir = GetDirectories("./**/bin").Concat(GetDirectories("./**/obj"));

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	foreach (var directory in artifactsDir.Concat(buildOutputDir))
	{
		if(DirectoryExists(directory)) DeleteDirectory(directory, true);
	}
});

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreRestore();
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreBuild("src/*/project.json");
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var directories = GetDirectories("./test/*.Test");
    foreach(var directory in directories)
    {
        DotNetCoreTest(directory.ToString());
    }
});

Task("Package")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var directories = GetDirectories("./src/*");
    foreach(var directory in directories)
    {
        DotNetCorePack(directory.ToString(), new DotNetCorePackSettings()
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts"
        });
    }
    
});

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);