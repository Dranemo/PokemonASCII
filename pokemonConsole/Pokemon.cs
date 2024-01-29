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
        private int spd;
        private int spe;


        public Pokemon(string name_, int level_, int pv_, int atk_, int def_, int spd, int spe)
        {
            this.name = name_;
            this.level = level_;
            this.pv = pv_;
            this.atk = atk_;
            this.def = def_;
            this.spd = spd;
            this.spe = spe;
        }


        public string getName() { return name; }
        public int getLevel() { return level; }
        public int getPv() { return pv; }
        public int getAtk() {  return atk; }
        public int getDef() { return def; }
        public int getSpe() { return spe; }
        public int getSpd() { return spd; }




        public void setPv(int pv) { this.pv = pv;}
        public void setAtk(int atk) { this.atk = atk;}
    }
}
