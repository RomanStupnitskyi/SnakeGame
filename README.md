# Snake Game - Two Player Console Game

A classic two-player Snake game built with C# and .NET 9, featuring competitive gameplay where two snakes battle for food and dominance in a shared console arena.

## 📋 Project Overview

This is a console-based multiplayer Snake game where two players control their own snakes simultaneously. The game features collision detection, food spawning, and multiple win conditions including draws.

### Key Features
- **Two-Player Gameplay**: Simultaneous multiplayer with independent snake controls
- **Real-time Collision Detection**: Wall, body, and head-to-head collision detection
- **Dynamic Food Spawning**: Food randomly appears on the game board, avoiding snakes
- **Color-Coded Snakes**: Player 1 (Green) and Player 2 (Cyan) snakes are visually distinct
- **Win Conditions**: Includes player victory, opponent defeat, and draw scenarios
- **Responsive Controls**: Support for both arrow keys and WASD controls

## 🏗️ Project Structure

```
SnakeGame/
├── Program.cs              # Application entry point
├── Game.cs                 # Main game loop and logic controller
├── Snake.cs                # Snake entity and movement logic
├── Food.cs                 # Food generation and collision
├── Direction.cs            # Direction enumeration (Up, Down, Left, Right)
├── Position.cs             # Position struct for coordinates
├── ConsoleRenderer.cs      # Console output and rendering
├── InputHander.cs          # Input processing (note: typo in class name)
├── SnakeGame.csproj        # Project configuration
└── SnakeGame.sln           # Solution file
```

## 🏛️ Architecture & Design

### Core Components

#### 1. **Program.cs**
Entry point of the application. Creates a Game instance and initiates the game loop.

```csharp
var game = new Game("Game finished");
game.Run();
```

#### 2. **Game.cs** - Main Game Controller
Orchestrates the entire game logic including:
- **Game State Management**: Tracks game over status and end messages
- **Game Loop**: Runs at 100ms intervals for consistent gameplay speed
- **Physics & Collision**: Manages snake movement, collision detection, and win condition checks
- **Player Management**: Maintains both snakes and food state

**Key Methods**:
- `Run()`: Main game loop that updates and renders until game over
- `Update()`: Processes input, movement, collisions, and game state changes
- `ProcessMove()`: Handles snake movement and food consumption
- `CheckDeath()`: Validates wall collision
- `CheckCollisionWithSnake()`: Validates body collision with opponent

**Game Configuration**:
- Board Size: 40x20 characters
- Game Speed: 100ms per frame
- Starting Positions: P1 at (5, 10) facing right, P2 at (34, 10) facing left

#### 3. **Snake.cs** - Snake Entity
Represents a single snake with:
- **Body Management**: List of Position objects representing the snake's body
- **Direction Control**: Current movement direction
- **Appearance**: Color coding (Green for P1, Cyan for P2)
- **Head Property**: Quick access to the first body segment

**Key Methods**:
- `CalculateNextPosition()`: Computes the next head position based on current direction
- `Move(Position)`: Moves snake by removing tail and adding new head
- `Grow(Position)`: Adds a new body segment (used when eating food)

**Physics**:
- Snake wraps around and updates in place
- Prevents 180-degree turns (can't reverse directly into itself)

#### 4. **Food.cs** - Food Management
Handles food generation and collision detection:
- **Random Spawning**: Uses Random class to generate food coordinates
- **Collision Avoidance**: Ensures food doesn't spawn on either snake's body
- **Safe Zone**: Food spawns within boundaries (1 to width-2, 1 to height-2)

**Key Method**:
- `Generate()`: Spawns food at a valid random position, re-rolling if it collides with snakes

#### 5. **Position.cs** - Coordinate Structure
Simple struct for storing X, Y coordinates with mutable properties.

#### 6. **Direction.cs** - Movement Enumeration
Defines four cardinal directions: Up, Down, Left, Right

#### 7. **ConsoleRenderer.cs** - Display System
Manages all console rendering operations:
- **Initialization**: Sets up console, hides cursor, draws borders
- **Rendering**: Draws snakes, food, and borders
- **Optimization**: Updates only changed elements (heads) after initial draw

**Key Methods**:
- `Setup()`: Initializes the console display and draws border
- `DrawTotal()`: Initial full render of all game objects
- `DrawStep()`: Optimized frame updates (only renders heads for performance)
- `DrawBorder()`: Renders the game board boundary
- `ShowGameOver()`: Displays end game message

**Visual Elements**:
- `#` - Border walls
- `O` - Snake body
- `@` - Food
- ` ` - Empty space

#### 8. **InputHander.cs** - Input Processing
Handles keyboard input for both players:
- **Non-blocking Input**: Reads all available keys in buffer without waiting
- **Player 1 Controls**: Arrow keys (↑ ↓ ← →)
- **Player 2 Controls**: WASD keys (W for up, A for left, S for down, D for right)
- **Direction Validation**: Prevents snakes from reversing into themselves (e.g., can't go left if moving right)

## 🎮 Gameplay Rules

### Win Conditions
1. **Player 1 Victory**: Player 2 dies (wall collision, body collision, or head-to-head)
2. **Player 2 Victory**: Player 1 dies
3. **Draw (Remis)**: Both snakes die simultaneously (head-to-head collision)
4. **Game Over**: Triggered immediately when any win condition is met

### Collision Detection
- **Wall Collision**: Snake touches board boundary
- **Body Collision**: Snake head touches opponent's body
- **Head-to-Head**: Both snakes move to the same position in the same frame (mutual death)
- **Food Collision**: Snake consumes food → snake grows by 1 segment

### Movement Mechanics
- Snakes move simultaneously in the same game tick
- Movement is calculated before collision detection
- Snake body follows head movement (FIFO queue)
- Eating food adds segment without removing tail
- Regular movement removes tail

## 🕹️ Controls

| Player 1 (Green) | Player 2 (Cyan) | Action      |
|------------------|-----------------|-------------|
| ↑ Up Arrow       | W               | Move Up     |
| ↓ Down Arrow     | S               | Move Down   |
| ← Left Arrow     | A               | Move Left   |
| → Right Arrow    | D               | Move Right  |

## 🛠️ Technical Details

### Technology Stack
- **Language**: C# 12
- **Framework**: .NET 9.0
- **Platform**: Console Application
- **IDE Target**: Visual Studio / VS Code with C# extension

### Project Configuration
```xml
<TargetFramework>net9.0</TargetFramework>
<OutputType>Exe</OutputType>
<ImplicitUsings>enable</ImplicitUsings>
<Nullable>enable</Nullable>
```

### Namespace Structure
- All game classes are in `SnakeGame` namespace
- Global usings enabled for cleaner code

### Code Quality Features
- **Nullable Reference Types**: Enabled for safer null handling
- **Implicit Usings**: Reduces boilerplate import statements
- **Primary Constructors**: Used in Snake class (C# 12 feature)

## 🚀 Building & Running

### Prerequisites
- .NET 9.0 SDK or later
- C# 12 compatible compiler

### Build
```bash
dotnet build SnakeGame.sln
```

### Run
```bash
dotnet run --project SnakeGame/SnakeGame.csproj
```

### Or directly
```bash
./SnakeGame/bin/Debug/net9.0/SnakeGame
```

## 📊 Game Loop Sequence

1. **Input Processing** → Both players' keyboard input is processed
2. **Position Calculation** → Next head positions are calculated for both snakes
3. **Collision Detection** → Check for deaths (walls, bodies, head-to-head)
4. **Win Condition Check** → Determine if game should end
5. **Movement & Growth** → Move snakes, process food consumption
6. **Rendering** → Update display with current game state
7. **Frame Delay** → Wait 100ms before next iteration

## 🎨 Color Scheme

| Element | Color      | Character |
|---------|------------|-----------|
| Player 1 Snake | Green  | O         |
| Player 2 Snake | Cyan   | O         |
| Food    | Red        | @         |
| Border  | Gray       | #         |

## 🐛 Known Issues & Improvements

### Current Implementation
- **Input Handler Class Name**: `InputHander` has a typo (should be `InputHandler`)
- **Console Cursor**: Repositioned to (0,0) after each draw for consistency
- **Food Generation**: Uses do-while loop that could theoretically loop forever on crowded boards

### Potential Enhancements
- Add difficulty levels with adjustable speed
- Implement high score tracking
- Add pause functionality
- Support for AI players
- Network multiplayer
- Different game modes (survival, race, etc.)
- Customizable board sizes
- Sound effects
- Score tracking per round

## 📝 Code Standards

### Naming Conventions
- **Classes**: PascalCase (Game, Snake, Food)
- **Methods**: PascalCase (ProcessMove, CalculateNextPosition)
- **Fields**: camelCase with underscore prefix for private fields (_gameOver, _food)
- **Constants**: UPPER_CASE (Width, Height)
- **Enums**: PascalCase (Direction, ConsoleColor)

### Design Patterns
- **Game Loop Pattern**: Main game loop in Game.Run()
- **Model-View-Controller**: Separation between Game logic, Snake/Food models, and ConsoleRenderer view
- **Static Utility**: InputHandler as static class for state-independent input processing
- **Value Type**: Position as struct for lightweight coordinate storage

## 🧪 Testing Recommendations

- Test collision detection with edge cases (corners, walls)
- Verify simultaneous movement calculations
- Test food spawning on full board scenarios
- Validate input handling with rapid key presses
- Check win condition logic for all three outcomes

## 📄 License & Attribution

This is an educational project demonstrating core game development concepts including:
- Game loops and frame-based updates
- Collision detection systems
- Input handling and event processing
- Rendering and visualization
- State management in game logic

---

**Project Version**: 1.0  
**Last Updated**: December 2025  
**Author**: RomanStupnitskyi
