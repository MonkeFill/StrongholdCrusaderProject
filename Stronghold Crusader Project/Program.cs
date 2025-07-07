namespace Stronghold_Crusader_Project
{
    public static class Program
    {
        public static Exception LatestException;
        public static void Main()
        {
            bool UseExceptionHandler = false; //If error handling should be with the default handler
            if (UseExceptionHandler)
            {
                try
                {
                    RunGame();
                }
                catch (Exception ExceptionError)
                {
                    EventLogger.LogEvent($"{ExceptionError.StackTrace} - {ExceptionError.Message}", EventLogger.LogType.Error); //Logging the error it has had and where
                    LatestException = ExceptionError; //Storing the exception
                }
            }
            else
            {
                RunGame();
            }
        }

        private static void RunGame()
        {
            using var game = new Game1();
            game.Run();
        }
    }
}
