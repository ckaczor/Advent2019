using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    public static class Day6
    {
        private static readonly Dictionary<string, MapObject> Orbits = new Dictionary<string, MapObject>
        {
            ["COM"] = new MapObject { Name = "COM" }
        };

        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day6\input.txt");

            foreach (var line in lines)
            {
                var lineParts = line.Split(')');

                var orbitObject = lineParts[1];

                Orbits[orbitObject] = new MapObject { Name = orbitObject };
            }

            foreach (var line in lines)
            {
                var lineParts = line.Split(')');

                var orbitCenter = lineParts[0];
                var orbitObject = lineParts[1];

                Orbits[orbitObject].Parent = Orbits[orbitCenter];
            }

            var count = 0;

            foreach (var orbitKey in Orbits.Keys)
            {
                var orbitObject = Orbits[orbitKey];

                while (orbitObject.Parent != null)
                {
                    count++;
                    orbitObject = orbitObject.Parent;
                }
            }

            Console.WriteLine(count);

            var santaPath = GetPath("SAN");
            var myPath = GetPath("YOU");

            var commonPoint = string.Empty;

            foreach (var key in myPath)
            {
                if (santaPath.Contains(key))
                {
                    commonPoint = key;
                    break;
                }
            }

            var jumpCount = 0;

            foreach (var key in myPath)
            {
                if (key == commonPoint)
                    break;

                jumpCount++;

            }

            foreach (var key in santaPath)
            {
                if (key == commonPoint)
                    break;

                jumpCount++;
            }

            Console.WriteLine(jumpCount);
        }

        private static List<string> GetPath(string startKey)
        {
            var path = new List<string>();

            var orbitObject = Orbits[startKey];

            while (orbitObject.Parent != null)
            {
                orbitObject = orbitObject.Parent;

                path.Add(orbitObject.Name);
            }

            return path;
        }
    }
}
