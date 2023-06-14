using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Customers.Queries.GetAll;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerInfo>>
{
    private readonly IReadOnlyRepository<CustomerInfo> _customersRepository;

    public GetAllCustomersQueryHandler(IReadOnlyRepository<CustomerInfo> customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IEnumerable<CustomerInfo>> Handle(GetAllCustomersQuery request,
                                                        CancellationToken cancellationToken)
    {
        return await _customersRepository.GetAllAsync(cancellationToken);
    }
}