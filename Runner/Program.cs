using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

/*
 * Author: Jakub Mifek
 */

namespace Runner
{
    class Program
    {

        static bool OnlyOnce = false;
        static bool ShowOutput = false;
        static bool ShowTransposition = false;
        static bool ToCache = true;
        static int StartSize = 55; // 29 - matice 10x10

        static void Main(string[] args)
        {


            var path = "../../../Release";
            var pars = new[] { "r", "n" }; // naive nebo recursive
            var cachePar = new[]
            {
                "64 64",
                "64 1024",
                "64 4096",
                "512 512",
                "4096 64"
            };

            var num = StartSize;
            using (var writer = File.CreateText("output.txt"))
            {
                if (!ToCache)
                    writer.WriteLine("N Time");

                while (true)
                {
                    Console.WriteLine($"N {num}");
                    foreach (var par in pars)
                        foreach (string cPar in cachePar)
                        {
                            var time = new Stopwatch();
                            var x = ToCache
                                ? $"\"{path}/CacheSim.exe\""
                                : $"\"{path}/../Transposed/bin/Release/Transposed.exe\"";

                            using (var process1 = new Process
                            {
                                StartInfo =
                            {
                                FileName = $"{path}/Transposition.exe",
                                Arguments = $"-{par} {num}",
                                RedirectStandardOutput = true,
                                RedirectStandardInput = false,
                                UseShellExecute = false
                            }
                            })
                            {

                                using (var process2 = new Process()
                                {
                                    StartInfo =
                                {
                                    FileName = x,
                                    Arguments = ToCache ? $"{cPar}" : $"{(ShowTransposition ? "show" : "")}",
                                    RedirectStandardOutput = true,
                                    RedirectStandardInput = true,
                                    UseShellExecute = false
                                }
                                })
                                {

                                    var y = new DataReceivedEventHandler((sender, eventArgs) =>
                                     {
                                         process2.StandardInput.WriteLine(eventArgs.Data);
                                     });
                                    process1.OutputDataReceived += y;
                                    process2.OutputDataReceived += (sender, eventArgs) => writer.WriteLine(eventArgs.Data);
                                    process2.Disposed += (sender, eventArgs) => process1.OutputDataReceived -= y;

                                    time.Start();
                                    process2.Start();
                                    process1.Start();
                                    process1.BeginOutputReadLine();
                                    process2.BeginOutputReadLine();
                                    process2.WaitForExit();
                                    time.Stop();
                                    if (!process1.HasExited)
                                        process1.Kill();

                                    if (process2.ExitCode != 0 || process1.ExitCode != 0 || OnlyOnce) // Mohla např dojít RAM :-O
                                        goto Finish;

                                }
                            }
                        }

                    num++;
                }

                Finish:
                Console.WriteLine("Exiting.");
            }
        }
    }
}
