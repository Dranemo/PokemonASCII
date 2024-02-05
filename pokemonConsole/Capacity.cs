using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usefull;

namespace pokemonConsole
{
    internal class Capacity
    {

        public int id { get; private set; }
        public string name { get; private set; }
        public string type { get; private set; }
        public int categorie { get; private set; }

        public int puissance { get; private set; }
        private string precision;
        public int pp;
        public int ppLeft;
        private int ppMax;

        private string sideEffect;
        private string sideEffectInfo;



        private string fileCSV = AdresseFile.FileDirection + "CSV\\capacites.csv";

        //lis le CSV
        public Capacity(int id_) 
        {
            using (StreamReader sr = new StreamReader(this.fileCSV))
            {
                string line;
                bool AttackFound = false;

                line = sr.ReadLine();
                line = sr.ReadLine();

                while ((line = sr.ReadLine()) != null && !AttackFound)
                {
                    string[] colonnes = line.Split(',');

                    int.TryParse(colonnes[0], out int id_search);

                    if (id_search == id_)
                    {
                        id = id_;
                        name = colonnes[1];
                        type = colonnes[2];
                        categorie = int.Parse(colonnes[3]);

                        puissance = int.Parse(colonnes[4]);
                        precision = colonnes[5];
                        pp = int.Parse(colonnes[6]);
                        ppMax = int.Parse(colonnes[7]);

                        sideEffect = colonnes[8];
                        sideEffectInfo = colonnes[9];


                        ppLeft = pp;
                    }
                }
            }
        }
        //lis la save
        public Capacity(int id_, int pp_, int ppLeft_)
        {
            using (StreamReader sr = new StreamReader(this.fileCSV))
            {
                string line;
                bool AttackFound = false;

                line = sr.ReadLine();
                line = sr.ReadLine();

                while ((line = sr.ReadLine()) != null && !AttackFound)
                {
                    string[] colonnes = line.Split(',');

                    int.TryParse(colonnes[0], out int id_search);

                    if (id_search == id_)
                    {
                        id = id_;
                        name = colonnes[1];
                        type = colonnes[2];
                        categorie = int.Parse(colonnes[3]);

                        puissance = int.Parse(colonnes[4]);
                        precision = colonnes[5];
                        pp = pp_;
                        ppMax = int.Parse(colonnes[7]);

                        sideEffect = colonnes[8];
                        sideEffectInfo = colonnes[9];


                        ppLeft = ppLeft_;
                    }
                }
            }
        }
        public enum SecondaryEffect
        {
            None,
            AtkUp,
            DefUp,
            SpeedUp,
            SpeUp,
            EsquiveUp,
            CritUp,
            CritBoost,
            AtkDown,
            ChanceAtkDown,
            DefDown,
            ChanceDefDown,
            SpeedDown,
            ChanceSpeedDown,
            SpeDown,
            PrecisionDown,
            Para,
            Burn,
            Freeze,
            Sleep,
            SelfSleep,
            Poison,
            PoisonGrave,
            Confusion,
            randx,
            Moneyx2,
            OHKO,
            Charge,
            Damagex2,
            EndFight,
            Imun,
            Stuck,
            randt,
            Flinch,
            doublex,
            doublet,
            Fail,
            StuckSelf,
            Recoil,
            ConstDamage,
            BlockAttack,
            BlockStatusMoves,
            Pause,
            Counter,
            VarDamage,
            VarDamageLevel,
            StealHP,
            ConstantStealHP,
            StealHPIfSleep,
            Heal,
            AlwaysCrit,
            Priority,
            CopyLastAttack,
            cinqt,
            ResetStats,
            CounterPatience,
            RandomAttack,
            CopyLastAttackTarget,
            Death,
            TransformIntoEnemy,
            ChangeTypeToEnemy,
            Clone,
        }
    }
}
