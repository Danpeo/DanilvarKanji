using DanilvarKanji.Application.Reviews.Queries;
using FluentValidation;

namespace DanilvarKanji.Application.Reviews.Validators;

public class GetReviewSessionValidator : AbstractValidator<GetReviewSessionQuery>
{
    public GetReviewSessionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();

        RuleFor(x => x.AppUser).NotEmpty().NotNull();
    }
}