namespace LearningDotnet.DependencyInjection
{
    public class LogToDB : IMyLogger
    {
        public void log(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("== Log To Db ==");
        }
    }
}
