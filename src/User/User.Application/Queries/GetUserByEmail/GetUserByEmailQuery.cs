using FluentValidation;
using MediatR;
using User.Application.DTOs;
using User.Application.Interfaces;
using User.Domain.ValueObjects;

namespace User.Application.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;

public sealed class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}

public sealed class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto?>
{
    private readonly IUserRepository _repository;

    public GetUserByEmailQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var user = await _repository.GetByEmailAsync(email, cancellationToken);
        return user?.ToDto();
    }
}
