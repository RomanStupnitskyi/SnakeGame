namespace SnakeGame;

public static class InputHandler
{
    public static void ProcessInput(Snake p1, Snake p2)
    {
        // Pętla odczytuje wszystkie klawisze z bufora,
        // dzięki temu jeśli wciśniesz jednocześnie 'W' i 'Strzałkę', gra obsłuży oba.
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            // STEROWANIE GRACZ 1 (Strzałki)
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (p1.CurrentDirection != Direction.Right) p1.CurrentDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (p1.CurrentDirection != Direction.Left) p1.CurrentDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    if (p1.CurrentDirection != Direction.Down) p1.CurrentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (p1.CurrentDirection != Direction.Up) p1.CurrentDirection = Direction.Down;
                    break;
                
                // STEROWANIE GRACZ 2 (WSAD)
                case ConsoleKey.A:
                    if (p2.CurrentDirection != Direction.Right) p2.CurrentDirection = Direction.Left;
                    break;
                case ConsoleKey.D:
                    if (p2.CurrentDirection != Direction.Left) p2.CurrentDirection = Direction.Right;
                    break;
                case ConsoleKey.W:
                    if (p2.CurrentDirection != Direction.Down) p2.CurrentDirection = Direction.Up;
                    break;
                case ConsoleKey.S:
                    if (p2.CurrentDirection != Direction.Up) p2.CurrentDirection = Direction.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}