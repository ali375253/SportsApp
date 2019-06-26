using System;
using SportsApp.Models;

namespace SportsApp.Test
{
    internal class DataDBInitializer
    {
        public DataDBInitializer()
        {
        }

        public void Seed(SportsAppContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Test.AddRange(
                new Models.Test() { Date = "100419", TestType = "Cooper Test" },
                new Models.Test() { Date = "150419", TestType = "100 Meter Sprint" },
                new Models.Test() { Date = "190419", TestType = "Cooper Test" }
            );

            context.TestDetail.AddRange(
                new TestDetail() { AthleteName = "Queen Jacobi", Distance =2850, Rating = "Good", TestId = 2},
                new TestDetail() { AthleteName = "Camille Grantham", Distance = 2950, Rating = "Good", TestId = 2 },
                new TestDetail() { AthleteName = "Delicia Ledonne", Distance = 3700, Rating = "Very good", TestId = 1 },
                new TestDetail() { AthleteName = "Rosario Reuben", Distance = 1850, Rating = "Average", TestId = 3 },
                new TestDetail() { AthleteName = "Lula Uhlman", Distance = 700, Rating = "Below average", TestId = 3 },
                new TestDetail() { AthleteName = "Marc  Voth", Distance = 2150, Rating = "Good", TestId = 2 }
            );
            context.SaveChanges();
        }

    }
}