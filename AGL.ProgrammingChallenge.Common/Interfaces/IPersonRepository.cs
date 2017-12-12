using AGL.ProgrammingChallenge.Models;
using System.Collections.Generic;

namespace AGL.ProgrammingChallenge.Common.Interfaces
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Get a Dictionary of all the cats in alphabetical order under a heading of the gender of their owner 
        /// </summary>
        /// <param name="person">JSON Data</param>
        /// <returns>
        /// Key  :  gender
        /// Value:  a list of all the cats in alphabetical order
        /// </returns>
        Dictionary<string, List<Pet>> GetCatsOfSortingByOwnerGender(IEnumerable<Person> person);
    }
}
