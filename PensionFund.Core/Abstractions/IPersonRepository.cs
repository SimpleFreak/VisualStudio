using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Core.Abstractions
{
    public interface IPersonRepository
    {
        Task<List<Person>> Get();
        Task<Guid> Create(Person person);
        Task<Guid> Update(Guid id, string fullName, int age, string gender, decimal salary, string workExperience, string cityResidence);
        Task<Guid> Delete(Guid id);
    }
}