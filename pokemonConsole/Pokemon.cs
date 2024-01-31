using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Pokemon
    {
        public int id {  get; private set; }
        public string name { get; private set; }
        public int level { get; private set; }

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

        private List<string> listType = new List<string>();
        private List<string> listEvo = new List<string>();

        List<int> listAttackId = new List<int>();
        List<int> listAttackLevel = new List<int>();
        List<int> listAttackActual = new List<int>();

        private string filePokemonCSV = "C:\\Users\\yanae\\Desktop\\C-Pokemon\\pokemonConsole\\pokemon.csv";
        public Pokemon(int id_, int level_, string name_, List<string> listType_, List<int> listPv_, List<int> listAtk_, List<int> listDef_, List<int> listSpd_, List<int> listSpe_, List<string> listEvo_)
        {
            this.id = id_;
            this.name = name_;
            this.level = level_;
            this.listType = listType_;
            this.listEvo = listEvo_;

            this.listPv = listPv_;
            this.listAtk = listAtk_;
            this.listDef = listDef_;
            this.listSpd = listSpd_;
            this.listSpe = listSpe_;

            this.pv = FormulaStatsPv(this.level, this.listPv);
            this.atk = FormulaStatsNotPv(this.level, this.listAtk);
            this.def = FormulaStatsNotPv(this.level, this.listDef);
            this.spe = FormulaStatsNotPv(this.level, this.listSpe);
            this.spd = FormulaStatsNotPv(this.level, this.listSpd);
            this.pvLeft = pv;
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
            Console.WriteLine($"Name = {this.name}");
            Console.WriteLine($"Level = {this.level}");

            for (int i = 0; i < this.listType.Count; i++)
            {
                Console.WriteLine($"Type {i + 1} = {this.listType[i]}");
            }

            Console.WriteLine($"Pv = {this.pv}");
            Console.WriteLine($"Atk = {this.atk}");
            Console.WriteLine($"Def = {this.def}");
            Console.WriteLine($"Spe = {this.spe}");
            Console.WriteLine($"Spd = {this.spd}");
        }

        private int FormulaStatsPv(int level, List<int> listPv)
        {

            return (int)(((((listPv[0] + listPv[1]) * 2 + Math.Sqrt(listPv[2]) / 4) * level) / 100) + level + 10);
        }
        private int FormulaStatsNotPv(int level, List<int> listStat)
        {
            return (int)(((((listStat[0] + listStat[1]) * 2 + Math.Sqrt(listStat[2]) / 4) * level) / 100) + 5);
        }

        public void Evolution()
        {
            if (this.listEvo.Count > 0)
            {
                string nextEvo = this.listEvo[0];
                string[] colonnes = nextEvo.Split(',');

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

                this.pv = FormulaStatsPv(this.level, this.listPv);
                this.atk = FormulaStatsNotPv(this.level, this.listAtk);
                this.def = FormulaStatsNotPv(this.level, this.listDef);
                this.spe = FormulaStatsNotPv(this.level, this.listSpe);
                this.spd = FormulaStatsNotPv(this.level, this.listSpd);

                if (this.listEvo.Count > 1)
                {
                    for (int i = 0; i < this.listEvo.Count - 1; i++)
                    {
                        string temp = this.listEvo[i + 1];
                        this.listEvo[i] = temp;
                    }
                }

                this.listEvo.RemoveAt(listEvo.Count - 1);
            }
        }
    }
}
