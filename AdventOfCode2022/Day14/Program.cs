using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day14 : DayN {

        RegolithReservoir reservoir;

        static void Main( string[] args ) {
            Day14 prog = new Day14("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day14( string file ) {
            reservoir = new RegolithReservoir(file);
        }

        public override string Part1() {
            return reservoir.dropSand().ToString();
        }

        public override string Part2() {
            return reservoir.findVoid().ToString();
        }
    }

    public class RegolithReservoir {

        List<string> reservoir = new List<string>();
        int floor = 0;
        int left = int.MaxValue;
        int right = 0;
        int cnt = 0;

        public RegolithReservoir(string file) {

            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines) {
                string[] parts = line.Split(new char[] { ' ', ',', '-', '>' }, StringSplitOptions.RemoveEmptyEntries);

                int startX = int.Parse(parts[0]);
                int startY = int.Parse(parts[1]);

                for (int i = 2; i < parts.Length; i += 2) {
                    int nextX = int.Parse(parts[i]);
                    int nextY = int.Parse(parts[i + 1]);
                    if ( startX == nextX ) {
                        int minY = Math.Min(startY, nextY);
                        int maxY = Math.Max(startY, nextY);

                        floor = Math.Max(floor, maxY);
                        left = Math.Min(left, startX);
                        right = Math.Max(right, startX);

                        for ( int j = minY; j <= maxY; j++ ) {
                            reservoir.Add($"{startX},{j}");
                        }

                    }
                    else {
                        int minX = Math.Min(startX, nextX);
                        int maxX = Math.Max(startX, nextX);

                        floor = Math.Max(floor, startY);
                        left = Math.Min(left, minX);
                        right = Math.Max(right, maxX);

                        for (int j = minX; j <= maxX; j++) {
                            reservoir.Add($"{j},{startY}");
                        }
                    }
                    startX = nextX;
                    startY = nextY;
                }

            }

        }

        public int dropSand() {

            while (cnt < 10000) {

                int xPos = 500;
                int yPos = 0;

                bool settled = false;

                if (cnt % 100 == 0) {
                    Console.WriteLine(cnt);
                }

                while (settled == false) {
                    if (yPos > floor) {
                        return cnt;
                    }
                    else if (reservoir.Contains($"{xPos},{yPos + 1}") == false) {
                        yPos += 1;
                    }
                    else if (reservoir.Contains($"{xPos - 1},{yPos + 1}") == false) {
                        xPos -= 1;
                        yPos += 1;
                    }
                    else if (reservoir.Contains($"{xPos + 1},{yPos + 1}") == false) {
                        xPos += 1;
                        yPos += 1;
                    }
                    else {
                        settled = true;
                    }
                }

                reservoir.Add($"{xPos},{yPos}");
                cnt++;

            }

            return cnt;
        }

        public int findVoid() {

            floor += 2;

            while (cnt < 35000) {

                int xPos = 500;
                int yPos = 0;

                bool settled = false;

                if (cnt % 100 == 0) {
                    Console.WriteLine(cnt);
                }

                while (settled == false) {
                    if (yPos + 1 == floor ) {
                        settled = true;
                    }
                    else if (reservoir.Contains($"{xPos},{yPos + 1}") == false) {
                        yPos += 1;
                    }
                    else if (reservoir.Contains($"{xPos - 1},{yPos + 1}") == false) {
                        xPos -= 1;
                        yPos += 1;
                    }
                    else if (reservoir.Contains($"{xPos + 1},{yPos + 1}") == false) {
                        xPos += 1;
                        yPos += 1;
                    }
                    else {
                        if ( xPos == 500 && yPos == 0) {
                            cnt++;
                            return cnt;
                        }
                        settled = true;
                    }
                }

                reservoir.Add($"{xPos},{yPos}");
                cnt++;

            }

            return cnt;
        }

        public void print() {

            for ( int y = 0; y <= floor; y++ ) {
                for (int x = left; x <= right; x++) {
                    if (reservoir.Contains($"{x},{y}")) {
                        Console.Write("#");
                    }
                    else {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

        }
    }

}