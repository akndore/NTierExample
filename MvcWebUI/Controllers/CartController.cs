using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Helpers;
using MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        ICartService       _cartService;
        ICartSessionHelper _cartSessionHelper;
        IProductService    _productService;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper,
        IProductService productService)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
        }
        public IActionResult AddToCart(int Id)
        {
            Product product = _productService.GetById(Id);

            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.AddToCart(cart,product);

            _cartSessionHelper.SetCart("cart",cart);


            return Redirect("/Product/Index");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            

          //  Product product = _productService.GetById(productId);

            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.RemoveFromCart(cart, Id);

            _cartSessionHelper.SetCart("cart", cart);


            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var model = new CartVM
            {
                Cart = _cartSessionHelper.GetCart("cart")
            };

            return View(model);
        
        }

    }
}
