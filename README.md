# .NET Core Docker Publisher
Publish your application to a Docker image using the .NET CLI

[![Build Status](https://travis-ci.org/vlesierse/dotnet-publish-docker.svg?branch=master)](https://travis-ci.org/vlesierse/dotnet-publish-docker)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/dotnet-publish-docker.svg?maxAge=2592000)](https://www.nuget.org/packages/dotnet-publish-docker)

## Prerequisites

- [.NET Core](http://dot.net)
- [Docker](https://docker.io) 

## Use the .NET Core Docker Publisher
Setting up your project to use the Docker publisher is very easy. These steps should get you up and running in no time.

The tool will create a `Dockerfile` next to your published application and execute `docker build` to create an Docker image.

### Change project.json
Next, you should change your `project.json` file and add the `dotnet-publish-docker` package to `tools` and add the `dotnet publish-docker` to the post-publish script.

```json
"tools": {
  "dotnet-publish-docker": "1.0.0-preview1"
},
"scripts": {
  "postpublish": "dotnet publish-docker --publish-folder %publish:OutputPath%"
}
```

> It's possible to create your own base Docker image and use this as `--base-image` parameter.

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
