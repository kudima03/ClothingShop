using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using DomainServices.Features.Templates.Queries.CollectionQueries;

namespace DomainServices.Features.ImagesInfos.Queries;

public class GetAllImagesInfosQuery : EntityCollectionQuery<ImageInfo>
{
    public GetAllImagesInfosQuery()
        : base(new Specification<ImageInfo, ImageInfo>(x => x))
    {
    }
}