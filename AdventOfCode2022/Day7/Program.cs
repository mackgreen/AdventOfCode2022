using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day7 : DayN {

        FileSystem fileSystem;

        static void Main( string[] args ) {
            Day7 prog = new Day7("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day7( string file ) {
            fileSystem = new FileSystem( file );
        }

        public override string Part1() {
            fileSystem.calcDirectorySize(fileSystem.rootDir).ToString();
            return fileSystem.sumSmallDirs.ToString();
        }

        public override string Part2() {
            return fileSystem.findDirectoryToDelete().ToString();
        }
    }

    public class FileSystem {

        public Directory rootDir;
        Directory curDir;
        public int sumSmallDirs = 0;
        public int dirToDelete = int.MaxValue;

        public FileSystem( string file ) {
            string[] lines = File.ReadAllLines(file);

            rootDir = new Directory("/", null);
            curDir = rootDir;

            for (int i = 1; i < lines.Length; i++) {
                string[] parts = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if ( parts[0] == "$" && parts[1] == "ls" ) {
                    continue;
                } 
                else if ( parts[0] == "$" && parts[1] == "cd" ) {
                    curDir = curDir.changeDirectory(parts[2]);
                }
                else if ( parts[0] == "dir" ) {
                    curDir.subDirs.Add(parts[1], new Directory(parts[1], curDir));
                }
                else {
                    curDir.files.Add(parts[1], int.Parse(parts[0]));
                }
            }

        }

        public int calcDirectorySize(Directory d) {
            foreach ( string file in d.files.Keys ) {
                d.size += d.files[file];
            }
            foreach ( Directory subDir in d.subDirs.Values ) {
                d.size += calcDirectorySize(subDir);
            }

            if ( d.size < 100000 ) {
                sumSmallDirs += d.size;
            }

            return d.size;
        }

        public int findDirectoryToDelete() {
            int space = 70000000 - rootDir.size;
            space = 30000000 - space;
            traverseDirectorySizes(space, rootDir);
            return dirToDelete;
        }

        public void traverseDirectorySizes( int space, Directory d ) {
            if ( d.size > space && d.size < dirToDelete ) {
                dirToDelete = d.size;
            }
            foreach (Directory subDir in d.subDirs.Values) {
                traverseDirectorySizes(space, subDir);
            }
        }

    }

    public class Directory {

        public string name { get; }
        public Directory parent { get; }
        public Dictionary<string, Directory> subDirs { get; set;  } = new Dictionary<string, Directory>();
        public Dictionary<string, int> files { get; set; } = new Dictionary<string, int>();
        public int size = 0;

        public Directory( string _name, Directory _parent ) {
            name = _name;
            parent = _parent;
        }

        public Directory changeDirectory(string name) {
            if ( name == ".." ) {
                return parent;
            }
            else {
                return subDirs[name];
            }
        }

    }
}