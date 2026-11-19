using System;
using System.Threading.Tasks;

class Program
{
 
    private static readonly object _consoleLock = new object();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting async tasks");

        Task<int> task1 = printC(ConsoleColor.Red, 1, 60);
        Task<int> task2 = printC(ConsoleColor.Green, 1, 100);

        int[] results = await Task.WhenAll(task1, task2);

        Console.ResetColor();
        Console.WriteLine($"Task 1 iterations: {results[0]}");
        Console.WriteLine($"Task 2 iterations: {results[1]}");
    }

    static async Task<int> printC(ConsoleColor color, int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            lock (_consoleLock)
            {
                Console.ForegroundColor = color;
                Console.Write(i + " ");
                Console.ResetColor();
            }

            await Task.Delay(50);
        }

        return end - start + 1;
    }
}