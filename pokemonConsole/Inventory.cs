using Microsoft.VisualBasic.FileIO;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Usefull;

namespace pokemonConsole
{
    class Item
    {
        public int id;
        public string name;

        private effect effectAtk;
        private string effect2;


        private string usage;

        public int quantity;

        private string fileCSV = AdresseFile.FileDirection + "CSV\\item.csv";

        public Item(int item_id, int quantity = 1)
        {
            using (StreamReader sr = new StreamReader(fileCSV))
            {
                string line;
                bool itemFound = false;

                line = sr.ReadLine();
                line = sr.ReadLine();

                while ((line = sr.ReadLine()) != null && !itemFound)
                {
                    string[] colonnes = line.Split(',');
                    if (item_id == int.Parse(colonnes[0]))
                    {
                        id = item_id;
                        name = colonnes[1];
                        string effect1 = colonnes[2];
                        effect2 = colonnes[3];
                        usage = colonnes[4];

                        this.quantity = quantity;

                        effectAtk = GetEffect(effect1);
                    }
                }
            }
        }
        private enum effect
        { 
            none,
            CatchPokemon,
            HealPV,
            Guerison,
            Curestatut,
            Revive,
            TenPPAllCap,
            AllPPAllCap,
            TenPPOneCap,
            AllPPOneCap,
            EVPV,
            EVATK,
            EVDEF,
            EVSPE,
            EVSPD,
            PPPlus,
            LevelUp,
            Evolve,
            ADDAtk,
            ADDDef,
            ADDSpe,
            ADDSpd,
            ImmuneStats,
            ADDCrit,
            CantFail
        }
        private effect GetEffect(string effect_)
        {
            if (effect_ == "CATCHPOKEMON") return effect.CatchPokemon;
            else if (effect_ == "HEALPV") return effect.HealPV;
            else if (effect_ == "GUERISON") return effect.Guerison;
            else if (effect_ == "CURESTATUT") return effect.Curestatut;
            else if (effect_ == "REVIVE") return effect.Revive;
            else if (effect_ == "10PPALLCAP") return effect.TenPPAllCap;
            else if (effect_ == "ALLPPALLCAP") return effect.AllPPAllCap;
            else if (effect_ == "10PP1CAP") return effect.TenPPOneCap;
            else if (effect_ == "ALLPP1CAP") return effect.AllPPOneCap;
            else if (effect_ == "EVPV") return effect.EVPV;
            else if (effect_ == "EVATK") return effect.EVATK;
            else if (effect_ == "EVDEF") return effect.EVDEF;
            else if (effect_ == "EVSPE") return effect.EVSPE;
            else if (effect_ == "EVSPD") return effect.EVSPD;
            else if (effect_ == "PPPLUS") return effect.PPPlus;
            else if (effect_ == "LEVELUP") return effect.LevelUp;
            else if (effect_ == "EVOLVE") return effect.Evolve;
            else if (effect_ == "ADDATK") return effect.ADDAtk;
            else if (effect_ == "ADDDEF") return effect.ADDDef;
            else if (effect_ == "ADDSPE") return effect.ADDSpe;
            else if (effect_ == "ADDSPE") return effect.ADDSpd;
            else if (effect_ == "IMMUNE") return effect.ImmuneStats;
            else if (effect_ == "ADDCRIT") return effect.ADDCrit;
            else if (effect_ == "CANTFAIL") return effect.CantFail;

            return effect.none;
        }




        static public void UseItem(Item item, ref Pokemon pokemon, ref Capacity capacity, bool isCombat = false, Pokemon pokemonAdverse = null, Player player = null)
        {
            if((item.usage == "NOTBATTLE" && !isCombat) || (item.usage == "BOTH" && !isCombat))
            {
                switch (item.effectAtk)
                {
                    case effect.HealPV:
                        if (pokemon.pvLeft < pokemon.pv && pokemon.pvLeft != 0)
                        {
                            try
                            {
                                int healInt = int.Parse(item.effect2);
                                if(pokemon.pvLeft + healInt > pokemon.pv)
                                {
                                    pokemon.pvLeft = pokemon.pv;
                                }
                                else
                                {
                                    pokemon.pvLeft += healInt;
                                }
                            }
                            catch
                            {
                                pokemon.pvLeft = pokemon.pv;
                            }
                            item.quantity--;
                        }
                        break;
                    case effect.Guerison:
                        if ((pokemon.pvLeft < pokemon.pv && pokemon.pvLeft != 0) || pokemon.statusProblem != "OK")
                        {
                            pokemon.pvLeft = pokemon.pv;
                            pokemon.statusProblem = "OK";


                            item.quantity--;
                        }
                        break;
                    case effect.Curestatut:
                        if (pokemon.statusProblem != "OK")
                        {
                            switch (item.effect2)
                            {
                                case "PSN":
                                    if (pokemon.statusProblem == "PSN" || pokemon.statusProblem == "PSNGRAVE") pokemon.statusProblem = "OK";
                                    break;
                                case "PAR":
                                    if (pokemon.statusProblem == "PAR") pokemon.statusProblem = "OK";
                                    break;
                                case "BRN":
                                    if (pokemon.statusProblem == "BRN") pokemon.statusProblem = "OK";
                                    break;
                                case "SLP":
                                    if (pokemon.statusProblem == "SLP") pokemon.statusProblem = "OK";
                                    break;
                                case "ALL":
                                    pokemon.statusProblem = "OK";
                                    break;
                            }

                            item.quantity--;
                        }
                        break;
                    case effect.Revive:
                        if(pokemon.pvLeft <= 0)
                        {
                            pokemon.pvLeft = pokemon.pv * int.Parse(item.effect2) / 100;
                            pokemon.ko = false;
                            item.quantity--;
                        }
                        break;
                    case effect.TenPPAllCap:
                        foreach (Capacity cap in pokemon.listAttackActual)
                        {
                            if (cap.ppLeft + 10 > cap.pp) cap.ppLeft = cap.pp;
                            else cap.ppLeft += 10;
                        }
                        item.quantity--;
                        break;
                    case effect.AllPPAllCap:
                        foreach (Capacity cap in pokemon.listAttackActual)
                        {
                            cap.ppLeft = cap.pp;
                        }
                        item.quantity--;
                        break;
                    case effect.TenPPOneCap:
                        if(capacity.ppLeft < capacity.pp)
                        {
                            if(capacity.ppLeft + 10 > capacity.pp) capacity.ppLeft = capacity.pp;
                            else capacity.ppLeft += 10;
                            item.quantity--;
                        }
                        break;
                    case effect.AllPPOneCap:
                        if (capacity.ppLeft < capacity.pp)
                        {
                            capacity.ppLeft = capacity.pp;
                            item.quantity--;
                        }
                        break;
                    case effect.EVPV:
                        if (pokemon.listPv[2] < 65535) { pokemon.GainEV(int.Parse(item.effect2), 0, 0, 0, 0); item.quantity--; }

                        break;
                    case effect.EVATK:
                        if (pokemon.listAtk[2] < 65535) { pokemon.GainEV(0, int.Parse(item.effect2), 0, 0, 0); item.quantity--; }
                        break;
                    case effect.EVDEF:
                        if (pokemon.listDef[2] < 65535) { pokemon.GainEV(0, 0, int.Parse(item.effect2), 0, 0); item.quantity--; }
                            break;
                    case effect.EVSPE:
                        if (pokemon.listSpe[2] < 65535) { pokemon.GainEV(0, 0, 0, int.Parse(item.effect2), 0); item.quantity--; }
                            break;
                    case effect.EVSPD:
                        if (pokemon.listSpd[2] < 65535) { pokemon.GainEV(0, 0, 0, 0, int.Parse(item.effect2)); item.quantity--; }
                            break;
                    case effect.PPPlus:
                        if(capacity.pp < capacity.ppMax)
                        {
                            int ppAdded = capacity.ppOriginal * 20 / 100;

                            capacity.pp += ppAdded;
                            capacity.ppLeft += ppAdded;
                            item.quantity--;
                        }
                        break;
                    case effect.LevelUp:
                        if(pokemon.level < 100)
                        {
                            pokemon.LevelUp(true);
                            item.quantity--;
                        }
                        break;
                    case effect.Evolve:
                        bool evolved = false;
                        foreach (int id in pokemon.evolutionItemId)
                        {
                            if (item.id == id) { pokemon.Evolution(); evolved = true; }
                        }
                        if (evolved)
                        {

                            item.quantity--;
                        }
                        break;


                }
            }
            else if (item.usage == "NOTBATTLE" && isCombat)
            {
                Console.WriteLine("Unusable");
            }
            else if ((item.usage == "BATTLE" && isCombat) || (item.usage == "BOTH" && isCombat))
            {
                switch (item.effectAtk)
                {
                    case effect.CatchPokemon:
                        if(pokemonAdverse.appartenant == 0)
                        {
                            Player.catchPokemon(pokemonAdverse, player, int.Parse(item.effect2));
                            item.quantity--;
                        }
                        break;
                    case effect.HealPV:
                        if (pokemon.pvLeft < pokemon.pv && pokemon.pvLeft != 0)
                        {
                            try
                            {
                                int healInt = int.Parse(item.effect2);
                                if (pokemon.pvLeft + healInt > pokemon.pv)
                                {
                                    pokemon.pvLeft = pokemon.pv;
                                }
                                else
                                {
                                    pokemon.pvLeft += healInt;
                                }
                            }
                            catch
                            {
                                pokemon.pvLeft = pokemon.pv;
                            }
                            item.quantity--;
                        }
                        break;
                    case effect.Guerison:
                        if ((pokemon.pvLeft < pokemon.pv && pokemon.pvLeft != 0) || pokemon.statusProblem != "OK")
                        {
                            pokemon.pvLeft = pokemon.pv;
                            pokemon.statusProblem = "OK";
                            item.quantity--;
                        }
                        break;
                    case effect.Curestatut:
                        if (pokemon.statusProblem != "OK")
                        {
                            switch (item.effect2)
                            {
                                case "PSN":
                                    if (pokemon.statusProblem == "PSN" || pokemon.statusProblem == "PSNGRAVE") pokemon.statusProblem = "OK";
                                    break;
                                case "PAR":
                                    if (pokemon.statusProblem == "PAR") pokemon.statusProblem = "OK";
                                    break;
                                case "BRN":
                                    if (pokemon.statusProblem == "BRN") pokemon.statusProblem = "OK";
                                    break;
                                case "SLP":
                                    if (pokemon.statusProblem == "SLP") pokemon.statusProblem = "OK";
                                    break;
                                case "ALL":
                                    pokemon.statusProblem = "OK";
                                    break;
                            }
                            item.quantity--;
                        }
                        break;
                    case effect.Revive:
                        if (pokemon.pvLeft <= 0)
                        {
                            pokemon.pvLeft = pokemon.pv * int.Parse(item.effect2) / 100;
                            pokemon.ko = false;
                            item.quantity--;
                        }
                        break;
                    case effect.ADDAtk:
                        Combat.PrintInEmptyMenu($"L'attaque de {pokemon.name} a augmenté !");
                        pokemon.atkCombat = (int)(pokemon.atkCombat * 1.5);
                        item.quantity--;
                        break;
                    case effect.ADDDef:
                        Combat.PrintInEmptyMenu($"La défense de {pokemon.name} a augmenté !");
                        pokemon.defCombat = (int)(pokemon.defCombat * 1.5);
                        item.quantity--;
                        break;
                    case effect.ADDSpe:
                        Combat.PrintInEmptyMenu($"La vitesse de {pokemon.name} a augmenté !");
                        pokemon.spdCombat = (int)(pokemon.spdCombat * 1.5);
                        item.quantity--;
                        break;
                    case effect.ADDSpd:
                        Combat.PrintInEmptyMenu($"Le special de {pokemon.name} a augmenté !");
                        pokemon.speCombat = (int)(pokemon.speCombat * 1.5);
                        item.quantity--;
                        break;
                    case effect.ImmuneStats:
                        item.quantity--;
                        break;
                    case effect.ADDCrit:
                        item.quantity--;
                        break;
                    case effect.CantFail:
                        item.quantity--;
                        break;



                }
                }
            else if (item.usage == "BATTLE" && !isCombat)
            {
                Console.WriteLine("Unusable");
            }
        }

        public void JeterItem(Player player, int quantity)
        {
            if (this.quantity - quantity > 0)
            {
                this.quantity -= quantity;
            }
            else
            {
                player.inventory.Remove(this);
            }
        }
    }
}
