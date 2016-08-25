using System.Collections.Generic;
using System.Web.Mvc;
using RiskApplication.Enums;
using RiskApplication.Managers.Interfaces;
using RiskApplication.ViewModels;
using AppConstants = RiskApplication.Constants.Constants;

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
            return View("Index");
        }

        public JsonResult GetSettledBetHistoryForUnusualRateWin()
        {
            IList<SettledBetHistoryViewModel> settledBetHistory = _riskManager.GetSettledBetHistoryForUnusualRateWin(AppConstants.UnusualRate);

            return Json(settledBetHistory);
        }

        public JsonResult GetUnSettledRiskBets(RiskTypes riskType)
        {
            IList<UnSettledBetHistoryViewModel> unSettledRiskBets = _riskManager.GetUnSettledRiskBets(riskType);

            return Json(unSettledRiskBets);
        }
    }
}