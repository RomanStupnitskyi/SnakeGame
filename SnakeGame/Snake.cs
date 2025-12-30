namespace SnakeGame;

public class Snake(int startX, int startY)
{
	public List<Position> Body { get; private set; } = [new Position(startX, startY)];
	public Direction CurrentDirection { get; set; } = Direction.Right;

	public Position Head => Body.First();

	public Position CalculateNextPosition()
	{
		Position head = Head;
		Position next = new(head.X, head.Y);

		switch (CurrentDirection)
		{
			case Direction.Left: next.X--; break;
			case Direction.Right: next.X++; break;
			case Direction.Up: next.Y--; break;
			case Direction.Down: next.Y++; break;
		}
		return next;
	}

	public void Move(Position newHead)
	{
		Body.Insert(0, newHead);
		Body.RemoveAt(Body.Count - 1);
	}

	public void Grow(Position newHead)
	{
		Body.Insert(0, newHead);
	}
}