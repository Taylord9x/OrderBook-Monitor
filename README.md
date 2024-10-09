OrderBook Monitor
Overview

This project is a containerized system for monitoring the USDT/ZAR order book on VALR. It consists of a backend API built with FastAPI, which streams order book updates from the VALR websocket API, and a frontend React application that allows users to interactively query prices based on the USDT quantity.
Features

    Backend API:
        Streams order book updates from VALR.
        Provides a GET endpoint to calculate the ZAR price for a given USDT quantity.
        Implements a price calculation algorithm based on the order book data.

    Frontend Application:
        Simple React app with an input field for USDT quantities.
        Displays the corresponding ZAR price dynamically on each key press.

Prerequisites

    Docker
    Docker Compose

Project Structure
  OrderBook-Monitor/
├── OrderBook-Monitor-API/
│   ├── Dockerfile
│   └── ...
└── orderbook-monitor-client/
    ├── Dockerfile
    └── ...
├── docker-compose.yml
└── README.md

Getting Started

Follow these steps to set up and run the application:

    Navigate to the project directory: Open your terminal and go to the root directory where the docker-compose.yml file is located:
    cd ~/Repos/OrderBook-Monitor/

    Build the Docker images: Execute the following command to build the Docker images for both the API and the frontend:
    docker-compose build

    Start the application: Run the following command to start the application:
    docker-compose up

    Access the applications:

    Open your browser and navigate to http://localhost:3000/ to see the React frontend.
    Access the Swagger documentation for the API at http://localhost:5032

API Endpoints

    Get Price:
    GET /api/price/get-price?quantity={quantity}
    Returns the ZAR price for the specified quantity of USDT.

    Index Fund Calculation:
    POST /api/index-fund
    Accepts asset_cap, total_capital, and a list containing coin symbol, market cap, and price. Returns the allocation details for the fund.