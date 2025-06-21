namespace NumberBaseballUsingDelegate
{
    public class NumberBaseballUsingDelegate
    {
        static void Main(string[] args)
        {
            PlayTheGame();
        }

        private static void PlayTheGame()
        {
            // Create a new instance of the game
            var game = new NumberBaseballGame();

            Console.Write("1 이상 9 이하의 정수를 입력해라: ");
            string? input = Console.ReadLine();
            int Ball_num;

            while (input == null || int.TryParse(input, out Ball_num) == false || Ball_num < 1 || Ball_num > 9)
            {
                Console.Write("1 이상 9 이하의 정수를 입력해라: ");
                input = Console.ReadLine();
            }

            // Start the game
            game.StartGame(Ball_num);

            Console.WriteLine("Press any key to exit...");
        }
    }
}