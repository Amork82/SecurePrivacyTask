# Secure Privacy Task

# Overview
This application is developed with a front-end to back-end architecture and utilizes MongoDB for data storage. It showcases CRUD operations for the customer table along with a simplified management of key GDPR requests.

# Features
Menu Navigation: The application consists of three menu items: Home, Users, and Binary Check.
- Home: This section outlines the main objectives of the task and describes the application's structure.
- Users: This section displays a list of users along with a customizable search filter based on various fields. It also includes functions to create, edit, and delete users, while adhering to GDPR consent and regulations.
- Binary Check: This section provides an input field to test the functionality of verifying a binary code according to the rules specified in the "Home" section.
- Testing Project: An additional project, SecurePrivacyTask.Server.Tests, is included, which contains classes and methods to validate the effectiveness of the "BinaryCheck" functionality.

# Front-End
The front-end is built using Angular with TypeScript. The application leverages the Redux library to manage its state efficiently. It interfaces with the back-end for all API calls through a proxy file.

# Back-End
The back-end is developed in C# .NET 8 with a basic architecture consisting of modules and services. These are properly registered in the pipeline and exposed through controllers. The input and output results are mapped using dedicated DTOs (Data Transfer Objects). The data storage management is handled by the MongoDB library.

# Getting Started
To get started, clone the repository and follow the setup instructions provided in the installation section.

# Installation
Clone the repository:

- git clone in Visual Studio
- Configure multiple Startup Project in the Solution node
- Run the application and access it in your browser.
### Attention! Upon launching the "client" application, please click the "Refresh" button in your browser to ensure that the backend has completed its startup after the frontend.
