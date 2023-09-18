using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        int[] m1 = new int[20];
        int[] m2 = new int[20];
        int[] m3 = new int[30];

        // Заполняем массивы случайными числами
        for (int i = 0; i < m1.Length; i++)
        {
            m1[i] = random.Next(10, 101);  // Генерируем случайное число в диапазоне от 0 до 99
        }

        for (int i = 0; i < m2.Length; i++)
        {
            m2[i] = random.Next(10,101);  // Генерируем случайное число в диапазоне от -50 до 49
        }

        for (int i = 0; i < m3.Length; i++)
        {
            m3[i] = random.Next(10, 101); // Генерируем случайное число в диапазоне от 1000 до 1999
        }


        int count1 = CountMatch(m1, m3);
        int count2 = CountMatch(m2, m3);

        Console.WriteLine($"Колличество в m1: {count1}");
        Console.WriteLine($"Колличество в m2: {count2}");

        if (count1 > count2)
        {
            Console.WriteLine("m1 имеет болешее кол-во совпадений m2");
        }
        else if (count1 < count2)
        {
            Console.WriteLine("m2 имеет болешее кол-во совпадений m1");
        }
        else
        {
            Console.WriteLine("m1 и m2 имеют одинаковое кол-во совпадений");
        }

        Stopwatch stopWatch = new Stopwatch();

        // последовательно
        stopWatch.Start();
        int sequentialCount1 = CountMatch(m1, m3);
        int sequentialCount2 = CountMatch(m2, m3);
        stopWatch.Stop();

        Console.WriteLine($"Кол-во совпадений в m1 (последовательно): {sequentialCount1}");
        Console.WriteLine($"Кол-во совпадений в m2 (последовательно): {sequentialCount2}");
        Console.WriteLine($"Время последовательного выполнения: {stopWatch.ElapsedMilliseconds}ms");

        // параллельно
        stopWatch.Reset();
        stopWatch.Start();
        int parallelCount1 = CountMatchParallel(m1, m3);
        int parallelCount2 = CountMatchParallel(m2, m3);
        stopWatch.Stop();

        Console.WriteLine($"Кол-во совпадений m1 (параллельно): {parallelCount1}");
        Console.WriteLine($"Кол-во совпадений m2 (параллельно): {parallelCount2}");
        Console.WriteLine($"Время параллельного выполнения: {stopWatch.ElapsedMilliseconds}ms");

        if (parallelCount1 > parallelCount2)
        {
            Console.WriteLine("m1 имеет большее кол-во совпадений чем m2 (параллельно)");
        }
        else if (parallelCount1 < parallelCount2)
        {
            Console.WriteLine("m2 имеет большее кол-во совпадений чем m1 (параллельно)");
        }
        else
        {
            Console.WriteLine("m1 и m2 имею одинаковое кол-во совпадений (параллельно)");
        }
    }

    static int CountMatch(int[] arr1, int[] arr2)
    {
        return arr1.Count(x => arr2.Contains(x));
    }

    static int CountMatchParallel(int[] arr1, int[] arr2)
    {
        int matchCount = 0;

        Parallel.ForEach(arr1, x =>
        {
            if (arr2.Contains(x))
            {
                Interlocked.Increment(ref matchCount);
            }
        });

        return matchCount;
    }
}
