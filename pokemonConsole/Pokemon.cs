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


        public Pokemon(int level_, string name_, List<string> listType_, List<int> listPv_, List<int> listAtk_, List<int> listDef_, List<int> listSpd_, List<int> listSpe_)
        {
            this.name = name_;
            this.level = level_;
            this.listType = listType_;

            this.listPv = listPv_;
            this.listAtk = listAtk_;
            this.listDef = listDef_;
            this.listSpd = listSpd_;
            this.listSpe = listSpe_;

            this.pv = FormulaStatsPv(level, listPv);
            this.atk = FormulaStatsNotPv(level, listAtk);
            this.def = FormulaStatsNotPv(level, listDef);
            this.spe = FormulaStatsNotPv(level, listSpe);
            this.spd = FormulaStatsNotPv(level, listSpd);
        }


        public string getName() { return name; }
        public int getLevel() { return level; }
        public List<string> getListType() {  return listType; }
        public int getPv() { return pv; }
        public int getAtk() {  return atk; }
        public int getDef() { return def; }
        public int getSpe() { return spe; }
        public int getSpd() { return spd; }



        private int FormulaStatsPv(int level, List<int> listPv)
        {

            return (int)(((((listPv[0] + listPv[1]) * 2 + Math.Sqrt(listPv[2]) / 4) * level) / 100) + level + 10);
        }

        private int FormulaStatsNotPv(int level, List<int> listStat)
        {
            return (int)(((((listStat[0] + listStat[1]) * 2 + Math.Sqrt(listStat[2]) / 4) * level) / 100) + 5);
        }
    }
}
