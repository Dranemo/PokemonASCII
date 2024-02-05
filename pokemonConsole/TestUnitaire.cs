using NUnit.Framework;
using System;
using System.IO;

namespace pokemonConsole.Tests
{
    [TestFixture]
    public class CombatTests
    {
        [Test]
        public void TestLoopCombat_PokemonAdverseFaint()
        {
            // Arrange
            using (StringReader stringReader = new StringReader("1\n1\n"))
            {
                Console.SetIn(stringReader);

                StringWriter stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                Player player = new Player(); // Assurez-vous que vous avez une classe Player et créez une instance ici
                Combat.LoopCombat(player);

                // Extracting output and checking if Pokemon adverse faint message is present
                string output = stringWriter.ToString();
                Assert.IsTrue(output.Contains("Le Pokemon de l'adversaire a perdu !"));
            }
        }

        [Test]
        public void TestLoopCombat_PlayerFaint()
        {
            // Arrange
            using (StringReader stringReader = new StringReader("1\n4\n"))
            {
                Console.SetIn(stringReader);

                StringWriter stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                Player player = new Player(); // Assurez-vous que vous avez une classe Player et créez une instance ici
                Combat.LoopCombat(player);

                // Extracting output and checking if Player faint message is present
                string output = stringWriter.ToString();
                Assert.IsTrue(output.Contains("Le Pokemon du joueur a perdu !"));
            }
        }

        // Vous pouvez ajouter d'autres tests pour d'autres scénarios selon vos besoins
    }
}
