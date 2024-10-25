using Microsoft.EntityFrameworkCore;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.DataAccess.Repositories
{
    /* Класс репозитория для сущности Человек с основными CRUD запросами для дальнейшего использования в контроллерах */
    public class PersonRepository(PensionFundDbContext context) : IPersonRepository
    {
        private readonly PensionFundDbContext _context = context;

        public async Task<List<Person>> Get()
        {
            var person = await _context.Persons
                .AsNoTracking()
                .ToListAsync();

            var persons = person
                .Select(person => Person
                    .Create(person.Id, person.FullName, person.Age, person.Gender,
                        person.Salary, person.WorkExperience, person.CityResidence).Person)
                .ToList();

            return persons;
        }

        public async Task<Guid> Create(Person person)
        {
            var personEntity = Person.Create(person.Id, person.FullName,
                person.Age, person.Gender, person.Salary, person.WorkExperience,
                person.CityResidence);

            await _context.Persons.AddAsync(personEntity.Person);
            await _context.SaveChangesAsync();

            return personEntity.Person.Id;
        }

        public async Task<Guid> Update(Guid id, string fullName, int age,
            string gender, decimal salary, string workExperience, string cityResidence)
        {
            await _context.Persons
                .Where(person => person.Id == id)
                .ExecuteUpdateAsync(set => set
                .SetProperty(person => person.FullName, person => fullName)
                .SetProperty(person => person.Age, person => age)
                .SetProperty(person => person.Gender, person => gender)
                .SetProperty(person => person.Salary, person => salary)
                .SetProperty(person => person.WorkExperience, person => workExperience)
                .SetProperty(person => person.CityResidence, person => cityResidence));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Persons
                .Where(person => person.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
