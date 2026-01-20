namespace LearningDotnet.DependencyInjection
{
    public class LogToFile : IMyLogger
    {
        public void log(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("== Log To File ==");
        }
    }
}
