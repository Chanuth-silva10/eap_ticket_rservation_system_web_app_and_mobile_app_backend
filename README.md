# EAP Ticket Reservation System Backend

Welcome to the EAP Ticket Reservation System Backend repository. This project provides the backend services required for the EAP Ticket Reservation System, supporting both web and mobile applications.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

This backend system is developed to handle the core functionalities of a ticket reservation platform, including user management, event scheduling, and ticket booking. The backend is implemented in C# and is containerized using Docker for ease of deployment and scalability.

## Features

- **User Authentication and Authorization**: Secure login and registration processes.
- **Event Management**: Create, update, and delete events.
- **Ticket Booking**: Book and manage reservations.
- **API Endpoints**: RESTful APIs for all major operations.
- **Docker Support**: Containerized deployment for consistent environments.

## Technologies Used

- **C#**: Core programming language.
- **.NET Core**: Framework for building the application.
- **Docker**: Containerization for deployment.
- **MySQL**: Database management system.

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/Chanuth-silva10/eap_ticket_rservation_system_web_app_and_mobile_app_backend.git
   cd eap_ticket_rservation_system_web_app_and_mobile_app_backend
   ```

2. Build the Docker images and start the containers:
   ```sh
   docker-compose up --build
   ```
   
3. The application will be available at `http://localhost:5000`


## Configuration
Configuration settings can be found and modified in the appsettings.json file. Ensure that the database connection string and other necessary configurations are correctly set.

## Running the Application
To run the application locally, execute the following command:
```sh
dotnet run
```

