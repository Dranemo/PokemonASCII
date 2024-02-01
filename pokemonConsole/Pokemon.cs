using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Pokemon
    {
        // ------------------------------------- Infos Pokemon ------------------------------------- //
        public int id {  get; private set; }
        public string name { get; private set; }
        private List<string> listType = new List<string>();


        // ------------------ Level ------------------ //
        public int level { get; private set; }
        public int expToLevelUp { get; private set; }
        public int expActuel { get; set; }

        // ------------------ Statistiques ------------------ //
        public int pv { get; private set; }
        public int pvLeft {  get; set; }
        public int atk { get; private set; }
        public int def { get; private set; }
        public int spe { get; private set; }
        public int spd { get; private set; }


        // Max EV = 35,565
        // [0] = Base
        // [1] = DV
        // [2] = EV
        private List<int> listPv = new List<int>();
        private List<int> listAtk = new List<int>();
        private List<int> listDef = new List<int>();
        private List<int> listSpe = new List<int>();
        private List<int> listSpd = new List<int>();


        // ------------------ Capacites ------------------ //
        List<Capacity> listAttackActual = new List<Capacity>();


        // ------------------ Etat ------------------ //
        private string StatusProblem;

        // ------------------ Back ------------------ //
        private List<string> listEvo = new List<string>();

        private List<int> listAttackId = new List<int>();
        private List<int> listAttackLevel = new List<int>();

        private string expCourbe;
        public int expDonne { get; private set; }
        private bool peutEvoluer;
        private int methodeEvolution;
        private int evolutionLevel;
        private List<int> evolutionItemId = new List<int>();

        public int tauxCapture {  get; private set; }


        private string filePokemonCSV = "C:\\Users\\agathelier\\Desktop\\Nouveau dossier\\pokemonConsole\\pokemon.csv";




        // ------------------------------------- Fonctions ------------------------------------- //
        public Pokemon(int id_generate, int level_generate, bool sauvage = true, bool player = false)
        {

            Random random = new Random();

            // Variables
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

            List<int> listAttackStart = new List<int>();


            using (StreamReader sr = new StreamReader(filePokemonCSV))
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
                                peutEvoluer = false;
                                pokemonFinishReading = true;
                            }
                            else
                            {
                                peutEvoluer = true;
                            }
                            if (peutEvoluer)
                            {
                                methodeEvolution = int.Parse(colonnes[10]);
                                if (methodeEvolution == 0)
                                {
                                    evolutionLevel = int.Parse(colonnes[11]);
                                }
                                else if (methodeEvolution == 2)
                                {
                                    evolutionItemId.Add(int.Parse(colonnes[11]));
                                }
                                else if ((methodeEvolution == 3))
                                {
                                    string[] items = colonnes[11].Split('/');
                                    evolutionItemId = items.Select(int.Parse).ToList();
                                }
                            }
                            
                            expCourbe = colonnes[12];
                            expDonne = int.Parse(colonnes[13]);

                            tauxCapture = int.Parse(colonnes[14]);

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

            listPv.Add(basePv); listPv.Add(dvPv); listPv.Add(0);
            listAtk.Add(baseAtk); listAtk.Add(dvAtk); listAtk.Add(0);
            listDef.Add(baseDef); listDef.Add(dvDef); listDef.Add(0);
            listSpe.Add(baseSpe); listSpe.Add(dvSpe); listSpe.Add(0);
            listSpd.Add(baseSpd); listSpd.Add(dvSpd); listSpd.Add(0);


            id = id_generate;
            level = level_generate;

            pv = FormulaStatsPv(this.level, this.listPv);
            atk = FormulaStatsNotPv(this.level, this.listAtk);
            def = FormulaStatsNotPv(this.level, this.listDef);
            spe = FormulaStatsNotPv(this.level, this.listSpe);
            spd = FormulaStatsNotPv(this.level, this.listSpd);
            pvLeft = pv;

            expActuel = 0;
            if (expCourbe == "rapide")
            {
                expToLevelUp = FormulaCourbeRapide(level);
            }
            else if (expCourbe == "moyenne")
            {
                expToLevelUp = FormulaCourbeMoyenne(level);
            }
            else if (expCourbe == "parabolique")
            {
                expToLevelUp = FormulaCourbePara(level);
            }
            else if (expCourbe == "lente")
            {
                expToLevelUp = FormulaCourbeLente(level);
            }

            // Attaques 
            int numberOfAttacksAvailaible = listAttackStart.Count;
            int numberOfAttackLevel = 0;

            for (int i = listAttackLevel.Count - 1; i >= 0; i--)
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
                        foreach (Capacity AttackActual in listAttackActual)
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

                    for (int i = listAttackStart.Count - (numberOfAttacksAvailaible - numberOfAttackLevel); i < listAttackStart.Count; i++)
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


            // Sprite
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
        }

        public List<int> getListPv() {  return this.listPv; }
        public List<int> getListAtk() {  return this.listAtk; }
        public List<int> getListDef() {  return this.listDef; }
        public List<int> getListSpe() {  return this.listSpe; }
        public List<int> getListSpd() {  return this.listSpd; }
        public List<string> getListType() {  return this.listType; }
        public List<string> getListEvo() { return this.listEvo; }



        public void AfficherDetailsPokemon()
        {
            Console.WriteLine($"N°{id}");
            Console.WriteLine($"Name = {this.name}");
            Console.WriteLine($"Level = {this.level}");

            for (int i = 0; i < this.listType.Count; i++)
            {
                Console.WriteLine($"Type {i + 1} = {this.listType[i]}");
            }

            Console.WriteLine($"Pv = {pvLeft} / {pv}");
            Console.WriteLine($"Atk = {this.atk}");
            Console.WriteLine($"Def = {this.def}");
            Console.WriteLine($"Spe = {this.spe}");
            Console.WriteLine($"Spd = {this.spd}");

            foreach(Capacity item in listAttackActual)
            {
                Console.WriteLine(item.name);
            }
        }
        private void Evolution()
        {
            if (this.listEvo.Count > 0)
            {
                string nextEvo = this.listEvo[0];
                string[] colonnes = nextEvo.Split(',');

                this.id = int.Parse(colonnes[0]);
                this.name = colonnes[1];

                this.listType[0] = colonnes[2];
                if (colonnes[3] != "NONE")
                {
                    if (listType.Count == 1)
                    {
                        this.listType.Add(colonnes[3]);
                    }
                    else
                    {
                        this.listType[1] = colonnes[3];
                    }
                }

                this.listPv[0] = int.Parse(colonnes[4]);
                this.listAtk[0] = int.Parse(colonnes[5]);
                this.listDef[0] = int.Parse(colonnes[6]);
                this.listSpe[0] = int.Parse(colonnes[7]);
                this.listSpd[0] = int.Parse(colonnes[8]);

                int tempOldPv = pv;
                this.pv = FormulaStatsPv(this.level, this.listPv);
                this.atk = FormulaStatsNotPv(this.level, this.listAtk);
                this.def = FormulaStatsNotPv(this.level, this.listDef);
                this.spe = FormulaStatsNotPv(this.level, this.listSpe);
                this.spd = FormulaStatsNotPv(this.level, this.listSpd);

                pvLeft = pvLeft + (pv - tempOldPv);

                if (colonnes[9] == "FALSE")
                {
                    peutEvoluer = false;
                }
                else
                {
                    peutEvoluer = true;
                }

                if (peutEvoluer)
                {
                    methodeEvolution = int.Parse(colonnes[10]);

                    if (methodeEvolution == 0)
                    {
                        evolutionLevel = int.Parse(colonnes[11]);
                    }
                    else if (methodeEvolution == 2)
                    {

                        evolutionItemId.Clear();
                        evolutionItemId.Add(int.Parse(colonnes[11]));
                    }
                }

                expDonne = int.Parse(colonnes[13]);

                tauxCapture = int.Parse(colonnes[14]);

                listAttackId.Clear();
                listAttackLevel.Clear();

                string[] temp = colonnes[16].Split("/");
                listAttackId = temp.Select(int.Parse).ToList();

                temp = colonnes[17].Split("/");
                listAttackLevel = temp.Select(int.Parse).ToList();




                if (this.listEvo.Count > 1)
                {
                    for (int i = 0; i < this.listEvo.Count - 1; i++)
                    {
                        string temp2 = this.listEvo[i + 1];
                        this.listEvo[i] = temp2;
                    }
                }

                this.listEvo.RemoveAt(listEvo.Count - 1);
            }

            CheckNewCapacity();
        }
        public void LevelUp()
        {
            level++;


            if (expActuel >= expToLevelUp)
            {
                expActuel = expActuel - expToLevelUp;
            }
            if (expCourbe == "rapide")
            {
                expToLevelUp = FormulaCourbeRapide(level);
            }
            else if (expCourbe == "moyenne")
            {
                expToLevelUp = FormulaCourbeMoyenne(level);
            }
            else if (expCourbe == "parabolique")
            {
                expToLevelUp = FormulaCourbePara(level);
            }
            else if (expCourbe == "lente")
            {
                expToLevelUp = FormulaCourbeLente(level);
            }

            int tempOldPv = pv;
            this.pv = FormulaStatsPv(this.level, this.listPv);
            this.atk = FormulaStatsNotPv(this.level, this.listAtk);
            this.def = FormulaStatsNotPv(this.level, this.listDef);
            this.spe = FormulaStatsNotPv(this.level, this.listSpe);
            this.spd = FormulaStatsNotPv(this.level, this.listSpd);

            pvLeft = pvLeft + (pv - tempOldPv);
            CheckNewCapacity();
            
            if (level >= evolutionLevel)
            {
                Evolution();
            }
        }
        private void CheckNewCapacity()
        {
            for (int i = 0; i < listAttackLevel.Count; i++)
            {
                if (level == listAttackLevel[i])
                {
                    if (listAttackActual.Count < 4)
                    {
                        listAttackActual.Add(new Capacity(listAttackId[i]));
                        Console.WriteLine($"{name} a appris {listAttackActual[listAttackActual.Count - 1].name}.");
                    }
                    else
                    {
                        Capacity cap = new Capacity(listAttackId[i]);
                        bool choiceFinished = false;
                        bool loop2Question = true;
                        while (loop2Question)
                        {
                            bool loop1Question = true;
                            while (loop1Question)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Votre POKEMON souhaite apprendre l'attaque {cap.name}.");
                                Console.WriteLine("Mais votre POKEMON ne peut plus rien apprendre");
                                Console.WriteLine($"Voulez vous remplacer une attaque pour apprendre l'attaque {cap.name} ?");
                                Console.WriteLine("[OUI] [NON]");
                                string userInput2 = Console.ReadLine();

                                if (string.Equals(userInput2, "oui", StringComparison.OrdinalIgnoreCase))
                                {
                                    bool loopQuestion3 = true;
                                    Console.Clear();
                                    Console.WriteLine("Quelle capacite souhaitez vous remplacer ?");
                                    while (loop1Question)
                                    {
                                        foreach (Capacity capacity in listAttackActual)
                                        {
                                            Console.WriteLine(capacity.name);
                                        }
                                        string userInput3 = Console.ReadLine();

                                        if (string.Equals(userInput3, listAttackActual[0].name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("1... 2... 3... Tada !");
                                            Console.WriteLine($"{name} a oublié {listAttackActual[0].name}...");

                                            listAttackActual[0] = cap;
                                            loopQuestion3 = false;
                                            loop1Question = false;
                                            loop2Question = false;
                                            choiceFinished = true;
                                        }
                                        else if (string.Equals(userInput3, listAttackActual[1].name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("1... 2... 3... Tada !");
                                            Console.WriteLine($"{name} a oublié {listAttackActual[1].name}...");

                                            listAttackActual[1] = cap;
                                            loopQuestion3 = false;
                                            loop1Question = false;
                                            loop2Question = false;
                                            choiceFinished = true;
                                        }
                                        else if (string.Equals(userInput3, listAttackActual[2].name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("1... 2... 3... Tada !");
                                            Console.WriteLine($"{name} a oublié {listAttackActual[2].name}...");

                                            listAttackActual[2] = cap;
                                            loopQuestion3 = false;
                                            loop1Question = false;
                                            loop2Question = false;
                                            choiceFinished = true;
                                        }
                                        else if (string.Equals(userInput3, listAttackActual[3].name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            Console.WriteLine("1... 2... 3... Tada !");
                                            Console.WriteLine($"{name} a oublié {listAttackActual[3].name}...");

                                            listAttackActual[3] = cap;
                                            loopQuestion3 = false;
                                            loop1Question = false;
                                            loop2Question = false;
                                            choiceFinished = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Réponse invalide. Veuillez répondre par 'oui' ou 'non'.");
                                        }
                                        
                                        
                                        Console.WriteLine($"Et il a appris {cap.name}.");
                                    }
                                    
                                }
                                else if (string.Equals(userInput2, "non", StringComparison.OrdinalIgnoreCase))
                                {
                                    loop1Question = false;
                                }
                                else
                                {
                                    Console.WriteLine("Réponse invalide. Veuillez répondre par 'oui' ou 'non'.");
                                }
                            }

                            if (!choiceFinished)
                            {
                                Console.Clear();
                                Console.WriteLine($"Voulez vous vraiment renoncer a apprendre {cap.name} ?");
                                Console.WriteLine("[OUI] [NON]");
                                string userInput = Console.ReadLine();

                                if (string.Equals(userInput, "oui", StringComparison.OrdinalIgnoreCase))
                                {
                                    Console.WriteLine($"Vous n'avez pas appris {cap.name}");
                                    loop2Question = false;
                                }
                                else if (string.Equals(userInput, "non", StringComparison.OrdinalIgnoreCase))
                                {
                                    loop1Question = true;
                                }
                                else
                                {
                                    Console.WriteLine("Réponse invalide. Veuillez répondre par 'oui' ou 'non'.");
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine();
        }


        // ------------------ Formules ------------------ //
        private int FormulaStatsPv(int level, List<int> listPv)
        {

            return (int)(((((listPv[0] + listPv[1]) * 2 + Math.Sqrt(listPv[2]) / 4) * level) / 100) + level + 10);
        }
        private int FormulaStatsNotPv(int level, List<int> listStat)
        {
            return (int)(((((listStat[0] + listStat[1]) * 2 + Math.Sqrt(listStat[2]) / 4) * level) / 100) + 5);
        }

        private int FormulaCourbeRapide(int level)
        {
            double result = 0.8 * (level * level * level);
            return (int)Math.Round(result);
        }
        private int FormulaCourbeMoyenne(int level) 
        {
            return (level * level * level);
        }
        private int FormulaCourbePara(int level)
        {
            double result = 1.2 * (level * level * level) - 15 * (level * level) + 100 * level - 140;
            return (int)Math.Round(result);
        }
        private int FormulaCourbeLente(int level)
        {
            double result = 1.25 * (level * level * level);
            return (int)Math.Round(result);
        }

    }
}
