using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day13 : DayN {

        //SignalPackets signalPackets;

        string[] lines;

        static void Main( string[] args ) {
            Day13 prog = new Day13("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day13( string file ) {
            //signalPackets = new SignalPackets(file);
            lines = File.ReadAllLines(file);
        }

        public override string Part1() {
            int sum = 0;
            for ( int i = 0; i < lines.Length; i += 3 ) {
                Console.Write($"{i / 3}: ");
                if ( checkCorrectNess(lines[i], lines[i + 1]) == 1 ) {
                    sum += (i / 3) + 1;
                } 
            }

            return sum.ToString();
        }

        public override string Part2() {
            return "";
        }

        public int checkCorrectNess( string left, string right ) {

            while ( left.Length > 0 && (left[0] == ',' || left[0] == ']') ) {
                left = left.Substring(1);
            }

            while ( right.Length > 0 && (right[0] == ',' || right[0] == ']') ) {
                right = right.Substring(1);
            }

            if ( left.Length == 0 ) {
                Console.WriteLine("TRUE: Left is empty than right");
                return 1;
            }
            else if ( right.Length == 0 ) {
                Console.WriteLine("FALSE: Right is empty than right");
                return 0;
            }

            //Console.WriteLine($"{left} : {right}");

            if ( left[0] == '[' && right[0] == '[' ) {
                return checkCorrectNess(left.Substring(1), right.Substring(1));
            }
            else if ( isInt(left[0]) && isInt(right[0]) ) {
                string l = extractInts(left);
                left = left.Substring(l.Length);
                string r = extractInts(right);
                right = right.Substring(r.Length);

                string[] leftInts = l.Split(",", StringSplitOptions.RemoveEmptyEntries);
                string[] rightInts = r.Split(",", StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < Math.Min(leftInts.Length, rightInts.Length); i++) {
                    if (int.Parse(leftInts[i]) < int.Parse(rightInts[i])) {
                        Console.WriteLine("TRUE: Left is less than right");
                        return 1;
                    }
                    else if (int.Parse(leftInts[i]) > int.Parse(rightInts[i])) {
                        Console.WriteLine("FALSE: Left is greater than right");
                        return 0;
                    }
                }

                if ( leftInts.Length < rightInts.Length ) {
                    Console.WriteLine("TRUE: Left is shorter than right");
                    return 1;
                }
                else if (leftInts.Length > rightInts.Length) {
                    Console.WriteLine("FALSE: Left is longer than right");
                    return 0;
                }
                else {
                    return checkCorrectNess(left, right);
                }
            }
            else if (isInt(left[0])) {
                string l = extractInts(left);
                string[] leftInts = l.Split(",", StringSplitOptions.RemoveEmptyEntries);
                left = left.Substring(leftInts[0].Length);

                int ret = checkCorrectNess($"[{leftInts[0]}]", right);
                if ( ret >= 0 ) {
                    return ret;
                }
            }
            else {
                string r = extractInts(right);
                string[] rightInts = r.Split(",", StringSplitOptions.RemoveEmptyEntries);
                right = right.Substring(rightInts[0].Length);

                int ret = checkCorrectNess(left, $"[{rightInts[0]}]");
                if (ret >= 0) {
                    return ret;
                }
            }

            if ( left.Length > 0 ) {
                int ret = checkCorrectNess(left, right);
                if (ret >= 0) {
                    return ret;
                }
            }

            return -1;

        }

        public string extractList(string val) {
            //find matching brace
            int cnt = 1;
            string newVal = "";
            for (int i = 1; i < val.Length; i++) {
                if (val[i] == '[') {
                    cnt++;
                }
                else if (val[i] == ']') {
                    cnt--;
                    if (cnt == 0) {
                        break;
                    }
                }
                newVal += val[i];
            }
            return newVal;
        }

        public string extractInts(string val) {
            string newVal = "";
            for (int i = 0; i < val.Length; i++) {
                if ( isInt(val[i]) || val[i] == ',' ) {
                    newVal += val[i];
                }
                else if ( val[i] == '[' || val[i] == ']' ) {
                    break;
                }
            }
            return newVal;
        }

        public bool isInt( char val ) {
            if (val >= '0' && val <= '9') {
                return true;
            }
            return false;
        }

    }

    public class SignalPackets {

        string[] lines;

        public SignalPackets(string file) {
            lines = File.ReadAllLines(file);
        }

        public int findPacketsInRightOrder() {

            int sum = 0;

            for (int i = 0; i < lines.Length; i += 3) {

                Console.Write($"{i / 3}: ");
                Queue<char> leftQ = new Queue<char>(lines[i]);
                Packet left = new Packet(ref leftQ);

                Queue<char> rightQ = new Queue<char>(lines[i + 1]);
                Packet right = new Packet(ref rightQ);

                Console.WriteLine(comparePackets(left, right));

                if (comparePackets(left, right) == 1 ) {
                    Console.WriteLine(i / 3);
                    sum += (i / 3) + 1;
                }
            }

            return sum;

        }

        public int comparePackets(Packet left, Packet right) {

            if ( left.values.Count > 0 && right.values.Count == 0 ) {
                string strVals = String.Join(",", left.values);
                Queue<char> newQ = new Queue<char>(strVals);
                Packet newCh = new Packet(ref newQ, left);
                left.children.Add(newCh);
                left.values.Clear();
            }
            else if ( right.values.Count > 0 && left.values.Count == 0 ) {
                string strVals = String.Join(",", right.values);
                Queue<char> newQ = new Queue<char>(strVals);
                Packet newCh = new Packet(ref newQ, right);
                right.children.Add(newCh);
                right.values.Clear();
            }


            for ( int i = 0; i < left.values.Count; i++ ) {
                if ( i >= right.values.Count ) {
                    //Console.WriteLine("Right side out of items, not in the correct order");
                    return 0;
                }
                if (left.values[i] > right.values[i]) {
                    //Console.WriteLine("Right side is smaller, so inputs are not in the right order");
                    return 0;
                }
                if (left.values[i] < right.values[i]) {
                    //Console.WriteLine("Left side is smaller, so inputs are in the right order");
                    return 1;
                }   
            }

            if (left.values.Count < right.values.Count) {
                //Console.WriteLine("Left side ran out of items, so inputs are in the right order");
                return 1;
            }

            for ( int i = 0; i < left.children.Count; i++ ) {

                if ( i >= right.children.Count ) {
                    //Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
                    return 0;
                }
                int chVal = comparePackets(left.children[i], right.children[i]);
                if ( chVal > -1 ) {
                    return chVal;
                }
            }

            if (left.children.Count < right.children.Count) {
                //
                //Console.WriteLine("Left side ran out of items, so inputs are in the right order");
                return 1;
            }

            return -1;
        }

    }

    public class Packet {
        Packet parent { get; set; }
        public List<Packet> children { get; set; } = new List<Packet>();
        public List<int> values { get; set; } = new List<int>();

        public Packet(ref Queue<char> pkt, Packet parent = null) {
            this.parent = parent;
            string strVals = "";
            while(pkt.Count > 0) {
                char pktChar = pkt.Dequeue();
                if ( pktChar == '[' ) {
                    children.Add(new Packet(ref pkt, this));
                }
                else if (pktChar == ']' ) {
                    if ( strVals != "" ) {
                        values.Add(int.Parse(strVals));
                        strVals = "";
                    }
                    return;
                }
                else if (pktChar == ',' ) {
                    if (strVals != "") {
                        values.Add(int.Parse(strVals));
                        strVals = "";
                    }
                }
                else {
                    strVals += pktChar;
                }
            }
            if (strVals != "") {
                values.Add(int.Parse(strVals));
                strVals = "";
            }
        }
    }

}