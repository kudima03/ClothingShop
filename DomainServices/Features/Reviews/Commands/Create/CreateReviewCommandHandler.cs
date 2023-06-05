using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Reviews.Commands.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Review>
{
    private readonly IRepository<Review> _repository;

    public CreateReviewCommandHandler(IRepository<Review> repository)
    {
        _repository = repository;
    }

    public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Review? review = await _repository.InsertAsync(request.Review, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return review;
        }
        catch (DbUpdateException)
        {
            throw new OperationFailureException($"Unable to perform create {nameof(Review)} operation. Check input.");
        }
    }
}