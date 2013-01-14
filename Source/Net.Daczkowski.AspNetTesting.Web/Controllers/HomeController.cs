namespace Net.Daczkowski.AspNetTesting.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Net.Daczkowski.AspNetTesting.Logic;
    using Net.Daczkowski.AspNetTesting.Read;
    using Net.Daczkowski.AspNetTesting.Web.Models;

    public class HomeController : Controller
    {
        private readonly ProductQueries productQueries = new ProductQueries();

        protected Cart Cart
        {
            get
            {
                if (this.Session["Cart"] == null)
                {
                    this.Session["Cart"] = new Cart();
                }

                return this.Session["Cart"] as Cart;
            }
        }
        
        public ActionResult Index()
        {
            var messages = new List<string>();
            this.ViewBag.PriceChangeMessage = messages;
          
            var summaries = this.productQueries.GetSummaryForAllProducts();
            this.Cart.CreateReceipt(id => summaries.First(summary => summary.Id == id), messages.Add);
            var receipt = this.Cart.LastReceipt;
            var model = new LandingPageViewModel(summaries, receipt);

            if (this.Session["AddToCart.Message"] != null)
            {
                this.ViewBag.AddedToCart = true;
                this.ViewBag.AddToCartMessage = this.Session["AddToCart.Message"];
                this.Session["AddToCart.Message"] = null;
            }

            return this.View(model);
        }

        public ActionResult AddToCart(int id)
        {
            this.Cart.AddProduct(id, 1);
            this.Session["AddToCart.Message"] = "Item " + id + " has been added";
            return this.RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            this.Cart.RemoveProduct(id);
            this.Session["AddToCart.Message"] = "Item " + id + " has been deleted";
            return this.RedirectToAction("Index");
        }
    }
}
