namespace SnakeGame;

public class Food
{
    public Position CurrentPosition { get; private set; }
    private readonly Random _random = new Random();

    public void Generate(int mapWidth, int mapHeight, Snake p1, Snake p2)
    {
        const int safeMinX = 1;
        var safeMaxX = mapWidth - 1; 
        const int safeMinY = 1;
        var safeMaxY = mapHeight - 1;

        do
        {
            var x = _random.Next(safeMinX, safeMaxX);
            var y = _random.Next(safeMinY, safeMaxY);
            CurrentPosition = new Position(x, y);
        } 
        // Sprawdzamy kolizję z P1 ORAZ P2
        while (IsOnSnake(p1) || IsOnSnake(p2));
    }

    private bool IsOnSnake(Snake snake)
    {
        return snake.Body.Any(p => p.X == CurrentPosition.X && p.Y == CurrentPosition.Y);
    }
}