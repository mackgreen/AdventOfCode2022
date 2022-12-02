using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day2 : DayN {

        RockPaperScissors rps;


        static void Main( string[] args ) {
            Day2 prog = new Day2("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day2( string file ) {
            rps = new RockPaperScissors(file);
        }

        public override string Part1() {
            return rps.getTotalScore().ToString();
        }

        public override string Part2() {
            return rps.getShapeAndScore().ToString();
        }
    }

    public class RockPaperScissors {

        string[] lines;

        public RockPaperScissors( string file ) {
            lines = File.ReadAllLines(file);
        }

        public int getTotalScore() {

            int totalScore = 0;

            foreach (string line in lines) {
                totalScore += calcScore(line[0], line[2]);
            }

            return totalScore;

        }

        public int getShapeAndScore() {
            int totalScore = 0;

            foreach (string line in lines) {
                totalScore += calcShapeScore(line[0], line[2]);
            }

            return totalScore;
        }

        int calcScore ( char theirs, char mine ) {
            int score = mine - 'W';

            if ( theirs - 'A' == mine - 'X' ) { //Draw
                score += 3;
            }
            else if ( theirs == 'C' && mine == 'X' ) { //Rock beats scissors
                score += 6;
            }
            else if ( theirs == 'A' && mine == 'Y' ) { //Paper beats rock
                score += 6;
            }
            else if ( theirs == 'B' && mine == 'Z' ) { //Scissors beats paper
                score += 6;
            }

            return score;
        }

        int calcShapeScore( char theirs, char result ) {

            int score = 0;

            if (result == 'Y') {
                score = 3 + theirs - '@'; //this looks stupid
            }
            else if (result == 'Z') {
                score = 6;
                if ( theirs == 'A' ) {
                    score += 2;
                }
                else if ( theirs == 'B' ) {
                    score += 3;
                }
                else {
                    score += 1;
                }
            }
            else {
                if (theirs == 'A') {
                    score += 3;
                }
                else if (theirs == 'B') {
                    score += 1;
                }
                else {
                    score += 2;
                }
            }

            return score;

        }

    }

}