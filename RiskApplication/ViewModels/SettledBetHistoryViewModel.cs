namespace RiskApplication.ViewModels
{
    public class SettledBetHistoryViewModel
    {
        public int Customer { get; set; }

        public int Event { get; set; }

        public int Participant { get; set; }

        public int Stake { get; set; }

        public int Win { get; set; }

        public decimal WinPercentage
        {
            get { return (Win/Stake)*100; }
        }
    }
}