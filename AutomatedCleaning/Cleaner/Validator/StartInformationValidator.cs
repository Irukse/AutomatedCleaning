using FluentValidation;

namespace AutomatedCleaning.Cleaner.Validator;

public class StartInformationValidator : AbstractValidator<StartInformation>
{
    public StartInformationValidator()
    {
        // RuleFor(x => x.Map)
        //   .Must(x => x.Equals("S") || x.Equals("C") || x.Equals(null)).WithMessage("No more than 10 orders are allowed");
        //
        RuleFor(startInformation => startInformation.Battery)
            .NotEmpty()
            .GreaterThan(0);
        RuleForEach(startInformation => startInformation.Commands)
            .NotEmpty()
            .Must(command => command.Contains("TL") || command.Contains("TR") || command.Contains("A") ||
                             command.Contains("B") || command.Contains("C"))
            .WithMessage("The value must be either 'TL' or 'TR' or 'A' or 'B' or 'C' only.");
        RuleFor(startInformation => startInformation.Start.Facing)
            .NotEmpty()
            .Must(facing => facing.Equals("N") || facing.Equals("S") || facing.Equals("W") || facing.Equals("E") ||
                            facing.Equals("C"))
            .WithMessage("The Face must match 'N' or 'S' or 'W' or 'E' only.");
        RuleFor(startInformation => startInformation.Start.X)
            .NotNull()
            .WithMessage("Value can not be null");
        RuleFor(startInformation => startInformation.Start.Y)
            .NotNull()
            .WithMessage("Value can not be null");
    }
}