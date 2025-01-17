# applicationApp
BewerbungsApp

BewerbungsApp is a cross-platform application built with .NET MAUI. It helps users manage their job applications efficiently by allowing them to record company details, track application statuses, and set interview dates.

Features

Add and manage job applications

Track statuses (e.g., "Applied," "Rejected," "Accepted")

Set and update interview dates

Interactive table for displaying application data

Responsive design optimized for Android and other platforms

Screenshots



Installation

Prerequisites

.NET 8 SDK

Visual Studio 2022 with the following workloads:

.NET Multi-platform App UI (MAUI)

Steps

Clone the repository:

git clone https://github.com/yourusername/BewerbungsApp.git

Open the solution in Visual Studio 2022.

Restore NuGet packages:

dotnet restore

Build and run the application:

Select the desired platform (e.g., Android Emulator).

Press F5 or click Start.

Usage

Adding a New Application

Enter the company name and application date in the input fields.

Press the "Save" button to add the entry to the table.

Updating an Entry

Select an entry from the table.

Choose an action from the displayed options (e.g., change interview date, mark as rejected).

Save the changes.

Deleting an Entry

Select an entry from the table.

Choose "Delete" from the options.

Confirm the deletion.

Project Structure

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

Technologies Used

.NET MAUI: Cross-platform development

C#: Programming language

XAML: User interface design

Contributing

Contributions are welcome! Please follow these steps:

Fork the repository.

Create a new branch:

git checkout -b feature/your-feature-name

Commit your changes:

git commit -m "Add your message here"

Push the branch:

git push origin feature/your-feature-name

Open a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for details.
