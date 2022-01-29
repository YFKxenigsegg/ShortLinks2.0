using FluentValidation.Results;

namespace ShortLinks.Kernel.Exceptions;
public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("One or more validation failures occured") =>
        Errors = new Dictionary<string, string[]>();

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        var failuresGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failuresGroup in failuresGroups)
        {
            var propertyName = failuresGroup.Key;
            var propertyFailures = failuresGroup.ToArray();

            Errors.Add(propertyName, propertyFailures);
        }
    }
}
