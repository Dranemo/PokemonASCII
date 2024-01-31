using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Pokemon
    {
        private string name;
        private int level;

        public int Pv { get; set; }
        private int pv;
        private int atk;
        private int def;
        private int spe;
        private int spd;

        List<int> listPv = new List<int>();
        List<int> listAtk = new List<int>();
        List<int> listDef = new List<int>();
        List<int> listSpe = new List<int>();
        List<int> listSpd = new List<int>();

        List<string> listType = new List<string>();

        List<string> listEvo = new List<string>();


        public Pokemon(int level_, string name_, List<string> listType_, List<int> listPv_, List<int> listAtk_, List<int> listDef_, List<int> listSpd_, List<int> listSpe_, List<string> listEvo_)
        {
            this.name = name_;
            this.level = level_;
            this.listType = listType_;
            this.listEvo = listEvo_;

            this.listPv = listPv_;
            this.Pv = FormulaStatsPv(this.level, this.listPv);
            this.listAtk = listAtk_;
            this.listDef = listDef_;
            this.listSpd = listSpd_;
            this.listSpe = listSpe_;

            this.pv = FormulaStatsPv(this.level, this.listPv);
            this.atk = FormulaStatsNotPv(this.level, this.listAtk);
            this.def = FormulaStatsNotPv(this.level, this.listDef);
            this.spe = FormulaStatsNotPv(this.level, this.listSpe);
            this.spd = FormulaStatsNotPv(this.level, this.listSpd);
        }


        public string getName() { return this.name; }
        public int getLevel() { return this.level; }
        public List<string> getListType() {  return this.listType; }
        public int getPv() { return this.pv; }
        public int getAtk() {  return this.atk; }
        public int getDef() { return this.def; }
        public int getSpe() { return this.spe; }
        public int getSpd() { return this.spd; }
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

                this.Pv = FormulaStatsPv(this.level, this.listPv);
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
            }
        }




    }
}
