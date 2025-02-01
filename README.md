# Movie Management System

## Overview
Movie Management System is a **Windows Forms Application** built using **C#** that allows users to manage movie records. It provides functionalities such as adding, updating, deleting, navigating through movies, and persisting data using a JSON file.

## Features
- **Add a new movie** with details such as ID, Title, Genre, Release Year, and Director.
- **Update existing movie** records.
- **Delete a movie** from the collection.
- **Navigate through movies** using first, previous, next, and last buttons.
- **Save and load movies** using JSON storage.
- **Logging** with Log4net for debugging and tracking application events.

## Technologies Used
- **C# (.NET Framework 4.8)** – Windows Forms Application (WinForms)
- **Newtonsoft.Json** – For handling JSON serialization and deserialization.
- **Log4net** – For logging application events.
- **Windows Forms (WinForms)** – UI framework for creating desktop applications.

## Installation & Setup
### Prerequisites
- **.NET Framework 4.8** or higher
- **Visual Studio** (Recommended)
- **NuGet Package Manager**

### Clone the Repository
```sh
git clone https://github.com/PrashantG1234/MovieManagementSystem.git
cd MovieManagementSystem
```

### Install Dependencies
Ensure that the required NuGet packages are installed:
```sh
nuget restore
```
If packages are missing, manually install them using:
```sh
Install-Package Newtonsoft.Json
Install-Package log4net
```

## Configuration
### Log4net Setup
The application uses **log4net** for logging events. Ensure that the `App.config` file is correctly configured for logging:
```xml
<log4net>
    <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
        <file value="C:\Users\prashantkumar_gupta\source\repos\Epam.MovieManager\application.log" />
        <appendToFile value="true" />
        <rollingStyle value="Date" />
        <datePattern value="yyyy-MM-dd'.log'" />
        <staticLogFileName value="false" />
        <layout type="log4net.Layout.PatternLayout">
            <conversionPattern value="[%date{yyyy-MM-dd HH:mm:ss}] [%level] %message%newline" />
        </layout>
    </appender>
    <root>
        <level value="DEBUG" />
        <appender-ref ref="RollingFileAppender" />
    </root>
</log4net>
```

### Running the Application
1. Open the solution in **Visual Studio**.
2. Build the project using **Ctrl + Shift + B**.
3. Run the application using **F5** or **Start Debugging**.

## Usage
1. **Load Movies**: Click "Load Movies" to display the saved movie records.
2. **Add Movie**: Fill in the movie details and click "Add" to save a new movie.
3. **Update Movie**: Modify details of an existing movie and click "Update".
4. **Delete Movie**: Enter the movie ID and click "Delete".
5. **Navigate**: Use "First", "Previous", "Next", and "Last" buttons to browse movies.
6. **Save Movies**: Click "Save" to persist data to `movie.json`.

## File Structure
```
MovieManagementSystem/
│-- Epam.MovieManager.sln      # Solution file
│-- Epam.MovieManager.UI/      # Windows Forms UI project
│-- Epam.MovieManager.Application/ # Business logic and data management
│-- movie.json                 # JSON file for storing movie records
│-- App.config                 # Configuration file for log4net
│-- README.md                  # Project documentation
```

## Contribution
Contributions are welcome! If you want to improve this project:
1. Fork the repository.
2. Create a new branch (`feature-xyz`).
3. Commit your changes (`git commit -m 'Add feature XYZ'`).
4. Push to your branch (`git push origin feature-xyz`).
5. Open a pull request.

## License
This project is open-source and available under the **MIT License**.

---
**Developed by:** *Prashant*

