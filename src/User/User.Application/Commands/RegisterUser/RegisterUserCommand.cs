using FluentValidation;
using MediatR;
using User.Application.Interfaces;
using User.Domain.Aggregates;
using User.Domain.ValueObjects;

namespace User.Application.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string FullName) : IRequest<Guid>;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
    }
}

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public RegisterUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        if (await _repository.EmailExistsAsync(email, cancellationToken))
        {
            throw new InvalidOperationException($"Email '{email.Value}' is already registered.");
        }

        var user = UserAccount.Register(email, request.FullName);
        await _repository.AddAsync(user, cancellationToken);
        return user.Id;
    }
}
