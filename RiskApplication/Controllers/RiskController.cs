using System.Web.Mvc;
using RiskApplication.Managers.Interfaces;
using RiskApplication.ViewModels;

namespace RiskApplication.Controllers
{
    public class RiskController : Controller
    {
        private readonly IRiskManager _riskManager;
        public RiskController(IRiskManager riskManager)
        {
            _riskManager = riskManager;
        }

        // GET: Risk
        public ActionResult Index()
        {
            BetHistoryViewModel model = _riskManager.GetBetStake();

            return View(model);
        }
    }
}