using MediatR;

namespace Kanakku.Application.Requests;

public class SampleRequest : IRequest<long>
{
}

public class SampleRequestHandler : IRequestHandler<SampleRequest, long>
{
    public async Task<long> Handle(SampleRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(1);
    }
}
