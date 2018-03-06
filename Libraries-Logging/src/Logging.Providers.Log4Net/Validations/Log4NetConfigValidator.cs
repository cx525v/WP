using FluentValidation;
using Worldpay.Extensions;
using Worldpay.Logging.Providers.Log4Net.Models;

namespace Worldpay.Logging.Providers.Log4Net.Validations
{
    public class Log4NetConfigValidator : AbstractValidator<Log4NetConfig>
    {
        public Log4NetConfigValidator()
        {
            RuleFor(model => model.Appenders)
                .NotEmpty().WithMessage(AppMessages.Validation.AtLeastOneShouldBeDefined.ParseIn(nameof(Log4NetAppenderConfig)));

            RuleFor(model => model.Appenders).SetCollectionValidator(new Log4NetAppenderConfigValidator());
        }
    }
}
