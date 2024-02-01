using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonConsole
{
    internal class Capacity
    {

        public int id { get; private set; }
        public string name { get; private set; }
        private string type;
        private int categorie;

        private string puissance;
        private string precision;
        private int pp;
        private int ppMax;

        private string sideEffect;
        private string sideEffectInfo;


        private int ppLeft;


        private string fileCSV = "C:\\Users\\yanae\\Desktop\\C-Pokemon\\pokemonConsole\\capacites.csv";

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

                        puissance = colonnes[4];
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
    }
}
