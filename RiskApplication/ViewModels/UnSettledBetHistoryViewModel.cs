namespace RiskApplication.ViewModels
{
    public class UnSettledBetHistoryViewModel
    {
        public int Customer { get; set; }

        public int Event { get; set; }

        public int Participant { get; set; }

        public int Stake { get; set; }

        public int ToWin { get; set; }

        public decimal ToWinPercentage
        {
            get { return (ToWin / Stake) * 100; }
        }
    }
}