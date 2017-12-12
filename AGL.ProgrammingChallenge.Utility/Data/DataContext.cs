using AGL.ProgrammingChallenge.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AGL.ProgrammingChallenge.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace AGL.ProgrammingChallenge.Utility.Data
{
    public class DataContext : IDataContext
    {
        public string peopleJsonUrl = @"http://agl-developer-test.azurewebsites.net/people.json";
   
        /// <summary>
        /// Get person Json result and then Deserialize to Object
        /// </summary>
        /// <returns>IQueryable Person</returns>
        public async Task<IEnumerable<Person>> GetPersonsFromJson()
        {
            using (var client = new HttpClient())
            using (var httpResponse = await client.GetAsync(peopleJsonUrl))
            using (var content = httpResponse.Content)
            {
                string jsonResult = await content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Person>>(jsonResult);

                return result.AsEnumerable();
            }
        }
    }
}
