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
        public SecondaryEffect secondaryEffect { get; set; }
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
        public void Use(Pokemon pokemon, Pokemon pokemonAdverse)
        {
            ApplyEffect(sideEffect, pokemon, pokemonAdverse);
        }
        public SecondaryEffect ApplyEffect(string sideEffect, Pokemon pokemon, Pokemon pokemonAdverse)
        {
            // Ajoutez des cas pour chaque effet possible
            switch (sideEffect.ToLower())
            {
                case "atkup":
                    Console.Write("atkup");
                    return SecondaryEffect.AtkUp;

                case "defup":
                    Console.Write("defup");
                    return SecondaryEffect.DefUp;

                case "speedup":
                    Console.Write("speedup");
                    return SecondaryEffect.SpeedUp;

                case "speup":
                    Console.Write("speup");
                    return SecondaryEffect.SpeUp;

                case "esquiveup":
                    Console.Write("esquiveup");
                    return SecondaryEffect.EsquiveUp;

                case "critup":
                    Console.Write("critup");
                    return SecondaryEffect.CritUp;

                case "critboost":
                    Console.Write("critboost");
                    return SecondaryEffect.CritBoost;

                case "atkdown":
                    Console.Write($"L'attaque du {pokemonAdverse.name} a baissé !");
                    pokemonAdverse.atkCombat = (int)(pokemonAdverse.atkCombat * 0.88);
                    return SecondaryEffect.AtkDown;

                case "chanceatkdown":
                    Console.Write("chanceatkdown");
                    return SecondaryEffect.ChanceAtkDown;

                case "defdown":
                    Console.Write($"La défense du {pokemonAdverse.name} a baissé !");
                    pokemonAdverse.defCombat = (int)(pokemonAdverse.defCombat * 0.88);
                    return SecondaryEffect.DefDown;

                case "chancedefdown":
                    Console.Write("chancedefdown");
                    return SecondaryEffect.ChanceDefDown;

                case "speeddown":
                    Console.Write($"La vitesse du {pokemonAdverse.name} a baissé !");
                    pokemonAdverse.spdCombat = (int)(pokemonAdverse.spdCombat * 0.88);
                    return SecondaryEffect.SpeedDown;

                case "chancespeeddown":
                    Console.Write("chancespeeddown");
                    return SecondaryEffect.ChanceSpeedDown;

                case "spedown":
                    Console.Write($"Le special du {pokemonAdverse.name} a baissé !");
                    pokemonAdverse.speCombat = (int)(pokemonAdverse.speCombat * 0.88);
                    return SecondaryEffect.SpeDown;

                case "precisiondown":
                    Console.Write("precisiondown");
                    return SecondaryEffect.PrecisionDown;

                case "para":
                    Console.Write("para");
                    return SecondaryEffect.Para;

                case "burn":
                    Console.Write("burn");
                    return SecondaryEffect.Burn;

                case "freeze":
                    Console.Write("freeze");
                    return SecondaryEffect.Freeze;

                case "sleep":
                    Console.Write("sleep");
                    return SecondaryEffect.Sleep;

                case "selfSleep":
                    Console.Write("selfsleep");
                    return SecondaryEffect.SelfSleep;

                case "poison":
                    Console.Write("Poison");
                    return SecondaryEffect.Poison;

                case "poisongrave":
                    Console.Write("PoisonGrave");
                    return SecondaryEffect.PoisonGrave;

                case "confusion":
                    Console.Write("Confusion");
                    return SecondaryEffect.Confusion;

                case "randx":
                    Console.Write("randx");
                    return SecondaryEffect.randx;

                case "moneyx2":
                    Console.Write("Moneyx2");
                    return SecondaryEffect.Moneyx2;

                case "ohko":
                    Console.Write("OHKO");
                    return SecondaryEffect.OHKO;

                case "charge":
                    Console.Write("Charge");
                    return SecondaryEffect.Charge;

                case "damagex2":
                    Console.Write("Damagex2");
                    return SecondaryEffect.Damagex2;

                case "endfight":
                    Console.Write("EndFight");
                    return SecondaryEffect.EndFight;

                case "imun":
                    Console.Write("Imun");
                    return SecondaryEffect.Imun;

                case "stuck":
                    Console.Write("Stuck");
                    return SecondaryEffect.Stuck;

                case "randt":
                    Console.Write("randt");
                    return SecondaryEffect.randt;

                case "flinch":
                    Console.Write("Flinch");
                    return SecondaryEffect.Flinch;

                case "doublex":
                    Console.Write("doublex");
                    return SecondaryEffect.doublex;

                case "doublet":
                    Console.Write("doublet");
                    return SecondaryEffect.doublet;

                case "fail":
                    Console.Write("Fail");
                    return SecondaryEffect.Fail;

                case "stuckself":
                    Console.Write("StuckSelf");
                    return SecondaryEffect.StuckSelf;

                case "recoil":
                    Console.Write("Recoil");
                    return SecondaryEffect.Recoil;

                case "constdamage":
                    Console.Write("ConstDamage");
                    return SecondaryEffect.ConstDamage;

                case "blockAttack":
                    Console.Write("BlockAttack");
                    return SecondaryEffect.BlockAttack;

                case "blockstatusmoves":
                    Console.WriteLine("Effect: BlockStatusMoves");
                    return SecondaryEffect.BlockStatusMoves;

                case "pause":
                    Console.WriteLine("Effect: Pause");
                    return SecondaryEffect.Pause;

                case "counter":
                    Console.WriteLine("Effect: Counter");
                    return SecondaryEffect.Counter;

                case "vardamage":
                    Console.WriteLine("Effect: VarDamage");
                    return SecondaryEffect.VarDamage;

                case "vardamagelevel":
                    Console.WriteLine("Effect: VarDamageLevel");
                    return SecondaryEffect.VarDamageLevel;

                case "stealhp":
                    Console.WriteLine("Effect: StealHP");
                    return SecondaryEffect.StealHP;

                case "constantstealhp":
                    Console.WriteLine("Effect: ConstantStealHP");
                    return SecondaryEffect.ConstantStealHP;

                case "stealhpifsleep":
                    Console.WriteLine("Effect: StealHPIfSleep");
                    return SecondaryEffect.StealHPIfSleep;

                case "heal":
                    Console.WriteLine("Effect: Heal");
                    return SecondaryEffect.Heal;

                case "alwayscrit":
                    Console.WriteLine("Effect: AlwaysCrit");
                    return SecondaryEffect.AlwaysCrit;

                case "priority":
                    Console.WriteLine("Effect: Priority");
                    return SecondaryEffect.Priority;

                case "copylastattack":
                    Console.WriteLine("Effect: CopyLastAttack");
                    return SecondaryEffect.CopyLastAttack;

                case "cinqt":
                    Console.WriteLine("Effect: Cinqt");
                    return SecondaryEffect.cinqt;

                case "resetstats":
                    Console.WriteLine("Effect: ResetStats");
                    return SecondaryEffect.ResetStats;

                case "counterpatience":
                    Console.WriteLine("Effect: CounterPatience");
                    return SecondaryEffect.CounterPatience;

                case "randomattack":
                    Console.WriteLine("Effect: RandomAttack");
                    return SecondaryEffect.RandomAttack;

                case "copylastattacktarget":
                    Console.WriteLine("Effect: CopyLastAttackTarget");
                    return SecondaryEffect.CopyLastAttackTarget;

                case "death":
                    Console.WriteLine("Effect: Death");
                    return SecondaryEffect.Death;

                case "transformintoenemy":
                    Console.WriteLine("Effect: TransformIntoEnemy");
                    return SecondaryEffect.TransformIntoEnemy;

                case "changetypetoenemy":
                    Console.WriteLine("Effect: ChangeTypeToEnemy");
                    return SecondaryEffect.ChangeTypeToEnemy;

                case "clone":
                    Console.WriteLine("Effect: Clone");
                    return SecondaryEffect.Clone;

                default:
                    Console.WriteLine($"Effect: {sideEffect} (Default)");
                    return SecondaryEffect.None;
            }
        }
    }
}
