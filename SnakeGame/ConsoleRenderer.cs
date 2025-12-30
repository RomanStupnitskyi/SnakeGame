namespace SnakeGame;

public class ConsoleRenderer
{
	public void Setup(int width, int height)
	{
		Console.CursorVisible = false;
		Console.Title = "Snake Game - Modular";
		// Dodajemy margines bezpieczeństwa dla Windows Console (+1)
		Console.SetWindowSize(width + 1, height + 1); 
		Console.SetBufferSize(width + 1, height + 1);
	}

	public void Draw(Snake snake, Food food)
	{
		// Rysowanie Głowy
		Console.SetCursorPosition(snake.Head.X, snake.Head.Y);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write("O");

		// Rysowanie Jedzenia
		Console.SetCursorPosition(food.CurrentPosition.X, food.CurrentPosition.Y);
		Console.ForegroundColor = ConsoleColor.Red;
		Console.Write("@");

		// Czyszczenie ogona (hack wydajnościowy)
		// Normalnie czyścilibyśmy cały ekran, ale to miga. 
		// Tutaj Game.cs musi nam powiedzieć co wyczyścić, 
		// albo po prostu w tym prostym przykładzie czyścimy miejsce za ogonem ręcznie w logice gry,
		// LUB tutaj po prostu resetujemy kolor.
		Console.ResetColor();
	}

	// Metoda pomocnicza do usuwania ogona z ekranu
	public void ClearPoint(Position position)
	{
		Console.SetCursorPosition(position.X, position.Y);
		Console.Write(" ");
	}
	
	public void ShowGameOver(int score)
	{
		Console.Clear();
		Console.WriteLine($"Game Over! Twój wynik: {score}");
	}
}