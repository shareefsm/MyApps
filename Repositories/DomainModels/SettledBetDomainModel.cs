using LINQtoCSV;

namespace Repositories.DomainModels
{
    public class SettledBetDomainModel
    {
        [CsvColumn(Name = "Customer")]
        public int Customer { get; set; }

        [CsvColumn(Name = "Event")]
        public int Event { get; set; }

        [CsvColumn(Name = "Participant")]
        public int Participant { get; set; }

        [CsvColumn(Name = "Stake")]
        public int Stake { get; set; }

        [CsvColumn(Name = "Win")]
        public int Win { get; set; }
    }
}