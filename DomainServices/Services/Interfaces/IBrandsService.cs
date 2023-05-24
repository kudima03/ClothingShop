using ApplicationCore.Entities;

namespace DomainServices.Services.Interfaces;

public interface IBrandsService
{
    Task<IList<Brand>> GetAllBrandsAsync();
    Task<Brand?> GetBrandByIdAsync(long id);
    Task<Brand> CreateBrandAsync(Brand brand);
    Task UpdateBrandAsync(Brand brand);
    Task DeleteBrandAsync(Brand brand);
    Task<decimal> GetAverageCostOfProductsAsync(long brandId);
}