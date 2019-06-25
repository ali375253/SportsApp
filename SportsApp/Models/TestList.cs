using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public class TestList
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Date { get; set; }
        public int NumberOfParticipants { get; set; }
        public string TestType { get; set; }
    }
}
