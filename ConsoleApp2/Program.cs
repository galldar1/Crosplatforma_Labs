using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Шлях до файлу INPUT.TXT у корені проекту.
        string inputFilePath = "INPUT.TXT";

        // Зчитуємо дані з файлу INPUT.TXT або вводимо їх вручну.
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

        // Створюємо масив для зберігання кількості можливих варіантів для кожної суми від 0 до 6 * N.
        long[] dp = new long[6 * N + 1];
        dp[0] = 1;

        // Обчислюємо кількість можливих варіантів для кожної суми для N кидків кубика.
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

        // Обчислюємо загальну кількість можливих варіантів для N кидків кубика.
        long totalWays = (long)Math.Pow(6, N);

        // Обчислюємо ймовірність, що сума дорівнює Q.
        double probability = (double)dp[Q] / totalWays;

        // Виводимо результат.
        Console.WriteLine("Ймовірність: " + probability.ToString("F10"));

        // Записуємо результат у вихідний файл OUTPUT.TXT з округленням до 10 знаків після коми.
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
    