namespace SnakeGame;

public class Game
{
    private const int Width = 40;
    private const int Height = 20;

    private readonly Snake _snake;
    private readonly Food _food;
    private readonly ConsoleRenderer _renderer;
        
    private bool _gameOver;
    private int _score;

    public Game()
    {
        _snake = new Snake(Width / 2, Height / 2);
        _food = new Food();
        _renderer = new ConsoleRenderer();
            
        _renderer.Setup(Width, Height);
        _food.Generate(Width, Height, _snake);
    }

    public void Run()
    {
        while (!_gameOver)
        {
            Update();
            Thread.Sleep(100); // Kontrola prędkości
        }
            
        _renderer.ShowGameOver(_score);
    }

    private void Update()
    {
        // 1. Input
        _snake.CurrentDirection = InputHandler.GetDirection(_snake.CurrentDirection);

        // 2. Logic - Obliczanie przyszłości
        var nextPos = _snake.CalculateNextPosition();

        // Sprawdzenie kolizji ze ścianą
        if (nextPos.X < 0 || nextPos.X >= Width || nextPos.Y < 0 || nextPos.Y >= Height)
        {
            _gameOver = true;
            return;
        }

        // Sprawdzenie kolizji z ogonem
        if (_snake.Body.Any(p => p.X == nextPos.X && p.Y == nextPos.Y))
        {
            _gameOver = true;
            return;
        }

        // Sprawdzenie jedzenia
        if (nextPos.X == _food.CurrentPosition.X && nextPos.Y == _food.CurrentPosition.Y)
        {
            _snake.Grow(nextPos);
            _score += 10;
            _food.Generate(Width, Height, _snake);
            // Przy wzroście nie usuwamy ogona, więc wystarczy narysować nową głowę
            _renderer.Draw(_snake, _food);
        }
        else
        {
            var tailToRemove = _snake.Body.Last(); // Zapamiętaj ogon przed ruchem
            _snake.Move(nextPos);
                
            _renderer.Draw(_snake, _food);
            _renderer.ClearPoint(tailToRemove); // Usuń stary ogon z ekranu
        }
    }
}