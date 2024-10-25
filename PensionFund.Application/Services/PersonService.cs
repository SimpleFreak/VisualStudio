using PensionFund.Application.Interfaces;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Services
{
    public class PersonService(IPersonRepository personRepository) : IPersonService
    {
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<List<Person>> GetAllPersons()
        {
            return await _personRepository.Get();
        }

        public async Task<Guid> CreatePerson(Person person)
        {
            return await _personRepository.Create(person);
        }

        public async Task<Guid> UpdatePerson(Guid id, string fullName, int age, string gender,
            decimal salary, string workExperience, string cityResidence)
        {
            return await _personRepository.Update(id, fullName, age, gender, salary,
                workExperience, cityResidence);
        }

        public async Task<Guid> DeletePerson(Guid id)
        {
            return await _personRepository.Delete(id);
        }
    }
}
