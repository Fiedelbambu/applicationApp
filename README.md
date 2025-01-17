# BewerbungsApp

BewerbungsApp is a cross-platform application built with .NET MAUI. It helps users manage their job applications efficiently by allowing them to record company details, track application statuses, and set interview dates.

## Features

- Add and manage job applications
- Track statuses (e.g., "Applied," "Rejected," "Accepted")
- Set and update interview dates
- Interactive table for displaying application data
- Responsive design optimized for Android and other platforms

## Screenshots

![App Screenshot Placeholder](https://via.placeholder.com/600x400?text=Screenshot+1)

## Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the following workloads:
  - .NET Multi-platform App UI (MAUI)

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/BewerbungsApp.git
   ```
2. Open the solution in Visual Studio 2022.
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Build and run the application:
   - Select the desired platform (e.g., Android Emulator).
   - Press **F5** or click **Start**.

## Usage

### Adding a New Application
1. Enter the company name and application date in the input fields.
2. Press the "Save" button to add the entry to the table.

### Updating an Entry
1. Select an entry from the table.
2. Choose an action from the displayed options (e.g., change interview date, mark as rejected).
3. Save the changes.

### Deleting an Entry
1. Select an entry from the table.
2. Choose "Delete" from the options.
3. Confirm the deletion.

## Project Structure

```
BewerbungsApp/
├── Resources/         # Assets like images and styles
│   ├── AppIcon/       # Application icons
│   ├── Images/        # General images (e.g., logo, CV icon)
├── Platforms/         # Platform-specific code
│   ├── Android/       # Android resources
│   ├── iOS/           # iOS resources
├── ViewModels/        # MVVM ViewModels
├── Views/             # XAML pages
├── Models/            # Data models
├── Services/          # Logic for data persistence
└── README.md          # Project documentation
```

## Technologies Used

- **.NET MAUI**: Cross-platform development
- **C#**: Programming language
- **XAML**: User interface design

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add your message here"
   ```
4. Push the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or feedback, feel free to reach out:

- **Email**: your-email@example.com
- **GitHub**: [yourusername](https://github.com/yourusername)

