using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("INPUT.txt");

        if (lines.Length > 0)
        {
            if (int.TryParse(lines[0], out int n))
            {
                long result = FindNthSmoothNumber(n);

                using (StreamWriter outputFile = new StreamWriter("OUTPUT.TXT"))
                {
                    outputFile.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("Невірний формат введення в файлі INPUT.txt.");
            }
        }
        else
        {
            Console.WriteLine("Файл INPUT.txt порожній.");
        }
    }

    static long FindNthSmoothNumber(int n)
    {
        long number = 1;
        int count = 0;

        while (count < n)
        {
            if (IsSmooth(number))
            {
                count++;
            }
            number++;
        }

        return number - 1;
    }

    static bool IsSmooth(long number)
    {
        string numStr = number.ToString();

        for (int i = 0; i < numStr.Length - 1; i++)
        {
            if (numStr[i] > numStr[i + 1])
            {
                return false;
            }
        }

        return true;
    }
}
