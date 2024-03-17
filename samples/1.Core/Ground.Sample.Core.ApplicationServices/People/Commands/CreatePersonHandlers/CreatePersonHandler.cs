using Ground.Core.ApplicationServices.Commands;
using Ground.Core.RequestResponse.Commands;
using Ground.Samples.Core.Contracts.People.Commands;
using Ground.Samples.Core.Domain.People.Entities;
using Ground.Samples.Core.Domain.People.ValueObjects;
using Ground.Utilities;

namespace Ground.Samples.Core.ApplicationServices.People.Commands.CreatePersonHandlers
{
    internal class CreatePersonHandler : CommandHandler<CreatePerson, long>
    {
        private readonly IPersonCommandRepository _repository;

        public CreatePersonHandler(GroundServices groundServices,IPersonCommandRepository repository) : base(groundServices)
        {
            _repository = repository;
        }

        public override async Task<CommandResult<long>> Handle(CreatePerson command)
        {
            Person person = new Person(new FirstName(command.FirstName),new LastName(command.LastName));
            await _repository.InsertAsync(person);
            await _repository.CommitAsync();
            return Ok(person.Id);
        }
    }
}
