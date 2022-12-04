using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {

    public class UI {

        static void Main( string[] args ) {

            string curDay = new DirectoryInfo("../../../../").GetDirectories().OrderByDescending(d => d.CreationTime).FirstOrDefault().Name;

            int d = int.Parse(curDay.Substring(3));

            Console.Write($"Day to execute (enter for {d}): ");

            string input = Console.ReadLine();

            if ( !String.IsNullOrEmpty(input) ) {
                d = int.Parse(input); 
            }

            var objType = Type.GetType($"AOC2022.Day{d}, Day{d}");

            DayN exProg = (DayN)Activator.CreateInstance(objType, $"../../../../Day{d}/example.txt");
            exProg.Debug = true;
            exProg.Run();

            Console.Write($"{Environment.NewLine}Anykey to run prog");
            Console.ReadKey(true);

            DayN prodProg = (DayN)Activator.CreateInstance(objType, $"../../../../Day{d}/input.txt");
            prodProg.Debug = false;
            prodProg.Run();

        }

    }

}
