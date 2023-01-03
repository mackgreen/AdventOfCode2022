using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day12 : DayN {

        HeightMap heightMap;

        static void Main( string[] args ) {
            Day12 prog = new Day12("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day12( string file ) {
            heightMap = new HeightMap(file);
        }

        public override string Part1() {
            return heightMap.findShortestPath().ToString();
        }

        public override string Part2() {
            return heightMap.findScenicPath().ToString();
        }
    }

    public class HeightMap {

        Node[,] map;

        int xStart, yStart, xStop, yStop;

        public HeightMap(string file) {

            string[] lines = File.ReadAllLines(file);

            map = new Node[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++) {
                for (int j = 0; j < lines[i].Length; j++) {
                    if (lines[i][j] == 'S') {
                        xStop = j;
                        yStop = i;
                        map[i, j] = new Node { x = j, y = i, val = 0 };
                    }
                    else if (lines[i][j] == 'E') {
                        xStart = j;
                        yStart = i;
                        map[i, j] = new Node { x = j, y = i, val = 27 };
                    }
                    else {
                        map[i, j] = new Node { x = j, y = i, val = lines[i][j] - 'a' + 1 };
                    }
                }
            }
        }

        public long findShortestPath() {
            return dijkstra(xStart, yStart);
        }

        public long findScenicPath() {
            long minLength = long.MaxValue;
            for (int i = 0; i < map.GetLength(0); i++) {
                for (int j = 0; j < map.GetLength(1); j++) {
                    if ( map[i, j].val == 1 && map[i, j].dist < minLength ) {
                        minLength = map[i, j].dist;
                    }
                }
            }
            return minLength;
        }

        private long dijkstra( int x, int y ) {
            map[y, x].dist = 0;

            PriorityQueue<Node, long> q = new PriorityQueue<Node, long>();
            q.Enqueue(map[y, x], map[y, x].dist);

            while (q.Count > 0) {
                Node u = q.Dequeue();
                u.queued = true;

                List<Node> neighbors = getUnqueuedNeighbors(u.x, u.y);

                foreach (Node n in neighbors) {
                    long alt = u.dist + 1;

                    if (alt >= n.dist) {
                        continue;
                    }

                    n.dist = alt;
                    q.Enqueue(n, alt);
                }

            }

            StreamWriter writetext = new StreamWriter("out.txt");

            for ( int i = 0; i < map.GetLength(0); i++ ) {
                for ( int j = 0; j < map.GetLength(1); j++ ) {
                    writetext.Write($"{map[i, j].dist},");
                }
                writetext.WriteLine();
            }

            writetext.Close();

            return map[yStop, xStop].dist;
        }

        private List<Node> getUnqueuedNeighbors( int x, int y ) {
            List<Node> retVal = new List<Node>();

            if (y > 0 && !map[y - 1, x].queued && (map[y, x].val - map[y - 1, x].val) <= 1 ) {
                retVal.Add(map[y - 1, x]);
            }
            if (y < map.GetLength(0) - 1 && !map[y + 1, x].queued && (map[y, x].val - map[y + 1, x].val) <= 1) {
                retVal.Add(map[y + 1, x]);
            }
            if (x > 0 && !map[y, x - 1].queued && (map[y, x].val - map[y, x - 1].val) <= 1) {
                retVal.Add(map[y, x - 1]);
            }
            if (x < map.GetLength(1) - 1 && !map[y, x + 1].queued && (map[y, x].val - map[y, x + 1].val) <= 1) {
                retVal.Add(map[y, x + 1]);
            }

            return retVal;
        }

        internal class Node {
            public int x { get; set; }
            public int y { get; set; }
            public int val { get; set; }
            public long dist { get; set; } = long.MaxValue;
            public bool queued { get; set; } = false;
        }
    }

}