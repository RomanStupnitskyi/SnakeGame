namespace SnakeGame;

public class Food
{
    public Position CurrentPosition { get; private set; }
    private readonly Random _random = new();

    public void Generate(int mapWidth, int mapHeight, Snake snake)
    {
        // POPRAWKA: Losujemy od 1 do (Max - 2).
        // Np. jeśli szerokość to 40, ściany są na 0 i 39.
        // Jedzenie może być na polach od 1 do 38.
        // Random.Next(min, max) -> max jest wykluczony (exclusive), więc dajemy mapWidth - 1
            
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
        while (snake.Body.Any(p => p.X == CurrentPosition.X && p.Y == CurrentPosition.Y));
    }
}