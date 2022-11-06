using AutoMapper;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Models;
using System.Collections.Generic;

namespace IndusShowroomApi.Data.Services
{
    public class SetupService : ISetupService
    {
        private readonly IMapper mapper;
        private readonly IProductRepo productRepo;
        private readonly IProduct_CategoryRepo product_CategoryRepo;
        private readonly IProduct_BrandRepo product_BrandRepo;

        public SetupService(IMapper mapper, IProductRepo productRepo, IProduct_CategoryRepo product_CategoryRepo, IProduct_BrandRepo product_BrandRepo)
        {
            this.mapper = mapper;
            this.productRepo = productRepo;
            this.product_CategoryRepo = product_CategoryRepo;
            this.product_BrandRepo = product_BrandRepo;

        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            foreach (var item in productRepo.GetProducts())
            {
                if (!item.IsDelete)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public Product GetProduct(int product_id)
        {
            return productRepo.GetProduct(product_id);
        }

        public bool InsertProduct(Product product)
        {
            productRepo.InsertProduct(product);
            if (productRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product product)
        {
            productRepo.UpdateProduct(product);
            if (productRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool DeleteProduct(Product product)
        {
            product.IsDelete = true;
            productRepo.UpdateProduct(product);
            if (productRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public List<Product_Category> GetProduct_Categories()
        {
            List<Product_Category> product_Categories = new List<Product_Category>();
            foreach (var item in product_CategoryRepo.GetProduct_Categories())
            {
                if (!item.IsDelete)
                {
                    product_Categories.Add(item);
                }
            }
            return product_Categories;
        }

        public Product_Category GetProduct_Category(int product_cat_id)
        {
            return product_CategoryRepo.GetProduct_Category(product_cat_id);
        }

        public bool InsertProduct_Category(Product_Category product_Category)
        {
            product_CategoryRepo.InsertProduct_Category(product_Category);
            if (product_CategoryRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool UpdateProduct_Category(Product_Category product_Category)
        {
            product_CategoryRepo.UpdateProduct_Category(product_Category);
            if (product_CategoryRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool DeleteProduct_Category(Product_Category product_Category)
        {
            foreach (var item in productRepo.GetProducts())
            {
                if (!item.IsDelete)
                {
                    if (item.PRODUCT_CAT_ID == product_Category.PRODUCT_CAT_ID)
                    {
                        return false;
                    }
                }
            }

            product_Category.IsDelete = true;
            product_CategoryRepo.UpdateProduct_Category(product_Category);
            if (productRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public List<Product_Brand> GetProduct_Brands()
        {
            return product_BrandRepo.GetProduct_Brands();
        }

        public Product_Brand GetProduct_Brand(int product_cat_id)
        {
            return product_BrandRepo.GetProduct_Brand(product_cat_id);
        }

        public bool InsertProduct_Brand(Product_Brand product_Brand)
        {
            product_BrandRepo.InsertProduct_Brand(product_Brand);
            if (product_BrandRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool UpdateProduct_Brand(Product_Brand product_Brand)
        {
            product_BrandRepo.UpdateProduct_Brand(product_Brand);
            if (product_BrandRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }

        public bool DeleteProduct_Brand(Product_Brand product_Brand)
        {
            foreach (var item in productRepo.GetProducts())
            {
                if (!item.IsDelete)
                {
                    if (item.PRODUCT_BRAND_ID == product_Brand.PRODUCT_BRAND_ID)
                    {
                        return false;
                    }
                }
            }

            product_Brand.IsDelete = true;
            product_BrandRepo.UpdateProduct_Brand(product_Brand);
            if (productRepo.SaveChanges())
            {
                return true;
            }
            return false;
        }
    }
}
