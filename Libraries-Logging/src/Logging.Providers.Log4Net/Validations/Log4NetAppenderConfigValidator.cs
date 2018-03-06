using FluentValidation;
using Worldpay.Extensions;
using Worldpay.Logging.Providers.Log4Net.Enums;
using Worldpay.Logging.Providers.Log4Net.Models;

namespace Worldpay.Logging.Providers.Log4Net.Validations
{
    public class Log4NetAppenderConfigValidator : AbstractValidator<Log4NetAppenderConfig>
    {
        public Log4NetAppenderConfigValidator()
        {

	       
            When(model => model.AppenderType == AppenderTypes.RollingFile, () =>
            {
                RuleFor(model => model.File)
                    .NotEmpty().WithMessage(AppMessages.Validation.FieldCannotBeNullOrEmpty.ParseIn(nameof(Log4NetAppenderConfig.File)));
                RuleFor(model => model.ConversionPattern)
                    .NotEmpty().WithMessage(AppMessages.Validation.FieldCannotBeNullOrEmpty.ParseIn(nameof(Log4NetAppenderConfig.ConversionPattern)));
            });

            When(model => model.RollingStyle == RollingStyles.Date, () =>
            {
                RuleFor(model => model.DatePattern)
                    .NotEmpty().WithMessage(AppMessages.Validation.FieldCannotBeNullOrEmpty.ParseIn(nameof(Log4NetAppenderConfig.DatePattern)));
            });
        }
    }
}
