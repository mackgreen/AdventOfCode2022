using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day10 : DayN {

        string[] instructions;
        List<int> register = new List<int>();

        static void Main( string[] args ) {
            Day10 prog = new Day10("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day10( string file ) {
            instructions = File.ReadAllLines(file);
        }

        public override string Part1() {

            register.Add(1);

            foreach (string instruction in instructions) {
                if ( instruction.StartsWith("noop") ) {
                    register.Add(register[register.Count - 1]);
                }
                else {
                    register.Add(register[register.Count - 1]);
                    register.Add(register[register.Count - 1] + int.Parse(instruction.Substring(5)));
                }
            }

            int val = (register[19] * 20) + (register[59] * 60) + (register[99] * 100) + (register[139] * 140) + (register[179] * 180) + (register[219] * 220);

            return val.ToString();
        }

        public override string Part2() {

            Console.WriteLine("\n");

            for ( int i = 0; i < 240; i++ ) {
                int chk = i % 40;
                if ( register[i] == chk - 1 || register[i] == chk || register[i] == chk + 1 ) {
                    Console.Write("#");
                }
                else {
                    Console.Write(" ");
                }
                if ( i % 40 == 39 ) {
                    Console.WriteLine();
                }
            }

            return "";
        }
    }

}