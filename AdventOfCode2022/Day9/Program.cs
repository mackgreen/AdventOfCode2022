using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day9 : DayN {

        RopePhysics rope;

        static void Main( string[] args ) {
            Day9 prog = new Day9("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day9( string file ) {
            rope = new RopePhysics(file);
        }

        public override string Part1() {
            //return rope.execMovements(2).ToString();
            return "0";
        }

        public override string Part2() {
            return rope.execMovements(10).ToString();
        }
    }

    class RopePhysics {

        string[] movements;

        List<Knot> knots = new List<Knot>();

        int hX = 0;
        int hY = 0;
        int tX = 0;
        int tY = 0;

        public RopePhysics(string file) {
            movements = File.ReadAllLines(file);
        }

        public int execMovements(int numKnots) {

            HashSet<string> moves = new HashSet<string>();

            for (int i = 0; i < numKnots; i++) {
                knots.Add(new Knot());
            }
            moves.Add($"{knots[knots.Count - 1].x},{knots[knots.Count - 1].y}");

            foreach (string movement in movements) {
                for ( int i = 0; i < int.Parse(movement.Substring(1)); i++) {
                    updateHeadPosition(knots[0], movement[0]);

                    Console.Write($"{knots[0].x},{knots[0].y} : ");

                    for (int j = 1; j < knots.Count; j++) {
                        moveKnotWithReference(knots[j - 1], knots[j]);
                        Console.Write($"{knots[j].x},{knots[j].y} : ");
                    }
                    Console.WriteLine();
                    moves.Add($"{knots[knots.Count - 1].x},{knots[knots.Count - 1].y}");
                }
            }
            return moves.Count;
        }

        public void updateHeadPosition( Knot head, char dir ) {
            if (dir == 'R') { head.x++; }
            else if (dir == 'L') { head.x--; }
            else if (dir == 'U') { head.y++; }
            else if (dir == 'D') { head.y--; }
        }

        public void moveKnotWithReference(Knot head, Knot tail) {
            if ( Math.Abs(head.x - tail.x) <= 1 && Math.Abs(head.y - tail.y) <= 1 ) {
                return;
            }
            if ( head.x == tail.x ) {
                tail.y += (head.y - tail.y) / 2;
            }
            else if ( head.y == tail.y ) {
                tail.x += (head.x - tail.x) / 2;
            }
            else {
                if (Math.Abs(head.x - tail.x) == 2) {
                    tail.x += (head.x - tail.x) / 2;
                }
                else {
                    tail.x += (head.x - tail.x);
                }
                if (Math.Abs(head.y - tail.y) == 2) {
                    tail.y += (head.y - tail.y) / 2;
                }
                else {
                    tail.y += (head.y - tail.y);
                }
            }
        }

        public class Knot {
            public int x { get; set; } = 0;
            public int y { get; set; } = 0;
        }

    }

}