using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day5 : DayN {

        SupplyStacks stacks;

        static void Main( string[] args ) {
            Day5 prog = new Day5("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day5( string file ) {
            stacks = new SupplyStacks(file);
        }

        public override string Part1() {
            return stacks.processCmds();
        }

        public override string Part2() {
            return stacks.process9001Cmds();
        }
    }

    public class SupplyStacks {

        List<Stack<char>> stacks = new List<Stack<char>>();
        List<Stack<char>> stacks2 = new List<Stack<char>>();

        string[] lines;
        int cmdStart = 0;

        public SupplyStacks( string file ) {

            lines = File.ReadAllLines(file);

            for (int i = 0; i < lines.Length; i++) {
                if ( String.IsNullOrEmpty(lines[i]) ) {
                    cmdStart = i + 1;

                    for (int j = 0; j <= lines[i - 2].Length / 4; j++ ) {
                        stacks.Add(new Stack<char>());
                        stacks2.Add(new Stack<char>());
                    }

                    for (int j = i - 2; j >= 0; j--) {
                        int s = 0;
                        for ( int k = 1; k < lines[j].Length; k += 4 ) {
                            if (lines[j][k] != ' ') {
                                stacks[s].Push(lines[j][k]);
                                stacks2[s].Push(lines[j][k]);
                            }
                            s++;
                        }
                    }

                    break;
                }

            }
            
        }

        public string processCmds() {

            for (int i = cmdStart; i < lines.Length; i++) {
                string[] parts = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                for ( int j = 0; j < int.Parse(parts[1]); j++ ) {
                    char crate = stacks[int.Parse(parts[3]) - 1].Pop();
                    stacks[int.Parse(parts[5]) - 1].Push(crate);
                }

            }

            string retStr = "";

            for (int i = 0; i < stacks.Count; i++) {
                retStr += stacks[i].Peek();
            }

            return retStr;
        }

        public string process9001Cmds() {

            for (int i = cmdStart; i < lines.Length; i++) {
                string[] parts = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Stack<char> intStack = new Stack<char>();

                for (int j = 0; j < int.Parse(parts[1]); j++) {
                    char crate = stacks2[int.Parse(parts[3]) - 1].Pop();
                    intStack.Push(crate);
                }

                for (int j = 0; j < int.Parse(parts[1]); j++ ) {
                    stacks2[int.Parse(parts[5]) - 1].Push(intStack.Pop());
                }


            }

            string retStr = "";

            for (int i = 0; i < stacks2.Count; i++) {
                retStr += stacks2[i].Peek();
            }

            return retStr;
        }

    }

}