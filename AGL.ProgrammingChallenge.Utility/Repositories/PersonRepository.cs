using AGL.ProgrammingChallenge.Common.Interfaces;
using System.Collections.Generic;
using AGL.ProgrammingChallenge.Models;
using System.Linq;
using System.Threading;

namespace AGL.ProgrammingChallenge.Utility.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        /// <summary>
        /// Get a Dictionary of all the cats in alphabetical order under a heading of the gender of their owner 
        /// </summary>
        /// <param name="person">JSON Data</param>
        /// <returns>
        /// Key  :  gender
        /// Value:  a list of all the cats in alphabetical order
        /// </returns>
        public Dictionary<string, List<Pet>> GetCatsOfSortingByOwnerGender(IEnumerable<Person> person)
        {
            var query = from p in person
                        where p.Pets != null
                        group p by Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Gender) into g
                        select new
                        {
                            Gender = g.Key,
                            Pets = g.SelectMany(a => a.Pets)
                            .Where(b => b.Type == Models.Enums.PetType.Cat.ToString())
                            .OrderBy(c => c.Name)
                            .ToList(),
                        };

            var result = query.ToDictionary(p => p.Gender, p => p.Pets);
            return result;
        }
    }
}
