using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {

    public class UI {

        static void Main( string[] args ) {

            DayN prog;

            Console.Write("Day to execute: ");

            int d = int.Parse( Console.ReadLine() );
            var objType = Type.GetType($"AOC2022.Day{d}, Day{d}");

            Console.Write("Type any character for debug, hit return for prod: ");

            if ( Console.ReadLine() == String.Empty ) {
                prog = (DayN)Activator.CreateInstance(objType, $"../../../../Day{d}/input.txt");
                prog.Debug = false;
            }
            else {
                prog = (DayN)Activator.CreateInstance(objType, $"../../../../Day{d}/example.txt");
                prog.Debug = true;
            }

            prog.Run();
        }

    }

}
