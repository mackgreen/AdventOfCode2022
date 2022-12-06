using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day6 : DayN {

        DataStream dataStream;

        static void Main( string[] args ) {
            Day6 prog = new Day6("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day6( string file ) {
            string[] lines = File.ReadAllLines(file);
            dataStream = new DataStream(lines[0]);
        }

        public override string Part1() {
            return dataStream.findStartOfPacket().ToString();
        }

        public override string Part2() {
            return dataStream.findStartOfMessage().ToString();
        }
    }

    public class DataStream {

        string data;

        public DataStream(string stream) {
            data = stream;
        }

        public int findStartOfPacket() {
            int packStart = 0;

            for ( int i = 3; i < data.Length; i++ ) {
                string packet = data.Substring(i - 3, 4);
                if ( packet.Distinct().Count() == 4 ) {
                    return i + 1;
                }
            }

            return packStart;
        }

        public int findStartOfMessage() {
            int msgStart = 0;

            for (int i = 13; i < data.Length; i++) {
                string msg = data.Substring(i - 13, 14);
                if (msg.Distinct().Count() == 14) {
                    return i + 1;
                }
            }

            return msgStart;
        }

    }

}