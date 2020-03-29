using Kanski.Fitting.Core;

namespace Kanski.Fitting.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                System.Console.WriteLine("running " + i);
                Sample.Compute();
            }
        }
    }
}
