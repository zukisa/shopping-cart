using Api.Models;
using Common.EmailHelper;
using Common.Extensions;
using Core.Entities;
using Core.Interfaces.Gateways.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRepository"></param>
        /// <param name="productOrderRepository"></param>
        /// <param name="productRepository"></param>
        /// <param name="emailSender"></param>
        /// <param name="userRepository"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="configuration"></param>
        public OrderController(IOrderRepository orderRepository, IProductOrderRepository productOrderRepository,
            IProductRepository productRepository, IEmailSender emailSender, IUserRepository userRepository,
            IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _productRepository = productRepository;
            _emailSender = emailSender;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        /// <summary>
        /// Creates the new order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(CreateOrderRequest request)
        {
            var orderNo = _orderRepository.GenerateOrderNo()?.OrderNo;
            if (orderNo.IsNullOrEmptyOrWhiteSpace())
            {
                return BadRequest("Order could not be created");
            }
            Order order = new Order
            {
                UserId = request.UserId,
                Id = orderNo
            };
            _orderRepository.Add(order);
            foreach(var item in request.ProductOrders)
            {
                _productOrderRepository.Add(new ProductOrder { OrderId = orderNo, ProductId = item.ProductId, Quantity = item.Quantity });

                Product product = _productRepository.GetById(item.ProductId);
                product.StockLevel -= item.Quantity;
                _productRepository.Update(product, product.Id);
                //Set Product name and price for invoice
                item.ProductName = product.Name;
                item.Price = product.Price;
            }
            //Send email
            ProcessOrderAndSendEmail(order, request);
            return Ok(order);
        }
        private void ProcessOrderAndSendEmail(Order order, CreateOrderRequest request)
        {
            //Create an email format and send email
            User user = _userRepository.GetById(order.UserId);
            if (user == null) return;
            string Content = @"Hi<br/><br/> Thank you for shopping with us. Please see attached invoice for your items.<br/><br/><br/> Kind Regards,<br/>BET SOFTWARE";
            //Generate Invoice and append details
            Tuple<string, byte[]> tuple = Utils.DocumentHelper.GenerateOrder(order, request, user, $@"{_webHostEnvironment.WebRootPath}\templates\", _configuration);
            // Set message detalils
            Common.EmailHelper.Classes.Message message = new Common.EmailHelper.Classes.Message(new[] { user.Email }, $"Items for Order:{order.Id}", Content, attachments: new Dictionary<string, byte[]>
            {
                { tuple.Item1, tuple.Item2 }
            });
            //Send email to user
            _emailSender.SendEmail(message);
        }
    }
}
