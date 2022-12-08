using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day8 : DayN {

        Forest forest;

        static void Main( string[] args ) {
            Day8 prog = new Day8("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day8( string file ) {
            forest = new Forest(file);
        }

        public override string Part1() {
            return forest.getVisibleTreeCount().ToString();
        }

        public override string Part2() {
            return forest.findMaxScenicScore().ToString();
        }
    }

    public class Forest {

        string[] trees;

        public Forest( string file ) {
            trees = File.ReadAllLines( file );
        }

        public int getVisibleTreeCount() {
            int cnt = (trees.Length * 2) + (trees[0].Length * 2 - 4);

            for ( int i = 1; i < trees.Length - 1; i++ ) {
                for ( int j = 1; j < trees[i].Length - 1; j++ ) {
                    cnt += checkTreeVisibility(i, j);
                }
            }

            return cnt;
        }

        public int findMaxScenicScore() {

            int maxScore = 0;

            for (int i = 1; i < trees.Length - 1; i++) {
                for (int j = 1; j < trees[i].Length - 1; j++) {
                    int score = calcScenicScore(i, j);
                    maxScore = Math.Max(maxScore, score);
                }
            }

            return maxScore;
        }

        public int checkTreeVisibility( int i, int j ) {

            char height = trees[i][j];

            int visible = 1;
            for (int x = i - 1; x >= 0; x--) {
                if (height <= trees[x][j]) {
                    visible = 0;
                    break;
                }
            }

            if ( visible == 1 ) {
                return 1;
            }

            visible = 1;
            for (int x = i + 1; x < trees.Length; x++) {
                if (height <= trees[x][j]) {
                    visible = 0;
                    break;
                }
            }

            if (visible == 1) {
                return 1;
            }

            visible = 1;
            for (int y = j - 1; y >= 0; y--) {
                if (height <= trees[i][y]) {
                    visible = 0;
                    break;
                }
            }

            if (visible == 1) {
                return 1;
            }

            visible = 1;
            for (int y = j + 1; y < trees[i].Length; y++) {
                if (height <= trees[i][y]) {
                    visible = 0;
                    break;
                }
            }

            if (visible == 1) {
                return 1;
            }

            return 0;

        }

        public int calcScenicScore( int i, int j ) {
            char height = trees[i][j];

            int north = 0;
            for (int x = i - 1; x >= 0; x--) {
                north++;
                if (height <= trees[x][j]) {
                    break;
                }
            }

            int south = 0;
            for (int x = i + 1; x < trees.Length; x++) {
                south++;
                if (height <= trees[x][j]) {
                    break;
                }
            }

            int west = 0;
            for (int y = j - 1; y >= 0; y--) {
                west++;
                if (height <= trees[i][y]) {
                    break;
                }
            }

            int east = 0;
            for (int y = j + 1; y < trees[i].Length; y++) {
                east++;
                if (height <= trees[i][y]) {
                    break;
                }
            }

            return (north * south * west * east);

        }
    }

}