# Linux Instructions

## Building The Application

1. Make sure you have .NET SDK installed on your version of Linux  https://docs.microsoft.com/en-us/dotnet/core/install/linux (this project uses 3.1)

2. Download this repository to the Linux machine and extract it to a convenient location

3. Navigate to the `TseExamples/ReportingOnLinux` folder in Visual Studio Code (or at the command line)

3. Open the `nuget.config` file and insert your Telerik username and password in the credentials section (if this is a shared machine, please set the environment variables instead)

4. Restore the NuGet Packages

    `dotnet restore`

5. Build and package the app using the publish command. 

    `dotnet publish -c release -r ubuntu.18.04-x64 --self-contained`
    
    I use the `ubuntu.18.04-x64` Runtime Identifier (RID), be sure to use the correct RID for yours. You can find a list of RIDs in the [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) for yours.

> You do not have to build the app on the Linux machine. You can just build the project on your Windows dev box with the `--self-contained flag`  and copy all the contents of `bin\Release\netcoreapp3.1\RID\` folder to the Linux machine. For example `dotnet publish -c release -r YOUR_RID --self-contained true`.

## Running the Application

Now that the project has been built, you can run the application.

1. Navigate to the build output folder: 

    `cd ReportingOnLinux/bin/Release/netcoreapp3.1/RID/`

2. Change the permissions of the executable

    `chmod 777 ./ReportingOnLinux`

3. Run the app!

    `./ReportingOnLinux`


    




