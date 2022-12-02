using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {

    public class Day1 : DayN {

        Calories calories;


        static void Main( string[] args ) {
            Day1 prog = new Day1("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day1( string file ) {
            calories = new Calories(file);
        }

        public override string Part1() {
            return calories.getMaxCals().ToString();
        }

        public override string Part2() {
            return calories.getTopThreeCals().ToString();
        }
    }

    public class Calories {

        List<int> elves = new List<int>();

        public Calories( string file ) {
            string[] lines = File.ReadAllLines( file );

            elves.Add(0);

            foreach (string line in lines) {

                if (String.IsNullOrEmpty(line)) {
                    elves.Add(0);
                }
                else {
                    elves[elves.Count - 1] += int.Parse(line);
                }
            }

        }

        public int getMaxCals() {
            elves.Sort();
            return elves[elves.Count - 1];
        }

        public int getTopThreeCals() {
            elves.Sort();
            return elves[elves.Count - 1] + elves[elves.Count - 2] + elves[elves.Count - 3];
        }
    }

}