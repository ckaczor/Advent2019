using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent
{
    public static class Day14
    {
        private class Reagent
        {
            public string Name { get; }
            public long Count { get; }

            public Reagent(string s)
            {
                var parts = s.Split(' ');

                Name = parts[1];
                Count = long.Parse(parts[0]);
            }
        }

        private class Reaction
        {
            public Reagent Result { get; }
            public List<Reagent> Components { get; } = new List<Reagent>();

            public Reaction(string s)
            {
                var reactionParts = s.Split("=>").Select(s2 => s2.Trim()).ToArray();

                Result = new Reagent(reactionParts[1]);

                var reagentParts = reactionParts[0].Split(',').Select(s2 => s2.Trim()).ToArray();

                foreach (var reagentPart in reagentParts)
                {
                    Components.Add(new Reagent(reagentPart));
                }
            }
        }

        private static readonly Dictionary<string, Reaction> Reactions = new Dictionary<string, Reaction>();
        private static readonly Dictionary<string, long> Inventory = new Dictionary<string, long>();

        public static void Execute()
        {
            var lines = File.ReadAllLines(@".\Day14\input.txt");

            foreach (var line in lines)
            {
                var reaction = new Reaction(line);

                Reactions[reaction.Result.Name] = reaction;
            }

            var oreRequired = GetRequiredOre("FUEL", 1);

            Console.WriteLine($"Ore: {oreRequired}");

            // Too lazy to do a binary search - find a decent starting point and go from there.  It finished
            // in the few minutes it took to get coffee - good enough!

            const long oreLimit = 1000000000000;
            var fuel = (long)Math.Floor((decimal)oreLimit / oreRequired);

            while (true)
            {
                oreRequired = GetRequiredOre("FUEL", fuel);

                if (oreRequired > oreLimit)
                    break;

                fuel++;
            }

            Console.WriteLine($"Fuel: {fuel - 1}");
        }

        private static long GetRequiredOre(string reagentName, long amountRequired)
        {
            // Get the reaction to produce this reagent
            var reaction = Reactions[reagentName];

            // Get what we have already
            var inInventory = Inventory.ContainsKey(reagentName) ? Inventory[reagentName] : 0;

            // Figure out how many reactions are needed
            var reactionsRequired = (long)Math.Ceiling((decimal)Math.Max(amountRequired - inInventory, 0) / reaction.Result.Count);

            // Set what we have in inventory after
            Inventory[reagentName] = (reaction.Result.Count * reactionsRequired) - (amountRequired - inInventory);

            var oreRequired = 0L;

            // Loop over each reagent
            foreach (var reagent in reaction.Components)
            {
                if (reagent.Name == "ORE")
                    oreRequired += reactionsRequired * reagent.Count;
                else
                    oreRequired += GetRequiredOre(reagent.Name, reagent.Count * reactionsRequired);
            }

            return oreRequired;
        }
    }
}
