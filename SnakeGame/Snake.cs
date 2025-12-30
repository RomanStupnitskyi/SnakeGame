namespace SnakeGame;

public class Snake(int startX, int startY, ConsoleColor color, Direction startDir)
{
    public List<Position> Body { get; private set; } = [new Position(startX, startY)];
    public Direction CurrentDirection { get; set; } = startDir;
    public ConsoleColor Color { get; private set; } = color; // Nowość: Kolor węża

    public Position Head => Body.First();

    public Position CalculateNextPosition()
    {
        var head = Head;
        var next = new Position(head.X, head.Y);

        switch (CurrentDirection)
        {
            case Direction.Left: next.X--; break;
            case Direction.Right: next.X++; break;
            case Direction.Up: next.Y--; break;
            case Direction.Down: next.Y++; break;
            default:
                throw new ArgumentOutOfRangeException();
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