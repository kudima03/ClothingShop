using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.CustomerInfo;
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
        CustomerInfo? customer = await _customersInfoRepository
                                       .ApplySpecification(new CustomerInfoWithUser(customerInfo =>
                                                               customerInfo.Id == request.Id))
                                       .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
        {
            throw new EntityNotFoundException($"{nameof(CustomerInfo)} with id:{request.Id} doesn't exist.");
        }

        return customer;
    }
}