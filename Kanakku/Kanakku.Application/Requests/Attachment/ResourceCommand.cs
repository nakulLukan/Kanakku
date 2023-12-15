using FluentValidation;
using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Attachment;
using Kanakku.Domain.Attachment;
using Kanakku.Shared;
using MediatR;

namespace Kanakku.Application.Requests.Attachment;

public class ResourceCommand : IRequest<BinaryResourceDto>
{
    public int Id { get; set; }
    public Stream DataStream { get; set; }
    public string FileFullName { get; set; }
}

public class ResourceCommandHandler : IRequestHandler<ResourceCommand, BinaryResourceDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly ISessionContext sessionContext;

    public ResourceCommandHandler(IAppDbContext appDbContext, ISessionContext sessionContext)
    {
        this.appDbContext = appDbContext;
        this.sessionContext = sessionContext;
    }

    public async Task<BinaryResourceDto> Handle(ResourceCommand request, CancellationToken cancellationToken)
    {
        MemoryStream memStream = new MemoryStream();
        await request.DataStream.CopyToAsync(memStream);
        var bytearray = memStream.ToArray();
        var userId = await sessionContext.GetUserId();
        BinaryResource resource = new BinaryResource
        {
            Id = request.Id,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow,
            CreatedBy = userId,
            ModifiedBy = userId,
            Data = bytearray,
            FileFullName = request.FileFullName,
            Extension = !string.IsNullOrEmpty(request.FileFullName) ? Path.GetExtension(request.FileFullName) : string.Empty,
            FileName = !string.IsNullOrEmpty(request.FileFullName) ? Path.GetFileNameWithoutExtension(request.FileFullName) : string.Empty
        };

        if (resource.Id > 0)
        {
            appDbContext.BinaryResources.Update(resource);
        }
        else
        {
            appDbContext.BinaryResources.Add(resource);
        }

        await appDbContext.SaveAsync(cancellationToken);
        var basePath = String.Format(DirectoryConstant.BINARY_RESOURCE_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        string path = $"{basePath}/{resource.Id}.jpeg";
        await File.WriteAllBytesAsync(path, bytearray, cancellationToken);

        return new BinaryResourceDto
        {
            Id = resource.Id,
            LocalPath = path,
            Base64String = Convert.ToBase64String(bytearray)
        };
    }
}

public class ResourceCommandValidator : AbstractValidator<ResourceCommand>
{
    public ResourceCommandValidator()
    {
        RuleFor(x => x.DataStream).Must(x => x.Length < Constant.RESOURCE_MAX_LENGTH)
            .WithMessage("File size cannot be greater than 3 mega bytes.");
    }
}
