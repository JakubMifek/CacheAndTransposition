using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Author: Jakub Mifek
 */

namespace Transposed
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool showTrans = args.Length > 0;

                using (var writer = Console.Out)
                using (var reader = Console.In)
                {
                    string line = reader.ReadLine();
                    var n = int.Parse(line.Split()[1]);
                    var m = n * n - 1;
                    var max = 0;
                    while (m > 0)
                    {
                        m /= 10;
                        max++;
                    }
                    var t = new string[max];
                    var x = "";

                    for (int i = 1; i < max; i++)
                    {
                        x += "0";
                        t[i] = x;
                    }

                    var array = new int[n, n];
                    for (int i = 0; i < n * n; i++)
                    {
                        array[i / n, i % n] = i;
                        if (showTrans)
                        {
                            writer.Write($"{t[max - i.Digits()]}{i}");
                            writer.Write((i + 1) % n == 0 ? i == n * n - 1 ? "\n" : ",\n" : ",");
                        }
                    }

                    var watch = new Stopwatch();
                    watch.Start();
                    while ((line = reader.ReadLine()) != "E")
                    {
                        var tokens = Array.ConvertAll(line.Split().Skip(1).ToArray(), int.Parse);
                        var tmp = array[tokens[0], tokens[1]];
                        array[tokens[0], tokens[1]] = array[tokens[2], tokens[3]];
                        array[tokens[2], tokens[3]] = tmp;
                    }
                    watch.Stop();
                    writer.WriteLine($"{n} {watch.ElapsedMilliseconds}");

                    if (showTrans)
                    {
                        for (int i = 0; i < n * n; i++)
                        {
                            writer.Write($"{t[max - array[i / n, i % n].Digits()]}{array[i / n, i % n]}");
                            writer.Write((i + 1) % n == 0 ? i == n * n - 1 ? "\n" : ",\n" : ",");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }
    }

    static class IntExtensions
    {
        public static int Digits(this int number)
        {
            int x = 10;
            int l = 1;
            while (x <= number)
            {
                l++;
                x *= 10;
            }

            return l;
        }
    }
}
