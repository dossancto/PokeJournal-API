# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy the file from your host to your current location.
COPY *.csproj ./

# Run the command inside your image filesystem.
RUN dotnet restore

# Copy the rest of your app's source code from your host to your image filesystem.
COPY . ./

# Build the app
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime image as a parent image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory.
WORKDIR /app

# Copy the build output from the previous stage.
COPY --from=build-env /app/out .

# Run the app on container startup.
ENTRYPOINT ["dotnet", "PokeJournal.dll"]
