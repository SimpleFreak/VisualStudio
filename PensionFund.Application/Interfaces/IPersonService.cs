using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Interfaces
{
    public interface IPersonService
    {
        Task<Guid> CreatePerson(Person person);

        Task<Guid> DeletePerson(Guid id);
        
        Task<List<Person>> GetAllPersons();
        
        Task<Guid> UpdatePerson(Guid id, string fullName, int age, string gender, decimal salary, string workExperience, string cityResidence);
    }
}