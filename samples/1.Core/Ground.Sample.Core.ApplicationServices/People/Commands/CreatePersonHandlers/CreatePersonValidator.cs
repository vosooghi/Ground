using FluentValidation;
using Ground.Extensions.Translations.Abstractions;
using Ground.Samples.Core.Contracts.People.Commands;
using Ground.Samples.Core.Domain.Shared;

namespace Ground.Samples.Core.ApplicationServices.People.Commands.CreatePersonHandlers
{
    public class CreatePersonValidator:AbstractValidator<CreatePerson>
    {
        private readonly ITranslator _translator;

        public CreatePersonValidator(ITranslator translator)
        {
            RuleFor(w => w.FirstName).NotEmpty().WithMessage(translator[Messages.InvalidNullValue, Messages.FirstName]);
            
        }
    }
}
