using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfaces;
using API.Dtos;
using AutoMapper;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private IGenericReposotory<Product> _productRepo { get; }
        private IGenericReposotory<ProductType> _typeRepo { get; }
        private IGenericReposotory<ProductBrand> _brandRepo { get; }
        private IMapper _mapper { get; }

        public ProductsController(IGenericReposotory<Product> productRepo,
                                     IGenericReposotory<ProductType> typeRepo,
                                     IGenericReposotory<ProductBrand> brandRepo,
                                     IMapper mapper)
        {
            _productRepo = productRepo;
            _typeRepo = typeRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams prodParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(prodParams);
            var countSpec = new ProductsWithTypesAndBrandsSpecificationCounts(prodParams);

            var products = await _productRepo.ListAsync(spec);
            var counts = await _productRepo.CountAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            var r = new Pagination<ProductToReturnDto>(prodParams.PageIndex,prodParams.PageSize,counts,data);
            return  Ok(r);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _brandRepo.ListAllAsync());
        }

        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            return await _brandRepo.GetByIdAsync(id);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _typeRepo.ListAllAsync());
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            return await _typeRepo.GetByIdAsync(id);
        }
    }
}