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
                    Thread.Sleep(1000);
                    return SecondaryEffect.AtkUp;
                case "defup":
                    Console.Write("defup");
                    Thread.Sleep(1000);
                    return SecondaryEffect.DefUp;
                case "speedup":
                    Console.Write("speedup");
                    Thread.Sleep(1000);
                    return SecondaryEffect.SpeedUp;
                case "speup":
                    Console.Write("speup");
                    Thread.Sleep(1000);
                    return SecondaryEffect.SpeUp;
                case "esquiveup":
                    Console.Write("esquiveup");
                    Thread.Sleep(1000);
                    return SecondaryEffect.EsquiveUp;
                case "critup":
                    Console.Write("critup");
                    Thread.Sleep(1000);
                    return SecondaryEffect.CritUp;
                case "critboost":
                    Console.Write("critboost");
                    Thread.Sleep(1000);
                    return SecondaryEffect.CritBoost;
                case "atkdown":
                    Console.Write("atkdown");
                    Thread.Sleep(1000); 
                    pokemonAdverse.atk = (int)(pokemonAdverse.atk * 0.88);
                    return SecondaryEffect.AtkDown;
                case "chanceatkdown":
                    Console.Write("chanceatkdown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ChanceAtkDown;
                case "defdown":
                    Console.Write("defdown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.DefDown;
                case "chancedefdown":
                    Console.Write("chancedefdown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ChanceDefDown;
                case "speeddown":
                    Console.Write("speeddown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.SpeedDown;
                case "chancespeeddown":
                    Console.Write("chancespeeddown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ChanceSpeedDown;
                case "spedown":
                    Console.Write("spedown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.SpeDown;
                case "precisiondown":
                    Console.Write("precisiondown");
                    Thread.Sleep(1000);
                    return SecondaryEffect.PrecisionDown;
                case "para":
                    Console.Write("para");
                Thread.Sleep(1000);
                    return SecondaryEffect.Para;
                case "burn":
                    Console.Write("burn");
                Thread.Sleep(1000);
                    return SecondaryEffect.Burn;
                case "freeze":
                    Console.Write("freeze");
                Thread.Sleep(1000); 
                    return SecondaryEffect.Freeze;
                case "sleep":
                    Console.Write("sleep");
                Thread.Sleep(1000);
                    return SecondaryEffect.Sleep;
                case "selfSleep":
                    Console.Write("selfsleep");
                    Thread.Sleep(1000);
                    return SecondaryEffect.SelfSleep;
                case "poison":
                    Console.Write("Poison");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Poison;
                case "poisongrave":
                    Console.Write("PoisonGrave");
                    Thread.Sleep(1000);
                    return SecondaryEffect.PoisonGrave;
                case "confusion":
                    Console.Write("Confusion");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Confusion;
                case "randx":
                    Console.Write("randx");
                    Thread.Sleep(1000);
                    return SecondaryEffect.randx;
                case "moneyx2":
                    Console.Write("Moneyx2");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Moneyx2;
                case "ohko":
                    Console.Write("OHKO");
                    Thread.Sleep(1000);
                    return SecondaryEffect.OHKO;
                case "charge":
                    Console.Write("Charge");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Charge;
                case "damagex2":
                    Console.Write("Damagex2");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Damagex2;
                case "endfight":
                    Console.Write("EndFight");
                    Thread.Sleep(1000);
                    return SecondaryEffect.EndFight;
                case "imun":
                    Console.Write("Imun");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Imun;
                case "stuck":
                    Console.Write("Stuck");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Stuck;
                case "randt":
                    Console.Write("randt");
                    Thread.Sleep(1000);
                    return SecondaryEffect.randt;
                case "flinch":
                    Console.Write("Flinch");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Flinch;
                case "doublex":
                    Console.Write("doublex");
                    Thread.Sleep(1000);
                    return SecondaryEffect.doublex;
                case "doublet":
                    Console.Write("doublet");
                    Thread.Sleep(1000);
                    return SecondaryEffect.doublet;
                case "fail":
                    Console.Write("Fail");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Fail;
                case "stuckself":
                    Console.Write("StuckSelf");
                    Thread.Sleep(1000);
                    return SecondaryEffect.StuckSelf;
                case "recoil":
                    Console.Write("Recoil");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Recoil;
                case "constdamage":
                    Console.Write("ConstDamage");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ConstDamage;
                case "blockAttack":
                    Console.Write("BlockAttack");
                    Thread.Sleep(1000);
                    return SecondaryEffect.BlockAttack;
                case "blockstatusmoves":
                    Console.WriteLine("Effect: BlockStatusMoves");
                    Thread.Sleep(1000);
                    return SecondaryEffect.BlockStatusMoves;

                case "pause":
                    Console.WriteLine("Effect: Pause");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Pause;
                case "counter":
                    Console.WriteLine("Effect: Counter");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Counter;

                case "vardamage":
                    Console.WriteLine("Effect: VarDamage");
                    Thread.Sleep(1000);
                    return SecondaryEffect.VarDamage;

                case "vardamagelevel":
                    Console.WriteLine("Effect: VarDamageLevel");
                    Thread.Sleep(1000);
                    return SecondaryEffect.VarDamageLevel;

                case "stealhp":
                    Console.WriteLine("Effect: StealHP");
                    Thread.Sleep(1000);
                    return SecondaryEffect.StealHP;

                case "constantstealhp":
                    Console.WriteLine("Effect: ConstantStealHP");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ConstantStealHP;

                case "stealhpifsleep":
                    Console.WriteLine("Effect: StealHPIfSleep");
                    Thread.Sleep(1000);
                    return SecondaryEffect.StealHPIfSleep;

                case "heal":
                    Console.WriteLine("Effect: Heal");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Heal;

                case "alwayscrit":
                    Console.WriteLine("Effect: AlwaysCrit");
                    Thread.Sleep(1000);
                    return SecondaryEffect.AlwaysCrit;

                case "priority":
                    Console.WriteLine("Effect: Priority");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Priority;

                case "copylastattack":
                    Console.WriteLine("Effect: CopyLastAttack");
                    Thread.Sleep(1000);
                    return SecondaryEffect.CopyLastAttack;

                case "cinqt":
                    Console.WriteLine("Effect: Cinqt");
                    Thread.Sleep(1000);
                    return SecondaryEffect.cinqt;

                case "resetstats":
                    Console.WriteLine("Effect: ResetStats");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ResetStats;

                case "counterpatience":
                    Console.WriteLine("Effect: CounterPatience");
                    Thread.Sleep(1000);
                    return SecondaryEffect.CounterPatience;

                case "randomattack":
                    Console.WriteLine("Effect: RandomAttack");
                    Thread.Sleep(1000);
                    return SecondaryEffect.RandomAttack;

                case "copylastattacktarget":
                    Console.WriteLine("Effect: CopyLastAttackTarget");
                    Thread.Sleep(1000);
                    return SecondaryEffect.CopyLastAttackTarget;

                case "death":
                    Console.WriteLine("Effect: Death");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Death;

                case "transformintoenemy":
                    Console.WriteLine("Effect: TransformIntoEnemy");
                    Thread.Sleep(1000);
                    return SecondaryEffect.TransformIntoEnemy;

                case "changetypetoenemy":
                    Console.WriteLine("Effect: ChangeTypeToEnemy");
                    Thread.Sleep(1000);
                    return SecondaryEffect.ChangeTypeToEnemy;

                case "clone":
                    Console.WriteLine("Effect: Clone");
                    Thread.Sleep(1000);
                    return SecondaryEffect.Clone;
                default:
                    Console.WriteLine($"Effect: {sideEffect} (Default)");
                    Thread.Sleep(1000);
                    return SecondaryEffect.None;
            }
        }
    }
}
