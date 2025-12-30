namespace SnakeGame;

public class Food
{
	public Position CurrentPosition { get; private set; }
	private readonly Random _random = new();

	public void Generate(int mapWidth, int mapHeight, Snake snake)
	{
		// Pętla while upewnia się, że jedzenie nie pojawi się na ciele węża
		do
		{
			CurrentPosition = new Position(_random.Next(0, mapWidth), _random.Next(0, mapHeight));
		} 
		while (snake.Body.Any(p => p.X == CurrentPosition.X && p.Y == CurrentPosition.Y));
	}
}