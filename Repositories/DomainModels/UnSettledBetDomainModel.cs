using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;

namespace Repositories.DomainModels
{
    public class UnSettledBetDomainModel
    {
        [CsvColumn(Name = "Customer")]
        public int Customer { get; set; }

        [CsvColumn(Name = "Event")]
        public int Event { get; set; }

        [CsvColumn(Name = "Participant")]
        public int Participant { get; set; }

        [CsvColumn(Name = "Stake")]
        public int Stake { get; set; }

        [CsvColumn(Name = "To Win")]
        public int ToWin { get; set; }
    }
}