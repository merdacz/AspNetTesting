namespace Net.Daczkowski.AspNetTesting.Web.Controllers
{
    using System.Web.Mvc;

    using Net.Daczkowski.AspNetTesting.Read;
    using Net.Daczkowski.AspNetTesting.Web.Models;

    public class AdminController : Controller
    {
        private readonly ProductQueries productQueries = new ProductQueries();

        public ActionResult Index()
        {
            var summaries = this.productQueries.GetSummaryForAllProducts();
            var model = new AdminPageViewModel(summaries);
            return this.View(model);
        }

        public ActionResult ChangePrice(int id, decimal newPrice)
        {
            ProductQueries.Summaries[id].Price = newPrice;
            return this.RedirectToAction("Index");
        }
    }
}
