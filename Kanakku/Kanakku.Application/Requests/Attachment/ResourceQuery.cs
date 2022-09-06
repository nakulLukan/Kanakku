using Kanakku.Application.Contracts.Essential;
using Kanakku.Application.Contracts.Storage;
using Kanakku.Application.Models.Attachment;
using Kanakku.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kanakku.Application.Requests.Attachment;

public class ResourceQuery : IRequest<BinaryResourceDto>
{
    public int ResourceId { get; set; }
}


public class ResourceQueryHandler : IRequestHandler<ResourceQuery, BinaryResourceDto>
{
    private IAppDbContext _appDbContext;
    private readonly IPermissionService permission;

    public ResourceQueryHandler(IAppDbContext appDbContext, IPermissionService permission)
    {
        _appDbContext = appDbContext;
        this.permission = permission;
    }

    public async Task<BinaryResourceDto> Handle(ResourceQuery request, CancellationToken cancellationToken)
    {
        var resource = await _appDbContext.BinaryResources.FirstAsync(x => x.Id == request.ResourceId, cancellationToken);

        await permission.GetStoragePermission();
        var basePath = String.Format(DirectoryConstant.BINARY_RESOURCE_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        string path = $"{basePath}/{resource.Id}.jpeg";
        await File.WriteAllBytesAsync(path, resource.Data, cancellationToken);
        return new BinaryResourceDto
        {
            Id = request.ResourceId,
            LocalPath = path,
            Base64String = Convert.ToBase64String(resource.Data),
            FileName = resource.FileName,
            FileFullName = resource.FileFullName,
            Extension = resource.Extension 
        };
    }
}
