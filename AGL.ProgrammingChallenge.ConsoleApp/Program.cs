using AGL.ProgrammingChallenge.Common.Interfaces;
using AGL.ProgrammingChallenge.Models;
using AGL.ProgrammingChallenge.Utility.Data;
using AGL.ProgrammingChallenge.Utility.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AGL.ProgrammingChallenge.ConsoleApp
{
    class Program
    {
        //Use C# 7.1, we can use async Main function
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // Setup our DI
            // Register application services.
            var serviceProvider = new ServiceCollection()
                            .AddScoped<IDataContext, DataContext>()
                            .AddScoped<IPersonRepository, PersonRepository>()
                            .BuildServiceProvider();

            //resolve services
            var dataRepository = serviceProvider.GetRequiredService<IDataContext>();
            var personRepository = serviceProvider.GetRequiredService<IPersonRepository>();

            // call Repository methord
            var personJsonData = await dataRepository.GetPersonsFromJson();
            Dictionary<string, List<Pet>> result = personRepository.GetCatsOfSortingByOwnerGender(personJsonData);
            
            // Console result 
            foreach (var res in result)
            {
                Console.WriteLine("{0}", res.Key);
                foreach (var pet in res.Value)
                {
                    Console.WriteLine("\t{0}", pet.Name);
                }
            }

            Console.ReadKey();

        }

    }
}
