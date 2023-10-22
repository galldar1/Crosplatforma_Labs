using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFilePath = "INPUT.TXT";

        int N, Q;
        if (File.Exists(inputFilePath))
        {
            try
            {
                string[] input = File.ReadAllText(inputFilePath).Split();
                N = int.Parse(input[0]);
                Q = int.Parse(input[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при зчитуванні даних з INPUT.TXT: " + ex.Message);
                return;
            }
        }
        else
        {
            Console.Write("Введіть значення N: ");
            N = int.Parse(Console.ReadLine());

            Console.Write("Введіть значення Q: ");
            Q = int.Parse(Console.ReadLine());
        }

        long[] dp = new long[6 * N + 1];
        dp[0] = 1;

        for (int i = 0; i < N; i++)
        {
            long[] newDp = new long[6 * N + 1];

            for (int j = 0; j < dp.Length; j++)
            {
                for (int k = 1; k <= 6; k++)
                {
                    if (j + k < newDp.Length)
                        newDp[j + k] += dp[j];
                }
            }

            dp = newDp;
        }

        long totalWays = (long)Math.Pow(6, N);

        double probability = (double)dp[Q] / totalWays;

        Console.WriteLine("Ймовірність: " + probability.ToString("F10"));

        try
        {
            File.WriteAllText("OUTPUT.TXT", probability.ToString("F10"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка при запису в OUTPUT.TXT: " + ex.Message);
        }
    }
}
    