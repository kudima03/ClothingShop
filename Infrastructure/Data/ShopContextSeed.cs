using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ShopContextSeed
{
    private static List<Brand> _brands;

    private static List<Section> _sections;

    private static List<Category> _categories;

    private static List<Subcategory> _subcategories;

    private static List<Product> _products;

    private static List<ProductColor> _productColors;

    private static List<Review> _reviews;

    private static List<OrderStatus> _orderStatuses;

    static ShopContextSeed()
    {
        InitSections();
        InitCategories();
        InitSubcategories();
        InitBrands();
        InitProductColors();
        InitProducts();
        InitReviews();
        InitOrderStatuses();
    }

    private static void InitSections()
    {
        _sections = new List<Section>()
        {
            new Section() { Name = "Men", },
            new Section() { Name = "Women" },
            new Section() { Name = "Children" }
        };
    }

    private static void InitCategories()
    {
        _categories = new List<Category>()
        {
            new Category()
            {
                Name = "Outerwear",
                Sections = new List<Section>()
                {
                    _sections.Single(x => x.Name == "Men"),
                    _sections.Single(x => x.Name == "Women"),
                    _sections.Single(x => x.Name == "Children"),
                }
            },
            new Category()
            {
                Name = "Sportswear",
                Sections = new List<Section>()
                {
                    _sections.Single(x => x.Name == "Men"),
                    _sections.Single(x => x.Name == "Women"),
                    _sections.Single(x => x.Name == "Children"),
                }
            },
            new Category()
            {
                Name = "Swimwear",
                Sections = new List<Section>()
                {
                    _sections.Single(x => x.Name == "Men"),
                    _sections.Single(x => x.Name == "Women"),
                    _sections.Single(x => x.Name == "Children"),
                }
            }
        };
    }

    private static void InitSubcategories()
    {
        _subcategories = new List<Subcategory>()
        {
            new Subcategory()
            {
                Name = "Jackets",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Outerwear"), }
            },
            new Subcategory()
            {
                Name = "Jerseys",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Outerwear"), }
            },
            new Subcategory()
            {
                Name = "Coats",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Outerwear"), }
            },
            new Subcategory()
            {
                Name = "Jumpers",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Outerwear"), }
            },
            new Subcategory()
            {
                Name = "Polo",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Outerwear"), }
            },
            new Subcategory()
            {
                Name = "Joggers",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Sportswear"), }
            },
            new Subcategory()
            {
                Name = "Ski suites",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Sportswear"), }
            },
            new Subcategory()
            {
                Name = "Swimsuites",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Swimwear"), }
            },
            new Subcategory()
            {
                Name = "Flippers",
                Categories = new List<Category>() { _categories.Single(x => x.Name == "Swimwear"), }
            },
        };
    }

    private static void InitBrands()
    {
        _brands = new List<Brand>()
        {
            new Brand() { Name = "H&M" },
            new Brand() { Name = "Adidas" },
            new Brand() { Name = "Nike" },
            new Brand() { Name = "Zara" },
            new Brand() { Name = "Pull&Bear" },
            new Brand() { Name = "Bershka" },
            new Brand() { Name = "Armani" },
        };
    }

    private static void InitProductColors()
    {
        _productColors = new List<ProductColor>()
        {
            new ProductColor()
            {
                ColorHex = "#1A1B24",
                ImagesInfos = new List<ImageInfo>()
                {
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1kOj0KG9O4QvxVq-TdLIlfHDUTXlmVB6c"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1Lwea5dpel5EVhwV-_LRXfD9lSt8opkbW"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=13z_pBZHdjUQQhPSkUeOnAknRIENQwI2b"
                    }
                }
            },
            new ProductColor()
            {
                ColorHex = "#1F2029",
                ImagesInfos = new List<ImageInfo>()
                {
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1SwvTDmWKnUnc-CCGXza4iQIPUSfXq6-m"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1qjUQhFaG73HmnRg6W4t8hk0wevbQCHut"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=14p7YGdnd59VhrXoJCTUd7ObGv-2gcuxW"
                    }
                }
            },
            new ProductColor()
            {
                ColorHex = "#17181F",
                ImagesInfos = new List<ImageInfo>()
                {
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1oHBtL9ocZUTAdq_f60pf9ql0WaK6H6Lc"

                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1SydnrR511t3XTC5LUld_4hqOqhM7INdr"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1VcDJul5zWVk_LK6yzfg6J8oAyQCQLcoN"
                    }
                }
            },
            new ProductColor()
            {
                ColorHex = "#EBEAEF",
                ImagesInfos = new List<ImageInfo>()
                {
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1RvsxR8lWGPbY-BF4wzvDw9ldGhXeTZSr"

                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1-A6inF7Awblbod7VcILOEyL1HvLJehfK"
                    },
                    new ImageInfo()
                    {
                        Url =
                            "https://drive.google.com/uc?export=view&id=1d1yuijG-MST7OZybzUDIyJ0EY58vTJUz"
                    }
                }
            },
        };
    }

    private static void InitProducts()
    {
        _products = new List<Product>()
        {
            new Product()
            {
                Name = "Jersey polo shirt",
                Brand = _brands.Single(x => x.Name == "Armani"),
                Subcategory = _subcategories.Single(x => x.Name == "Jerseys"),
                ProductOptions = new List<ProductOption>()
                {
                    new ProductOption()
                    {
                        Price = 310,
                        Quantity = 254,
                        Size = "S",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1A1B24")
                    },
                    new ProductOption()
                    {
                        Price = 330,
                        Quantity = 128,
                        Size = "M",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1A1B24")
                    },
                    new ProductOption()
                    {
                        Price = 340,
                        Quantity = 83,
                        Size = "L",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1A1B24")
                    }
                }
            },
            new Product()
            {
                Name = "Tencel-blend jersey shirt",
                Brand = _brands.Single(x => x.Name == "Armani"),
                Subcategory = _subcategories.Single(x => x.Name == "Jerseys"),
                ProductOptions = new List<ProductOption>()
                {
                    new ProductOption()
                    {
                        Price = 450,
                        Quantity = 134,
                        Size = "S",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1F2029")
                    },
                    new ProductOption()
                    {
                        Price = 460,
                        Quantity = 28,
                        Size = "M",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1F2029")
                    },
                    new ProductOption()
                    {
                        Price = 470,
                        Quantity = 93,
                        Size = "L",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#1F2029")
                    }
                }
            },
            new Product()
            {
                Name = "Stretch polo shirt",
                Brand = _brands.Single(x => x.Name == "Armani"),
                Subcategory = _subcategories.Single(x => x.Name == "Polo"),
                ProductOptions = new List<ProductOption>()
                {
                    new ProductOption()
                    {
                        Price = 314,
                        Quantity = 59,
                        Size = "S",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#EBEAEF")
                    },
                    new ProductOption()
                    {
                        Price = 329,
                        Quantity = 75,
                        Size = "M",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#EBEAEF")
                    },
                    new ProductOption()
                    {
                        Price = 329,
                        Quantity = 76,
                        Size = "L",
                        ProductColor = _productColors.Single(x => x.ColorHex == "#EBEAEF")
                    }
                }
            },
        };
    }

    private static void InitReviews()
    {
        _reviews = new List<Review>()
        {
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Jersey polo shirt"),
                Comment = "Nice jersey",
                DateTime = DateTime.UtcNow,
                Rate = 5
            },
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Jersey polo shirt"),
                Comment = "I liked it!",
                DateTime = DateTime.UtcNow,
                Rate = 5
            },
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Jersey polo shirt"),
                Comment = "Very good.",
                DateTime = DateTime.UtcNow,
                Rate = 5
            },
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Tencel-blend jersey shirt"),
                Comment = "Good material.",
                DateTime = DateTime.UtcNow,
                Rate = 4
            },
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Tencel-blend jersey shirt"),
                Comment = "Amazing quality.",
                DateTime = DateTime.UtcNow,
                Rate = 4
            },
            new Review()
            {
                UserId = 1,
                Product = _products.Single(x => x.Name == "Stretch polo shirt"),
                Comment = "Very lightweight product.",
                DateTime = DateTime.UtcNow,
                Rate = 3
            },
        };
    }

    private static void InitOrderStatuses()
    {
        _orderStatuses = new List<OrderStatus>()
        {
            new OrderStatus() { Name = OrderStatusName.InReview, },
            new OrderStatus() { Name = OrderStatusName.InDelivery, },
            new OrderStatus() { Name = OrderStatusName.Completed, },
            new OrderStatus() { Name = OrderStatusName.Cancelled, },
        };
    }

    public static async Task SeedAsync(ShopContext shopContext)
    {
        if (!await shopContext.Sections.AnyAsync())
        {
            await shopContext.Sections.AddRangeAsync(_sections);
        }

        if (!await shopContext.Categories.AnyAsync())
        {
            await shopContext.Categories.AddRangeAsync(_categories);
        }

        if (!await shopContext.Subcategories.AnyAsync())
        {
            await shopContext.Subcategories.AddRangeAsync(_subcategories);
        }

        if (!await shopContext.Brands.AnyAsync())
        {
            await shopContext.Brands.AddRangeAsync(_brands);
        }

        if (!await shopContext.ProductsColors.AnyAsync())
        {
            await shopContext.ProductsColors.AddRangeAsync(_productColors);
        }

        if (!await shopContext.Products.AnyAsync())
        {
            await shopContext.Products.AddRangeAsync(_products);
        }

        if (!await shopContext.Reviews.AnyAsync())
        {
            await shopContext.Reviews.AddRangeAsync(_reviews);
        }

        if (!await shopContext.OrderStatuses.AnyAsync())
        {
            await shopContext.OrderStatuses.AddRangeAsync(_orderStatuses);
        }

        await shopContext.SaveChangesAsync();
    }
}