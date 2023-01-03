using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day11 : DayN {

        string inFile;

        static void Main( string[] args ) {
            Day11 prog = new Day11("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day11( string file ) {
            inFile = file;
        }

        public override string Part1() {
            KeepAway keepAway = new KeepAway(inFile);
            keepAway.playRounds(20, true);
            return keepAway.getLevel().ToString();
        }

        public override string Part2() {
            KeepAway keepAway = new KeepAway(inFile);
            keepAway.playRounds(10000, false);
            return keepAway.getLevel().ToString();
        }
    }

    public class KeepAway {

        List<Monkey> monkeys = new List<Monkey>();

        public KeepAway( string file ) {

            string[] lines = File.ReadAllLines(file);

            for (int i = 0; i < lines.Length; i += 7) {

                Monkey cur = new Monkey();

                string[] parts = lines[i + 1].Split(new string[] { ":", " ", "," }, StringSplitOptions.RemoveEmptyEntries);
                for ( int j = 2; j < parts.Length; j++ ) {
                    cur.items.Add(int.Parse(parts[j]));
                }

                cur.expression = lines[i + 2].Substring(19);
                cur.test = int.Parse(lines[i + 3].Substring(21));
                cur.ifTrue = int.Parse(lines[i + 4].Substring(29));
                cur.ifFalse = int.Parse(lines[i + 5].Substring(30));

                monkeys.Add(cur);
            }
        }

        public void playRounds(int num, bool relief) {

            int lcm = 1;
            foreach (Monkey m in monkeys) {
                lcm *= m.test;
            }

            for (int rnd = 0; rnd < num; rnd++) {

                foreach ( Monkey m in monkeys ) {

                    foreach ( int item in m.items ) {
                        string ex = m.expression.Replace("old", item.ToString() + ".0");
                        Int64 val = evalExpression(ex);

                        if (relief) {
                            val = val / 3;
                        }
                        else {
                            val = val % lcm;
                        }

                        if ( val % m.test == 0 ) {
                            monkeys[m.ifTrue].items.Add(val);
                        }
                        else {
                            monkeys[m.ifFalse].items.Add(val);
                        }
                        m.cnt++;
                    }

                    m.items.Clear();

                }

            }

        }

        public Int64 getLevel() {
            monkeys.Sort(delegate ( Monkey x, Monkey y ) {
                return x.cnt.CompareTo(y.cnt);
            });

            Console.WriteLine($"{monkeys[monkeys.Count - 1].cnt} * {monkeys[monkeys.Count - 2].cnt}");


            return (monkeys[monkeys.Count - 1].cnt * monkeys[monkeys.Count - 2].cnt);
        }

        public Int64 evalExpression(string expression) {
            System.Data.DataTable table = new System.Data.DataTable();
            return Convert.ToInt64(table.Compute(expression, String.Empty));
        }

        internal class Monkey {

            public List<Int64> items { get; set; } = new List<Int64>();
            public string expression { get; set; }
            public int test { get; set; }
            public int ifTrue { get; set; }
            public int ifFalse { get; set; }
            public Int64 cnt { get; set; } = 0;

        }

    }

}