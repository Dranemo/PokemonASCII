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
        private string fileCSV = "C:\\Users\\yanae\\Desktop\\C-Pokemon\\pokemonConsole\\pokemon.csv";


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

            List<string> listEvo = new List<string>();



            List<int> listAttackId = new List<int>();
            List<int> listAttackLevel = new List<int>();
            List<int> listAttackStart = new List<int>();

            List<Capacity> listAttackActual = new List<Capacity>();


            using (StreamReader sr = new StreamReader(gen.fileCSV))
            {
                string line;
                bool pokemonFound = false;
                bool pokemonFinishReading = false;

                line = sr.ReadLine(); 
                line = sr.ReadLine();

                while ((line = sr.ReadLine()) != null && !pokemonFinishReading)
                {
                    if (!pokemonFound)
                    {
                        string[] colonnes = line.Split(',');

                        int.TryParse(colonnes[0], out int id_search);

                        if (id_search == id_generate)
                        {
                            name = colonnes[1];
                            listType.Add(colonnes[2]);
                            if (colonnes[3] != "NONE")
                            {
                                listType.Add(colonnes[3]);
                            }
                            basePv = int.Parse(colonnes[4]);
                            baseAtk = int.Parse(colonnes[5]);
                            baseDef = int.Parse(colonnes[6]);
                            baseSpe = int.Parse(colonnes[7]);
                            baseSpd = int.Parse(colonnes[8]);
                            if (colonnes[9] == "FALSE")
                            {
                                pokemonFinishReading = true;
                            }

                            string[] temp = colonnes[15].Split("/");
                            listAttackStart = temp.Select(int.Parse).ToList();

                            temp = colonnes[16].Split("/");
                            listAttackId = temp.Select(int.Parse).ToList();

                            temp = colonnes[17].Split("/");
                            listAttackLevel = temp.Select(int.Parse).ToList();


                            pokemonFound = true;
                        }
                    }

                    else
                    {
                        listEvo.Add(line);

                        string[] colonnes = line.Split(',');
                        if (colonnes[9] == "FALSE")
                        {
                            pokemonFinishReading = true;
                        }
                    }
                }
            }

            listPv.Add(basePv); listPv.Add(dvPv); listPv.Add(ev_generate_all_stats);
            listAtk.Add(baseAtk); listAtk.Add(dvAtk); listAtk.Add(ev_generate_all_stats);
            listDef.Add(baseDef); listDef.Add(dvDef); listDef.Add(ev_generate_all_stats);
            listSpe.Add(baseSpe); listSpe.Add(dvSpe); listSpe.Add(ev_generate_all_stats);
            listSpd.Add(baseSpd); listSpd.Add(dvSpd); listSpd.Add(ev_generate_all_stats);

            // Attaques 
            int numberOfAttacksAvailaible = listAttackStart.Count;
            int numberOfAttackLevel = 0;

            for (int i = listAttackLevel.Count-1; i >= 0; i--)
            {
                if (listAttackLevel[i] <= level_generate)
                {
                    numberOfAttacksAvailaible++;
                    numberOfAttackLevel++;
                }
            }

            for (int i = 0; i < listAttackLevel.Count; i++)
            {
                for (int j = 0; j < listAttackId.Count; j++)
                {
                    if (listAttackId[j] == listAttackLevel[i])
                    {
                        numberOfAttacksAvailaible--;
                    }
                }
            }


            int numberAssigned = 0;
            if (numberOfAttacksAvailaible <= 4)
            {
                foreach (int AttackStart in listAttackStart)
                {
                    listAttackActual.Add(new Capacity(AttackStart));
                    numberAssigned++;
                }

                while (numberAssigned < numberOfAttacksAvailaible)
                {
                    foreach (int AttackId in listAttackId)
                    {
                        bool AttackTaken = false;
                        foreach(Capacity AttackActual in listAttackActual)
                        {
                            if (AttackActual.id == AttackId)
                            {
                                AttackTaken = true;
                            }
                        }
                        if (!AttackTaken)
                        {
                            listAttackActual.Add(new Capacity(AttackId));
                            numberAssigned++;
                        }
                        if (numberAssigned == numberOfAttacksAvailaible)
                        {
                            break;
                        }
                    }
                }
            }


            else if (numberOfAttacksAvailaible > 4)
            {
                if (numberOfAttackLevel < 4)
                {
                    List<Capacity> temp = new List<Capacity>();
                    
                    for (int i = 0; i < numberOfAttackLevel; i++) 
                    {
                        temp.Add(new Capacity(listAttackId[i]));
                    }

                    for (int i = listAttackStart.Count - (numberOfAttacksAvailaible - numberOfAttackLevel);i < listAttackStart.Count; i++)
                    {
                        bool AttackAlreadyTaken = false;
                        foreach (Capacity AttackActual in temp)
                        {
                            if (listAttackStart[i] == AttackActual.id)
                            {
                                AttackAlreadyTaken = true;
                            }
                        }
                        if (!AttackAlreadyTaken)
                        {
                            listAttackActual.Add(new Capacity(listAttackStart[i]));
                        }
                    }

                    foreach (Capacity AttackActual in temp)
                    {
                        listAttackActual.Add(AttackActual);
                    }
                }



                if (numberOfAttackLevel >= 4)
                {
                    int AttackAEviter = numberOfAttackLevel - 4;
                    for (int i = AttackAEviter; i < AttackAEviter + 4; i++)
                    {
                        listAttackActual.Add(new Capacity(listAttackId[i]));
                    }
                }
            }














            string asciiArtFileName = $"ascii-art ({id_generate}).txt";
            string asciiArtFilePath = Path.Combine("C:\\Users\\yanae\\Desktop\\C-Pokemon\\pokemonConsole\\Assets\\", asciiArtFileName);

            if (File.Exists(asciiArtFilePath))
            {
                string asciiArt = File.ReadAllText(asciiArtFilePath);
                Console.WriteLine(asciiArt);
            }
            else
            {
                Console.WriteLine($"Sprite ASCII non trouvé pour le Pokémon avec l'ID {id_generate}");
            }

            Pokemon pokemonGenerated = new Pokemon(id_generate, level_generate, name,listType, listPv, listAtk, listDef, listSpd, listSpe, listEvo);
            return pokemonGenerated;

        }

    }
}
