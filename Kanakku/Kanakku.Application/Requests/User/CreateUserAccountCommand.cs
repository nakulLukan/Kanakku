using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Shared.Utilities;
using MediatR;

namespace Kanakku.Application.Requests.User;

public class CreateUserAccountCommand : SignupDto, IRequest<AppUserMinDto>
{
}

public class CreateUserAccountCommandHandler : IRequestHandler<CreateUserAccountCommand, AppUserMinDto>
{
    private readonly IAppDbContext _appDbContext;

    public CreateUserAccountCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<AppUserMinDto> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
    {
        request.Username = request.Username.Trim();
        request.Password = request.Password.Trim();
        if (request.Password != request.ConfirmPassword.Trim())
        {
            throw new AppException("'Password' and 'Confirm Password' should exactly match.");
        }

        return new AppUserMinDto
        {
            Username = request.Username
        };
    }
}
