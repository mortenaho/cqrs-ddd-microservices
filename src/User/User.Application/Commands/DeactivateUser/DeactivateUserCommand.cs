using FluentValidation;
using MediatR;
using User.Application.Interfaces;

namespace User.Application.Commands.DeactivateUser;

public sealed record DeactivateUserCommand(Guid UserId) : IRequest;

public sealed class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
{
    public DeactivateUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

public sealed class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
{
    private readonly IUserRepository _repository;

    public DeactivateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException($"User with id '{request.UserId}' was not found.");

        user.Deactivate();
        await _repository.UpdateAsync(user, cancellationToken);
    }
}
