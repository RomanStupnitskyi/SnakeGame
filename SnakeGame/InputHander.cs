namespace SnakeGame;

public static class InputHandler
{
	public static Direction GetDirection(Direction currentDirection)
	{
		if (!Console.KeyAvailable) return currentDirection;

		ConsoleKey key = Console.ReadKey(true).Key;

		return key switch
		{
			ConsoleKey.LeftArrow => currentDirection != Direction.Right ? Direction.Left : currentDirection,
			ConsoleKey.RightArrow => currentDirection != Direction.Left ? Direction.Right : currentDirection,
			ConsoleKey.UpArrow => currentDirection != Direction.Down ? Direction.Up : currentDirection,
			ConsoleKey.DownArrow => currentDirection != Direction.Up ? Direction.Down : currentDirection,
			_ => currentDirection,
		};
	}
}