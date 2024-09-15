# Battleship_game
 Battleship_game
1. Overview
The Battleship game is a web-based application that allows two players (Human and Machine) to play against each other. The application consists of a frontend built with React and a backend API implemented with ASP.NET Core.

2. Architecture
Frontend:

React Components:

GameBoard: Manages game state, interacts with the backend, and updates the UI.
Grid: Displays the game grid and handles cell clicks.
ShipInfo: Shows the status of ships.
GameStatus: Displays the current game status and handles game start/reset.
State Management:

humanGrid: Represents the human player’s grid.
machineGrid: Represents the machine player’s grid.
currentTurn: Indicates whose turn it is.
humanShips and machineShips: Lists of ships for each player.
humanWaterPositions and machineWaterPositions: Lists of missed shots.
gameOver: Indicates if the game is over.
winner: Stores the winner’s name.
Backend:

ASP.NET Core API: Clean Architecture
BattleshipController: Handles API requests for starting a new game and making a turn.
BattleshipService: Contains the business logic for starting a new game and processing turns.
Services and Data:
IBattleshipService: Interface defining methods for game operations.
BattleshipService: Implements game logic such as ship placement and turn handling.
Domain Entities:
Board: Represents the game board with ships and their positions.
Ship: Represents a ship with size, name, and positions.
GridPosition: Represents a position on the grid.
MoveRequest: Represents a request to make a move.
MoveResult: Represents the result of a move.
Data Flow:

Start New Game:

The frontend sends a POST request to /api/Battleship/start.
The backend generates a new game board with ships for both players.
The game state is returned to the frontend, which updates the UI.
Take Turn:

The frontend sends a POST request to /api/Battleship/turn with the move details.
The backend processes the move, updates the game state, and checks for game over conditions.
The updated game state is returned to the frontend, which updates the UI.
3. Components and Interaction
GameBoard Component:
Initializes the game and manages game turns.
Handles firing at positions and updating the grid based on the result.
Grid Component:
Displays the grid and allows players to click cells to fire.
ShipInfo Component:
Displays information about ships, including hits and sunk status.
GameStatus Component:
Displays the current game status, including the turn and game over conditions.
4. Error Handling
Frontend: Handles errors during API calls and displays appropriate messages.
Backend: Handles exceptions and returns meaningful error responses.
Execution Steps Document
1. Setting Up the Development Environment
Frontend:

Ensure Node.js and npm are installed.
Navigate to the frontend project directory.
Install dependencies: npm install.
Start the development server: npm start.
Backend:

Ensure .NET SDK is installed.
Navigate to the backend project directory.
Restore dependencies: dotnet restore.
Start the application: dotnet run.
2. Running the Application
Start the Backend API:

Open a terminal and navigate to the backend directory.
Run the command: dotnet run.
The API will be available at https://localhost:44355 (or another configured port).
Start the Frontend Application:

Open a new terminal and navigate to the frontend directory.
Run the command: npm start.
The React application will be available at http://localhost:3000 (or another configured port).
3. Playing the Game
Start a New Game:

Open the frontend application in a web browser.
Click on "Start New Game" to initialize the game.
Take Turns:

Click on cells in the grid to fire at the opponent’s ships.
The game will automatically switch turns between the human player and the machine.
End Game:

The game will automatically end when all ships of one player are sunk.
The winner will be displayed, and you can start a new game if desired.
4. Testing
Unit Tests:

For backend testing, navigate to the test project directory.
Run tests using: dotnet test.
Integration Tests:

Ensure the backend API is running.
Navigate to the test project directory for integration tests.
Run tests using: dotnet test.
