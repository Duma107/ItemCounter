# Advanced Interactive Item Counter with Web API
**Author:** Dumisani Nxumalo

## Description
A comprehensive C# application that counts occurrences of items across multiple data types. Features both an interactive console interface and a RESTful Web API with Swagger documentation. Supports strings, integers, doubles, characters, booleans, and dates with intelligent parsing and real-time processing.

## Features
- **Dual-Mode Operation**: Console application or Web API server
- **Multi-Type Data Support**: Handles 6 different data types seamlessly
- **RESTful Web API**: Full HTTP API with Swagger/OpenAPI documentation
- **Interactive Console**: Intuitive numbered menu for direct usage
- **Intelligent Input Parsing**: Smart conversion with comprehensive error handling
- **Flexible Boolean Input**: Accepts multiple boolean representations
- **Date Format Support**: Handles common date formats with validation
- **Generic LINQ-Powered Engine**: Efficient counting using modern C# features
- **CORS Support**: Cross-origin requests enabled for web integration
- **Health Monitoring**: Built-in health check endpoint

## Supported Data Types
| Type | Description | Input Examples |
|------|-------------|----------------|
| Strings/Words | Text separated by spaces | `apple banana apple orange` |
| Integers | Whole numbers | `1 2 3 2 1 4 5` |
| Doubles | Decimal numbers | `3.14 2.5 3.14 1.0 2.7` |
| Characters | Individual characters | `hello world` |
| Booleans | True/false values | `true false yes no 1 0` |
| Dates | Date values | `01/15/2024 2024-12-25 03/10/2023` |

## Requirements
- .NET 8.0 SDK or higher
- ASP.NET Core runtime (for Web API mode)
- Console/Terminal access
- Port 5000/5001 available (for Web API)

## Installation & Setup

### 1. Clone/Download Project
```bash
# Clone the repository (if using Git)
git clone <repository-url>
cd ItemCounter

# Or download and extract the project files
```

### 2. Verify .NET Installation
```bash
dotnet --version
# Should show .NET 8.0 or higher
```

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Build the Project
```bash
dotnet build
```

## Usage

### Console Mode (Default)
Run the interactive console application:
```bash
dotnet run
```

The program presents a comprehensive menu:
```
=== Item Counter ===
1. Count words/strings
2. Count integers
3. Count doubles
4. Count characters
5. Count booleans
6. Count dates
7. Start Web API
8. Exit
Choose an option (1-8):
```

### Web API Mode
Start the Web API server:
```bash
dotnet run --api
```

The API will be available at:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: http://localhost:5000/swagger

### API Endpoints
- `GET /` - Welcome message and API information
- `GET /api/itemcounter/health` - Health check endpoint
- `POST /api/itemcounter/count-strings` - Count string occurrences
- `POST /api/itemcounter/count-integers` - Count integer occurrences
- `POST /api/itemcounter/count-doubles` - Count double occurrences
- `POST /api/itemcounter/count-characters` - Count character occurrences
- `POST /api/itemcounter/count-booleans` - Count boolean occurrences
- `POST /api/itemcounter/count-dates` - Count date occurrences

## Console Usage Examples

### Count Words/Strings
```
Enter words/strings separated by spaces:
apple banana apple orange banana apple grape

Counting strings:
apple: 3 occurrence(s)
banana: 2 occurrence(s)
orange: 1 occurrence(s)
grape: 1 occurrence(s)
```

### Count Characters
```
Enter text (each character will be counted):
programming

Counting characters:
p: 1 occurrence(s)
r: 2 occurrence(s)
o: 1 occurrence(s)
g: 2 occurrence(s)
a: 1 occurrence(s)
m: 2 occurrence(s)
i: 1 occurrence(s)
n: 2 occurrence(s)
```

### Count Booleans (Flexible Input)
```
Enter boolean values separated by spaces (true/false, yes/no, 1/0):
true false true yes no 1 0 true

Counting booleans:
True: 4 occurrence(s)
False: 4 occurrence(s)
```

## Deployment Options

### 1. Self-Contained Deployment
Create a standalone executable that includes the .NET runtime:

```bash
# Windows
dotnet publish -c Release -r win-x64 --self-contained -o ./publish

# Linux
dotnet publish -c Release -r linux-x64 --self-contained -o ./publish

# macOS
dotnet publish -c Release -r osx-x64 --self-contained -o ./publish
```

### 2. Framework-Dependent Deployment
Smaller deployment that requires .NET runtime on target machine:

```bash
dotnet publish -c Release -o ./publish
```

### 3. Docker Deployment
Create a `Dockerfile`:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ItemCounter.csproj", "."]
RUN dotnet restore "./ItemCounter.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ItemCounter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItemCounter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItemCounter.dll", "--api"]
```

Build and run:
```bash
docker build -t itemcounter-api .
docker run -p 8080:80 itemcounter-api
```

### 4. Azure App Service Deployment
```bash
# Install Azure CLI
az login

# Create resource group
az group create --name ItemCounterRG --location "East US"

# Create App Service plan
az appservice plan create --name ItemCounterPlan --resource-group ItemCounterRG --sku B1 --is-linux

# Create web app
az webapp create --resource-group ItemCounterRG --plan ItemCounterPlan --name YourAppName --runtime "DOTNETCORE|8.0"

# Deploy
dotnet publish -c Release
cd bin/Release/net8.0/publish
zip -r ../publish.zip .
az webapp deployment source config-zip --resource-group ItemCounterRG --name YourAppName --src ../publish.zip
```

### 5. IIS Deployment (Windows)
1. Install IIS with ASP.NET Core Module
2. Publish the application:
   ```bash
   dotnet publish -c Release -o C:\inetpub\wwwroot\ItemCounter
   ```
3. Create IIS site pointing to the publish folder
4. Ensure Application Pool uses "No Managed Code"

### 6. Linux Server Deployment (Ubuntu/Debian)
```bash
# Install .NET runtime
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-8.0

# Deploy application
sudo mkdir /var/www/itemcounter
sudo cp -r ./publish/* /var/www/itemcounter/
sudo chown -R www-data:www-data /var/www/itemcounter

# Create systemd service
sudo nano /etc/systemd/system/itemcounter.service
```

Service file content:
```ini
[Unit]
Description=Item Counter API
After=network.target

[Service]
Type=forking
User=www-data
Group=www-data
WorkingDirectory=/var/www/itemcounter
ExecStart=/usr/bin/dotnet /var/www/itemcounter/ItemCounter.dll --api
Restart=on-failure
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=itemcounter
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

Start the service:
```bash
sudo systemctl enable itemcounter
sudo systemctl start itemcounter
sudo systemctl status itemcounter
```

## Configuration

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Set to `Production` for production deployment
- `ASPNETCORE_URLS`: Configure binding URLs (default: `http://localhost:5000;https://localhost:5001`)

### Production Settings
For production deployment, consider:
- Enable HTTPS with proper certificates
- Configure logging (Serilog, NLog, etc.)
- Add authentication/authorization if needed
- Implement rate limiting
- Set up monitoring and health checks
- Configure reverse proxy (Nginx, Apache)

## Technical Architecture

### Core Components
- **Generic Counter Method**: Universal `CountOccurrences<T>` using LINQ
- **Web API Controllers**: RESTful endpoints with Swagger documentation
- **Type-Specific Services**: Dedicated parsing and processing logic
- **Dual-Mode Startup**: Console or Web API based on command line arguments
- **CORS Support**: Cross-origin resource sharing enabled
- **Error Handling**: Comprehensive exception handling and validation

### Performance Features
- **LINQ Optimization**: Efficient grouping and counting
- **Memory Management**: Single-pass data processing
- **Type Safety**: Generic constraints prevent runtime errors
- **Async/Await**: Non-blocking operations in Web API mode

## Project Structure
```
ItemCounter/
├── Program.cs              # Main application entry point
├── Controllers/            # Web API controllers
├── Services/              # Business logic services
├── Models/                # Data models and DTOs
├── ItemCounter.csproj     # Project configuration
├── README.md             # This documentation
└── Dockerfile            # Docker configuration (optional)
```

## Monitoring & Health Checks
- Health check endpoint: `/api/itemcounter/health`
- Built-in Swagger documentation at `/swagger`
- Comprehensive error logging and handling
- CORS support for web integration

## Use Cases
- **Data Analysis**: Quick frequency analysis of datasets
- **Web Services**: API integration for counting operations
- **Quality Assurance**: Validate data consistency
- **Educational**: Learn about data types and LINQ
- **Microservices**: Lightweight counting service
- **Research**: Statistical analysis of categorical data

## Version History
- **v3.0**: Added Web API mode with Swagger documentation
- **v2.0**: Added doubles, booleans, and dates support
- **v1.1**: Enhanced user interface and error handling
- **v1.0**: Initial release with strings, integers, and characters

## Author
**Dumisani Nxumalo**  
Software Developer

## License
This project is for educational and demonstration purposes.

*Last updated: June 2025*
