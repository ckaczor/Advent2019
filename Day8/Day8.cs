using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day8
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day8\input.txt");

            var data = lines[0].ToCharArray();

            var index = 0;

            var minimumZeroCount = int.MaxValue;
            var minimumZeroLayer = 0;

            var layers = new List<char[]>();

            while (index < data.Length)
            {
                var layer = data[index..(index + 25 * 6)];

                layers.Add(layer);

                index += (25 * 6);
            }

            var layerIndex = 0;

            foreach (var layer in layers)
            {
                var currentZeroCount = layer.Count(c => c == '0');

                if (currentZeroCount < minimumZeroCount)
                {
                    minimumZeroCount = currentZeroCount;
                    minimumZeroLayer = layerIndex;
                }

                layerIndex++;
            }

            Console.WriteLine(layers[minimumZeroLayer].Count(c => c == '1') * layers[minimumZeroLayer].Count(c => c == '2'));
        }
    }
}
