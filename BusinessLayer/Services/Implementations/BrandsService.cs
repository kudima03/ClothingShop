using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using BusinessServices.Services.Interfaces;

namespace BusinessServices.Services.Implementations;

public class BrandsService : IBrandsService
{
    private readonly IReadOnlyRepository<Brand> _brandReadOnlyRepository;
    private readonly IRepository<Brand> _brandsRepository;

    public BrandsService(IRepository<Brand> brandsRepository, IReadOnlyRepository<Brand> brandReadOnlyRepository)
    {
        _brandsRepository = brandsRepository;
        _brandReadOnlyRepository = brandReadOnlyRepository;
    }

    public async Task<IList<Brand>> GetAllBrandsAsync()
    {
        return await _brandReadOnlyRepository.GetAllNonTrackingAsync();
    }

    public async Task<Brand?> GetBrandByIdAsync(long id)
    {
        return await _brandReadOnlyRepository.GetFirstOrDefaultNonTrackingAsync(predicate: brand => brand.Id == id);
    }

    public async Task<Brand> CreateBrandAsync(Brand brand)
    {
        Brand entity = (await _brandsRepository.InsertAsync(brand)).Entity;
        await _brandsRepository.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateBrandAsync(Brand brand)
    {
        _brandsRepository.Update(brand);
        await _brandsRepository.SaveChangesAsync();
    }

    public async Task DeleteBrandAsync(Brand brand)
    {
        _brandsRepository.Delete(brand);
        await _brandsRepository.SaveChangesAsync();
    }

    public async Task<decimal> GetAverageCostOfProductsAsync(long brandId)
    {
        return await _brandsRepository.AverageAsync(
            brand =>
                brand.Products.Select(product =>
                    product.ProductOptions.Select(productOption => productOption.Price).Average()).Average(),
            brand => brand.Id == brandId);
    }
}