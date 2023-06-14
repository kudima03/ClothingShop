using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Specifications.CustomerInfo;

public class CustomerInfoWithUser : Specification<Entities.CustomerInfo, Entities.CustomerInfo>
{
    public CustomerInfoWithUser(Expression<Func<Entities.CustomerInfo, bool>>? predicate = null)
        : base(x => x,
            predicate,
            include: x => x.Include(c => c.User))
    {
    }
}