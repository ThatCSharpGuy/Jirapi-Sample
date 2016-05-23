using Jirapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jirapi.Resources;

namespace JirapiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            Console.Read();
        }

        static async Task MainAsync()
        {
            var pokeClient = new PokeClient();

            var dexes = await pokeClient.GetResourceList<Pokedex>();

            Console.WriteLine("Pokedexes");
            foreach (var dex in dexes.Results)
            {
                Console.WriteLine(dex.Name);
            }

            var kantoDex = await dexes.Results
                .First(d => d.Name.Equals("kanto"))
                .GetResource();

            Console.WriteLine("Kanto dex:");
            foreach (var description in kantoDex.Descriptions)
            {
                Console.WriteLine("\t ({0}): {1}",description.Language.Name, description.Description1);
            }

            var pokemonCount = 10;
            Console.WriteLine("Primeros " + pokemonCount + " pokémons:");
            for (int i = 0; i < pokemonCount; i++)
            {
                Console.WriteLine("\t{0}.- {1}", i+1, kantoDex.PokemonEntries[i].PokemonSpecies.Name);
            }

            Console.WriteLine("Consulta a pokémon");
            var pikachu = await pokeClient.Get<Pokemon>("pikachu");
            Console.WriteLine(pikachu.Name);

            var jirachi = await pokeClient.Get<Pokemon>(385);
            Console.WriteLine(jirachi.BaseExperience);

            var netBall = await pokeClient.GetByUrl<Item>("http://pokeapi.co/api/v2/item/6");
            Console.WriteLine(netBall.Name);

        }
    }
}
