using System;
using System.Collections.Generic;
using System.IO;

namespace Advent
{
    public static class Day8
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day8\input.txt");

            var data = lines[0].ToCharArray();

            var index = 0;

            var layers = new List<char[]>();

            while (index < data.Length)
            {
                var layer = data[index..(index + 25 * 6)];

                layers.Add(layer);

                index += (25 * 6);
            }

            /*
            var minimumZeroCount = int.MaxValue;
            var minimumZeroLayer = 0;

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
            */

            var finalImage = new char[25 * 6];
            var startLayer = data.Length / (25 * 6) - 1;

            finalImage = layers[startLayer];

            for (var layerIndex = startLayer - 1; layerIndex >= 0; layerIndex--)
            {
                var layer = layers[layerIndex];

                for (var i = 0; i < layer.Length; i++)
                {
                    if (layer[i] != '2')
                        finalImage[i] = layer[i];
                }
            }

            var outputIndex = 1;
            foreach (var c in finalImage)
            {
                Console.Write(c == '0' ? ' ' : '*');

                if (outputIndex % 25 == 0)
                    Console.WriteLine();

                outputIndex++;
            }
        }
    }
}
