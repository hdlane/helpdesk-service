# Helpdesk Service

## Description

Helpdesk Service is a Windows service built in C# that queries devices for information which is then sent to a configured endpoint. It is intended to be used in conjunction with the [Helpdesk Tool](https://github.com/hdlane/helpdesk-tool) and [Helpdesk API](https://github.com/hdlane/helpdesk-api).

### Goal

The goal of the Helpdesk Service is to get information from computers and in front of helpdesk teams to make support quick and easy.

### Background

While developing the Helpdesk Tool, I wanted to get as close to live data as possible with Windows computers on the network. This prompted me to develop a Windows service that sends that information to an endpoint more frequently and only when the computer's information changed.

### Features

* Query computer information periodically that is sent to an endpoint only when information changes
* Install as a Windows service that will run no matter who is logged in
* Logging is sent to the Windows Event Viewer

## Installation

### Requirements

#### To Install Service

* Windows OS x64 or Arm64
* The application - Can be downloaded from [Releases](https://github.com/hdlane/helpdesk-service/releases)
* .NET 8.0 Desktop Runtime to run application
    * You will be prompted during the first launch of the app to download the runtime, or you can download below
    * [x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.8-windows-x64-installer?cid=getdotnetcore)
    * [Arm64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.8-windows-arm64-installer?cid=getdotnetcore)

#### To Develop

* Windows OS
* .NET SDK 8.0
* .NET IDE like [Visual Studio Community Edition](https://visualstudio.microsoft.com/vs/community/)
    * Wix Toolset - `dotnet tool install --global wix`
    * [HeatWave for VS2022](https://marketplace.visualstudio.com/items?itemName=FireGiant.FireGiantHeatWaveDev17) extension installed from the Visual Studio Installer
* NuGet Packages (.NET 8.0 versions)
    * Microsoft.Extensions.Hosting
    * Microsoft.Extensions.Hosting.WindowsServices
    * System.Management

#### To Build Installer

* Open the project in Visual Studio
* Right click on the HelpdeskService project, and click Publish
    * If prompted for publish settings, select Folder as target, leave default Location, and then Publish
* Right click on the installer project, and click Build
    * You can find the installer where you cloned the repo, then going to the installer folder

## Usage

The Helpdesk Service is installed with the MSI file. It is installed as a Windows service that auto-starts when Windows boots. Every minute, the service will gather the following computer information:

* Computer name
* MAC address
* Username of current user
* IP Address
* Windows OS information
* Drive free space
* Manufacturer and model
* Uptime since last reboot

Data will only be reported if there are changes in the computer name, username, or IP address. The service will also report when it first starts.

### How to change API endpoint

There is an appsettings.json file installed under C:\Program Files\Helpdesk\Helpdesk Service where you can adjust the endpoint. Update the "Server" key and then restart the service.

### Error Logging

Log entries like application startup / shutdown and errors are logged in Event Viewer under the Application category. You can find entries like a Warning if the endpoint is unreachable and other messages like that.

## License

[MIT](https://choosealicense.com/licenses/mit/)
