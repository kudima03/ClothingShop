using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Customers.Commands.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly IRepository<CustomerInfo> _customersInfoRepository;

    public DeleteCustomerCommandHandler(IRepository<CustomerInfo> customersInfoRepository)
    {
        _customersInfoRepository = customersInfoRepository;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        CustomerInfo? customerInfo = await _customersInfoRepository.GetFirstOrDefaultAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (customerInfo is not null)
        {
            _customersInfoRepository.Delete(customerInfo);
            await _customersInfoRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}