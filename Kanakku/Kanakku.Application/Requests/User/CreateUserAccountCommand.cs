using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.User;
using Kanakku.Domain.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanakku.Application.Requests.User;

public class CreateUserAccountCommand : IRequest<AppUserMinDto>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
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
        if(request.Password != request.ConfirmPassword)
        {
            throw new Exception("Password and Confirm Password should match");
        }

        var isUsernameExists = await _appDbContext.AppUsers.AnyAsync(x => x.Username == request.Username, cancellationToken);
        if (isUsernameExists)
        {
            throw new Exception("User already exists");
        }

        AppUser newUser = new AppUser
        {
            Username = request.Username,
            Password = request.Password,
            IsActive = true,
            Name = String.Empty,
            Email = String.Empty,
        };

        try
        {
            _appDbContext.AppUsers.Add(newUser);
            await _appDbContext.SaveAsync(cancellationToken);
        }
        catch(Exception e)
        {

        }

        return new AppUserMinDto
        {
            Id = newUser.Id,
            Username = newUser.Name
        };
    }
}
