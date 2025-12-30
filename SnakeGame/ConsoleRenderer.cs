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

        // Na Macu nie możemy wymusić rozmiaru okna kodem bez błędów.
        // Prosimy użytkownika o przygotowanie okna lub czyścimy to co jest.
        Console.Clear();
            
        // Rysujemy ramkę (opcjonalne, ale pomaga zobaczyć granice)
        DrawBorder();
    }

    // Metoda do narysowania całej planszy raz (na starcie)
    public void DrawTotal(Snake snake, Food food)
    {
        // Rysuj całego węża
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var part in snake.Body)
        {
            DrawPixel(part.X, part.Y, "O");
        }

        // Rysuj jedzenie
        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(food.CurrentPosition.X, food.CurrentPosition.Y, "@");

        Console.ResetColor();
    }

    // Metoda do aktualizacji (rysowanie tylko zmian) - dla wydajności
    public void DrawStep(Snake snake, Food food)
    {
        // 1. Rysuj nową głowę
        Console.ForegroundColor = ConsoleColor.Green;
        DrawPixel(snake.Head.X, snake.Head.Y, "O");

        // 2. Rysuj jedzenie (na wypadek gdyby się zmieniło lub zostało nadpisane)
        Console.ForegroundColor = ConsoleColor.Red;
        DrawPixel(food.CurrentPosition.X, food.CurrentPosition.Y, "@");

        Console.ResetColor();
    }

    public void ClearPoint(Position position)
    {
        DrawPixel(position.X, position.Y, " ");
    }
        
    public void ShowGameOver(int score)
    {
        Console.Clear();
        Console.WriteLine($"Game Over! Twój wynik: {score}");
    }

    // Pomocnicza metoda bezpieczna
    private void DrawPixel(int x, int y, string symbol)
    {
        // Zabezpieczenie: Nie rysuj poza oknem terminala
        if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
                    
                // WAŻNE: Po narysowaniu znaku, kursor przesuwa się w prawo.
                // Jeśli jesteśmy na krawędzi, może to spowodować nową linię.
                // Przesuwamy kursor w bezpieczne miejsce (poza planszę, np. 0,0) aby nie migał
                Console.SetCursorPosition(0, 0); 
            }
            catch
            {
                // Ignorujemy błędy rysowania (np. przy zmianie rozmiaru okna w trakcie gry)
            }
        }
    }
        
    private void DrawBorder()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        // Rysowanie prostej ramki, jeśli mieści się w oknie
        for (int x = 0; x <= _width; x++)
        {
            DrawPixel(x, _height, "#"); // Dolna ściana
            DrawPixel(x, 0, "#");       // Górna ściana
        }
        for (int y = 0; y <= _height; y++)
        {
            DrawPixel(0, y, "#");       // Lewa ściana
            DrawPixel(_width, y, "#");  // Prawa ściana
        }
        Console.ResetColor();
    }
}