using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Acme.SimpleTaskApp.Common;
using internPJ3.Person1;

namespace internPJ3.Common
{
    public class LookupAppService : internPJ3AppServiceBase, ILookupAppService
    {
        private readonly IRepository<Person, int> _personRepository;


        public LookupAppService(IRepository<Person, int> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<ListResultDto<ComboboxItemDto>> GetPeopleComboboxItems()
        {
            var people = await _personRepository.GetAllListAsync();
            return new ListResultDto<ComboboxItemDto>(
                people.Select(p => new ComboboxItemDto(p.Id.ToString("D"), p.Name)).ToList()
            );
        }
    }
}