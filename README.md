# .NET Core Docker Publisher
Publish your application to a Docker image using the .NET CLI

## Prerequisites

- [.NET CLI](https://github.com/dotnet/cli)
- [Docker](https://docker.io) 

## Use the .NET Core Docker Publisher
Setting up your project to use the Docker publisher is very easy. These steps should get you up and running in no time.

The tool will create a `Dockerfile` next to your published application and execute `docker build` to create an Docker image.

### NuGet Feed
I haven't pushed to the tool to the public nuget.org feed, but when RC2 hits the shelve I definitely will do that.
For now when you would like use the publisher you should add this myget feed to your project's `NuGet.config` file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    ...
    <add key="Lesierse" value="https://www.myget.org/F/lesierse/api/v3/index.json" />
    ...
  </packageSources>
</configuration>
```

### Change project.json
Next, you should change your `project.json` file and add the `dotnet-publish-docker` package to `tools` and add the `dotnet publish-docker` to the post-publish script.

```json
"tools": {
  "dotnet-publish-docker": "1.0.0-dev"
},
"scripts": {
  "postpublish": "dotnet publish-docker --base-image vlesierse/dotnet --publish-folder %publish:OutputPath%"
}
```

> Unfortunately the current .NET Core Docker image is an older alpha version from november 2015. Until the RC2 releases you could use `vlesierse/dotnet:latest` which has the latest version of the .NET Core.
It's also possible to create your own base Docker image and use this as `--base-image` parameter.

### Publish your project
Now you're able to publish your application and you should see it creates a Docker image for your.  

```bash
dotnet publish
```

### Run your Docker image
With the Docker CLI you should see your image and will be able to run it.

```bash
docker images
docker run --rm <appname>
```