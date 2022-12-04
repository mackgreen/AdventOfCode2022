using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day4 : DayN {

        Assignments assignments;

        static void Main( string[] args ) {
            Day4 prog = new Day4("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day4( string file ) {
            assignments = new Assignments(file);
        }

        public override string Part1() {
            return assignments.getAllFullyContained().ToString();
        }

        public override string Part2() {
            return assignments.getAllOverlapping().ToString();
        }
    }

    public class Assignments {

        string[] lines;
        
        public Assignments(string file) {
            lines = File.ReadAllLines(file);
        }

        public int getAllFullyContained() {
            int cnt = 0;

            foreach (string line in lines) {
                string[] parts = line.Split(new char[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (int.Parse(parts[0]) >= int.Parse(parts[2]) && int.Parse(parts[1]) <= int.Parse(parts[3])) {
                    cnt++;
                }
                else if (int.Parse(parts[2]) >= int.Parse(parts[0]) && int.Parse(parts[3]) <= int.Parse(parts[1])) {
                    cnt++;
                }

            }

            return cnt;
        }

        public int getAllOverlapping() {
            int cnt = 0;

            foreach (string line in lines) {
                string[] parts = line.Split(new char[] { '-', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if ( int.Parse(parts[0]) >= int.Parse(parts[2]) && int.Parse(parts[0]) <= int.Parse(parts[3])) {
                    cnt++;
                }
                else if ( int.Parse(parts[1]) >= int.Parse(parts[2]) && int.Parse(parts[1]) <= int.Parse(parts[3]) ) {
                    cnt++;
                }
                else if (int.Parse(parts[2]) >= int.Parse(parts[0]) && int.Parse(parts[2]) <= int.Parse(parts[1])) {
                    cnt++;
                }
                else if (int.Parse(parts[3]) >= int.Parse(parts[0]) && int.Parse(parts[3]) <= int.Parse(parts[1])) {
                    cnt++;
                }

            }

            return cnt;
        }

    }
}