using Api.Models;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for Product requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="mapper"></param>
        /// 
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Returns a list of Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
           return Ok(_productRepository.ListAll());
        }
        /// <summary>
        /// Returns a product that matches the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(Guid id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        /// <summary>
        /// Create the new product
        /// </summary>
        /// <param name="request">Product Request Model</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(CreateProductRequest request)
        {
            var product = _productRepository.Add(_mapper.Map<Product>(request));
            return Ok(product);
        }
        /// <summary>
        /// Modifies the existing Product that matches the provided id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="request">Product Request model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateProductRequest request)
        {
            if (id != request.Id) return BadRequest();
            Product product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            product =   _productRepository.Update(_mapper.Map<Product>(request), id);

            return Ok(product);
        }
        /// <summary>
        /// Removes the existing product that matches the provided id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Product product = _productRepository.GetById(id);
            if (product == null) return NotFound();

            _productRepository.Delete(product);

            return Ok(product);
        }

    }
}
