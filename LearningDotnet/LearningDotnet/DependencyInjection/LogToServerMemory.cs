namespace LearningDotnet.DependencyInjection
{
    public class LogToServerMemory : IMyLogger
    {
        public void log(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("== Log To Server Memory ==");
        }
    }
}
