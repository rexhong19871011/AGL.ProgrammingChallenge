using AGL.ProgrammingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGL.ProgrammingChallenge.Common.Interfaces
{
    public interface IDataContext
    {
        /// <summary>
        /// Get person Json result
        /// </summary>
        Task<IEnumerable<Person>> GetPersonsFromJson();
    }
}
