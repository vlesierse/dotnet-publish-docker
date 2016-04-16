FROM buildpack-deps:trusty-scm

# Work around https://github.com/dotnet/cli/issues/1582 until Docker releases a
# fix (https://github.com/docker/docker/issues/20818). This workaround allows
# the container to be run with the default seccomp Docker settings by avoiding
# the restart_syscall made by LTTng which causes a failed assertion.
ENV LTTNG_UST_REGISTER_TIMEOUT 0

RUN apt-get update && apt-get install libicu52 liblldb-3.6 liblttng-ust0 libunwind8 clang-3.5 -y  \
    && curl -s https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-host-ubuntu-x64.latest.deb -o dotnet-host-ubuntu-x64.latest.deb \
    && curl -s https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-sharedframework-ubuntu-x64.latest.deb -o dotnet-sharedframework-ubuntu-x64.latest.deb \
    && curl -s https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-sdk-ubuntu-x64.latest.deb -o dotnet-sdk-ubuntu-x64.latest.deb \
    && dpkg -i dotnet-host-ubuntu-x64.latest.deb \
    && dpkg -i dotnet-sharedframework-ubuntu-x64.latest.deb \
    && dpkg -i dotnet-sdk-ubuntu-x64.latest.deb \
    && rm *.deb
    