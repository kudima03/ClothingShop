using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;

namespace DomainServices.Features.ImagesInfos.Queries;

public class GetImageInfoByIdQuery : SingleEntityQuery<ImageInfo>
{
    public GetImageInfoByIdQuery(long id)
        : base(new Specification<ImageInfo, ImageInfo>(
            x => x,
            x => x.Id == id))
    {
        Id = id;
    }

    public long Id { get; set; }
}