namespace SnakeGame;

public class ConsoleRenderer
{
    private int _width;
    private int _height;

    public void Setup(int width, int height)
    {
        _width = width;
        _height = height;
        Console.CursorVisible = false;
        Console.Clear();
        DrawBorder();
    }

    public void DrawTotal(Snake p1, Snake p2, Food food)
    {
        DrawSnake(p1);
        DrawSnake(p2);

        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(food.CurrentPosition.X, food.CurrentPosition.Y, "@");
        Console.ResetColor();
    }

    public void DrawStep(Snake p1, Snake p2, Food food)
    {
        // Rysujemy tylko głowy (optymalizacja)
        Console.ForegroundColor = p1.Color;
        DrawPixel(p1.Head.X, p1.Head.Y, "O");

        Console.ForegroundColor = p2.Color;
        DrawPixel(p2.Head.X, p2.Head.Y, "O");

        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(food.CurrentPosition.X, food.CurrentPosition.Y, "@");
        Console.ResetColor();
    }

    private void DrawSnake(Snake snake)
    {
        Console.ForegroundColor = snake.Color;
        foreach (var part in snake.Body)
        {
            DrawPixel(part.X, part.Y, "O");
        }
    }

    public void ClearPoint(Position position)
    {
        DrawPixel(position.X, position.Y, " ");
    }
        
    public void ShowGameOver(string winnerMessage)
    {
        Console.Clear();
        Console.WriteLine("GAME OVER!");
        Console.WriteLine(winnerMessage);
    }

    private void DrawPixel(int x, int y, string symbol)
    {
        if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
                Console.SetCursorPosition(0, 0); 
            }
            catch { }
        }
    }
        
    private void DrawBorder()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        for (var x = 0; x < _width; x++)
        {
            DrawPixel(x, 0, "#");
            DrawPixel(x, _height - 1, "#");
        }
        for (var y = 0; y < _height; y++)
        {
            DrawPixel(0, y, "#");
            DrawPixel(_width - 1, y, "#");
        }
        Console.ResetColor();
    }
}