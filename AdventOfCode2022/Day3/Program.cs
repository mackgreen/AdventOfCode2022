using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day3 : DayN {

        Rucksack rucksack;

        static void Main( string[] args ) {
            Day3 prog = new Day3("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day3( string file ) {
            rucksack = new Rucksack(file);
        }

        public override string Part1() {
            return rucksack.parseContentPriorities().ToString();
        }

        public override string Part2() {
            return rucksack.parseGroupBadges().ToString();
        }
    }

    public class Rucksack {

        string[] lines;

        public Rucksack(string file) {
            lines = File.ReadAllLines(file);
    
        }

        public int parseContentPriorities() {

            int priority = 0;

            foreach (string line in lines) {
                string l = line.Substring(0, line.Length / 2);
                string r = line.Substring(line.Length / 2);

                IEnumerable<char> both = l.Intersect(r);

                if ( both.Count() == 1 ) {
                    char val = both.First();
                    if ( val >= 'a' ) {
                        priority += val - 'a' + 1;
                    }
                    else {
                        priority += val - 'A' + 27;
                    }
                }
            }

            return priority;
        }

        public int parseGroupBadges() {

            int badgeVal = 0;

            for ( int i = 0; i < lines.Length; i += 3 ) {
                IEnumerable<char> oneAndTwo = lines[i].Intersect(lines[i + 1]);
                IEnumerable<char> all = oneAndTwo.Intersect(lines[i + 2]);

                if (all.Count() == 1) {
                    char val = all.First();
                    if (val >= 'a') {
                        badgeVal += val - 'a' + 1;
                    }
                    else {
                        badgeVal += val - 'A' + 27;
                    }
                }

            }

            return badgeVal;
        }

    }

}