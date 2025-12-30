namespace SnakeGame;

public class Game
{
    private const int Width = 40;
    private const int Height = 20;

    private readonly Snake _p1; // Strzałki
    private readonly Snake _p2; // WASD
    private readonly Food _food;
    private readonly ConsoleRenderer _renderer;
        
    private bool _gameOver;
    private string _endMessage;

    public Game(string endMessage)
    {
        _endMessage = endMessage;
        _renderer = new ConsoleRenderer();
        _renderer.Setup(Width, Height);

        // P1 (Zielony) startuje z lewej strony, idzie w prawo
        _p1 = new Snake(5, Height / 2, ConsoleColor.Green, Direction.Right);
            
        // P2 (Żółty/Cyan) startuje z prawej strony, idzie w lewo
        _p2 = new Snake(Width - 6, Height / 2, ConsoleColor.Cyan, Direction.Left);

        _food = new Food();
        _food.Generate(Width, Height, _p1, _p2);

        _renderer.DrawTotal(_p1, _p2, _food); 
    }

    public void Run()
    {
        while (!_gameOver)
        {
            Update();
            Thread.Sleep(100); 
        }
            
        _renderer.ShowGameOver(_endMessage);
    }

    private void Update()
    {
        // 1. Input dla obu graczy
        InputHandler.ProcessInput(_p1, _p2);

        // 2. Obliczamy przyszłe pozycje
        var nextP1 = _p1.CalculateNextPosition();
        var nextP2 = _p2.CalculateNextPosition();

        // 3. Sprawdzamy śmierć (Kolizje ze ścianami i ciałami)
        var p1Died = CheckDeath(nextP1, _p1) || CheckCollisionWithSnake(nextP1, _p2);
        var p2Died = CheckDeath(nextP2, _p2) || CheckCollisionWithSnake(nextP2, _p1);
            
        // Specjalny przypadek: Zderzenie czołowe (Head to Head)
        if (nextP1.X == nextP2.X && nextP1.Y == nextP2.Y)
        {
            p1Died = true;
            p2Died = true;
        }

        if (p1Died && p2Died)
        {
            _gameOver = true;
            _endMessage = "REMIS! Obaj zginęliście.";
            return;
        }
        if (p1Died)
        {
            _gameOver = true;
            _endMessage = "GRACZ 2 (Niebieski) WYGRYWA!";
            return;
        }
        if (p2Died)
        {
            _gameOver = true;
            _endMessage = "GRACZ 1 (Zielony) WYGRYWA!";
            return;
        }

        // 4. Ruch i Jedzenie (dla P1)
        ProcessMove(_p1, nextP1);
            
        // 5. Ruch i Jedzenie (dla P2)
        ProcessMove(_p2, nextP2);

        // 6. Rysowanie zmian
        _renderer.DrawStep(_p1, _p2, _food);
    }

    private void ProcessMove(Snake snake, Position nextPos)
    {
        if (nextPos.X == _food.CurrentPosition.X && nextPos.Y == _food.CurrentPosition.Y)
        {
            snake.Grow(nextPos);
            // Generujemy jedzenie, uwzględniając oba węże
            _food.Generate(Width, Height, _p1, _p2); 
        }
        else
        {
            var tailToRemove = snake.Body.Last();
            snake.Move(nextPos);
            _renderer.ClearPoint(tailToRemove);
        }
    }

    // Sprawdza ścianę i własne ciało
    private bool CheckDeath(Position pos, Snake owner)
    {
        // Ściana
        if (pos.X <= 0 || pos.X >= Width - 1 || pos.Y <= 0 || pos.Y >= Height - 1) 
            return true;

        // Własne ciało (samobójstwo)
        // Skip(1) bo głowa zawsze jest w ciele, sprawdzamy czy weszliśmy w resztę
        // Uwaga: przy ruchu lista się jeszcze nie zaktualizowała, więc sprawdzamy Body
        if (owner.Body.Any(p => p.X == pos.X && p.Y == pos.Y))
            return true;

        return false;
    }

    // Sprawdza kolizję z INNYM wężem
    private bool CheckCollisionWithSnake(Position headPos, Snake otherSnake)
    {
        return otherSnake.Body.Any(p => p.X == headPos.X && p.Y == headPos.Y);
    }
}