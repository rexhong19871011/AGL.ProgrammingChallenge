using AGL.ProgrammingChallenge.Common.Interfaces;
using AGL.ProgrammingChallenge.Models;
using AGL.ProgrammingChallenge.Utility.Data;
using AGL.ProgrammingChallenge.Utility.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AGL.ProgrammingChallenge.UnitTest
{
    // xUnit test for main sort algorithm
    public class TestPersonRepository
    {
        public IServiceProvider serviceProvider;
        public IEnumerable<Person> personTestData;
        public IDataContext dataRepository;
        public IPersonRepository personRepository;

        public TestPersonRepository()
        {
            #region Init
            // Setup our DI
            // Register application services.
            serviceProvider = new ServiceCollection()
                            .AddScoped<IDataContext, DataContext>()
                            .AddScoped<IPersonRepository, PersonRepository>()
                            .BuildServiceProvider();

            dataRepository = serviceProvider.GetRequiredService<IDataContext>();
            personRepository = serviceProvider.GetRequiredService<IPersonRepository>();

            #endregion
        }

        [Fact]
        public void Test_GetCatsOfSortingByOwnerGender_Success()
        {

            // json test data
            var jsonTestData = "[{'name':'Bob','gender':'Male','age':23,'pets':[{'name':'A','type':'Cat'},{'name':'Fido','type':'Dog'}]},{'name':'Steve','gender':'Male','age':45,'pets':null},{'name':'Alice','gender':'Female','age':64,'pets':[{'name':'D','type':'Cat'},{'name':'Nemo','type':'Fish'},{'name':'B','type':'Cat'}]}]";

            personTestData = JsonConvert.DeserializeObject<List<Person>>(jsonTestData).AsEnumerable();

            Dictionary<string, List<Pet>> result = personRepository.GetCatsOfSortingByOwnerGender(personTestData);

            int expectedGenderCount = 2;
            string firstRowGender = "Male";
            string secondRowGender = "Female";
            string firstRowGenderMaleCatName = "A";
            string firstRowGenderFemaleCatName = "B";
            Assert.Equal(expectedGenderCount, result.Count);
            Assert.Equal(firstRowGender, result.Keys.ToList()[0]);
            Assert.Equal(secondRowGender, result.Keys.ToList()[1]);

            //test cat
            Assert.Equal(firstRowGenderMaleCatName, result["Male"][0].Name);
            Assert.Equal(firstRowGenderFemaleCatName, result["Female"][0].Name);

        }

        [Fact]
        public void Test_GetCatsOfSortingByOwnerGender_IsNull_Count()
        {

            // json test data
            var jsonTestData = "[{'name':'Steve','gender':'Male','age':45,'pets':null}]";

            personTestData = JsonConvert.DeserializeObject<List<Person>>(jsonTestData).AsEnumerable();

            Dictionary<string, List<Pet>> result = personRepository.GetCatsOfSortingByOwnerGender(personTestData);

            int expectedIsNullPetCount = 0;
           
            Assert.Equal(expectedIsNullPetCount, result.Count);
           
        }
    }
}
