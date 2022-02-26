using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //IProductDal kullanıyoruz, bu sayede ne InMemory ne EntityFramework vb gibi siniflara bagimli olmuyoruz
        //IProductDal zaten hepsinin referansini tutuyor. O yüzden IProductDal kullanıyoruz. Başka bir zaman değiştirmek
        //istersek Business'ta değişiklik yapmamıza gerek kalmıyor
        IProductDal _productDal;


        // BIR ENTITY MANAGER KENDISI HARIC BASKA BİR DAL(DATA ACCESS LAYER)'i ENJEKTE EDEMEZ
        // o yüzden bunu kaldırıyoruz
        // ctor injection'i da kaldıryoruz.
        //ICategoryDal _categoryDal;

        //DAL yerine service'leri enjekte etmeliyiz. Mikro servis mimarisi buna denmektedir.
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //Claim
        [SecuredOperation("product.add,editor")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
               CheckIfProductNameExist(product.ProductName),
               CheckIfCategoryLimitExceeded()); 
            
            return new ErrorResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == categoryId));

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice<=max));

        }

        public IDataResult<List<ProductDetailDto>>  GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName);
            if(result.Count != 0)
            {
                return new ErrorResult(Messages.ProductNameExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count >= 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();

        }

    }
}
