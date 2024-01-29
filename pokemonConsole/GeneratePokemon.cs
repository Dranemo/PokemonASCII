using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace pokemonConsole
{
    internal class GeneratePokemon
    {
        private string name = "";
        private int level;

        private int basePv = 35;
        private int evPv = 0;

        private int baseAtk = 55;
        private int evAtk = 0;

        private int baseDef = 30;
        private int evDef = 0;

        private int baseSpd = 90;
        private int evSpd = 0;

        private int baseSpe = 50;
        private int evSpe = 0;


        public static Pokemon generatePokemon(int id_generate, int level_generate)
        {
            GeneratePokemon gen = new GeneratePokemon();
            Random random = new Random();

            string fileCSV = "C:\\Users\\ycaillot\\Desktop\\pokemonConsole\\pokemonConsole\\pokemon.csv";
            using (StreamReader sr = new StreamReader(fileCSV))
            {
                string line;
                bool pokemonFound = false;

                while ((line = sr.ReadLine()) != null || !pokemonFound)
                {
                    string[] colonnes = line.Split(',');

                    int.TryParse(colonnes[0], out int id_search);

                    if (id_search == id_generate)
                    {
                        gen.name = colonnes[1];
                        gen.basePv = int.Parse(colonnes[4]);
                        gen.baseAtk = int.Parse(colonnes[5]);
                        gen.baseDef = int.Parse(colonnes[6]);
                        gen.baseSpe = int.Parse(colonnes[7]);
                        gen.baseSpd = int.Parse(colonnes[8]);

                        pokemonFound = true;
                    }
                }
            }

            gen.level = level_generate;

            int dvPv = random.Next(0, 16);
            int dvAtk = random.Next(0, 16);
            int dvDef = random.Next(0, 16);
            int dvSpd = random.Next(0, 16);
            int dvSpe = random.Next(0, 16);

            int maxPv = FormulaStatsPv(gen.level, gen.basePv, dvPv, gen.evPv);
            int maxAtk = FormulaStatsNotPv(gen.level, gen.baseAtk, dvAtk, gen.evAtk);
            int maxDef = FormulaStatsNotPv(gen.level, gen.baseDef, dvDef, gen.evDef);
            int maxSpd = FormulaStatsNotPv(gen.level, gen.baseSpd, dvSpd, gen.evSpd);
            int maxSpe = FormulaStatsNotPv(gen.level, gen.baseSpe, dvSpe, gen.evSpe);

            Pokemon pokemonGenerated = new Pokemon(gen.name, gen.level, maxPv, maxAtk, maxDef, maxSpd, maxSpe);
            return pokemonGenerated;

        }



        public static int FormulaStatsPv(int level, int baseStat, int dvStat, int evStat)
        {
            return (int)(((((baseStat + dvStat) * 2 + Math.Sqrt(evStat) / 4) * level) / 100) + level + 10);
        }

        public static int FormulaStatsNotPv(int level, int baseStat, int dvStat, int evStat)
        {
            return (int)(((((baseStat + dvStat) * 2 + Math.Sqrt(evStat) / 4) * level) / 100) + 5);
        }
    }
}
