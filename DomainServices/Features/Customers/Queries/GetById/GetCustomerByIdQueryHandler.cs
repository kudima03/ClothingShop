using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Customers.Queries.GetById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerInfo>
{
    private readonly IReadOnlyRepository<CustomerInfo> _customersInfoRepository;

    public GetCustomerByIdQueryHandler(IReadOnlyRepository<CustomerInfo> customersInfoRepository)
    {
        _customersInfoRepository = customersInfoRepository;
    }

    public async Task<CustomerInfo> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _customersInfoRepository.ApplySpecification(request.Specification)
                   .FirstOrDefaultAsync(cancellationToken) ??
               throw new EntityNotFoundException($"{nameof(CustomerInfo)} with id:{request.Id} doesn't exist.");
    }
}