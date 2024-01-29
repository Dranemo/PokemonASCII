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
        private string fileCSV = "C:\\Users\\ycaillot\\Desktop\\C-Pokemon\\pokemonConsole\\pokemon.csv";

        public static Pokemon generatePokemon(int id_generate, int level_generate, int ev_generate_all_stats = 0)
        {
            GeneratePokemon gen = new GeneratePokemon();
            Random random = new Random();

            string name = "";
            int basePv = 0;
            int baseAtk = 0;
            int baseDef = 0;
            int baseSpe = 0;
            int baseSpd = 0;

            int dvPv = random.Next(0, 16);
            int dvAtk = random.Next(0, 16);
            int dvDef = random.Next(0, 16);
            int dvSpe = random.Next(0, 16);
            int dvSpd = random.Next(0, 16);

            List<int> listPv = new List<int>();
            List<int> listAtk = new List<int>();
            List<int> listDef = new List<int>();
            List<int> listSpe = new List<int>();
            List<int> listSpd = new List<int>();
            List<string> listType = new List<string>();


            using (StreamReader sr = new StreamReader(gen.fileCSV))
            {
                string line;
                bool pokemonFound = false;


                while ((line = sr.ReadLine()) != null || !pokemonFound)
                {
                    string[] colonnes = line.Split(',');

                    int.TryParse(colonnes[0], out int id_search);

                    if (id_search == id_generate)
                    {
                        name = colonnes[1];
                        basePv = int.Parse(colonnes[4]);
                        baseAtk = int.Parse(colonnes[5]);
                        baseDef = int.Parse(colonnes[6]);
                        baseSpe = int.Parse(colonnes[7]);
                        baseSpd = int.Parse(colonnes[8]);

                        listType.Add(colonnes[2]);
                        if (colonnes[3] != "NONE")
                        {
                            listType.Add(colonnes[3]);
                        }

                        pokemonFound = true;
                    }
                }
            }

            listPv.Add(basePv); listPv.Add(dvPv); listPv.Add(ev_generate_all_stats);
            listAtk.Add(baseAtk); listAtk.Add(dvAtk); listAtk.Add(ev_generate_all_stats);
            listDef.Add(baseDef); listDef.Add(dvDef); listDef.Add(ev_generate_all_stats);
            listSpe.Add(baseSpe); listSpe.Add(dvSpe); listSpe.Add(ev_generate_all_stats);
            listSpd.Add(baseSpd); listSpd.Add(dvSpd); listSpd.Add(ev_generate_all_stats);

            Pokemon pokemonGenerated = new Pokemon(level_generate, name,listType, listPv, listAtk, listDef, listSpd, listSpe);
            return pokemonGenerated;

        }

    }
}
