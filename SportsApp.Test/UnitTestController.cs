using Microsoft.EntityFrameworkCore;
using SportsApp.Controllers;
using SportsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportsApp.Test
{
    public class UnitTestController
    {
        private ITestListRepository repository;
        private IUnitOfWork unitOfWork;
        public static DbContextOptions<SportsAppContext> dbContextOptions { get; }
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = SportsApp; Integrated Security = True;";

        static UnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<SportsAppContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public UnitTestController()
        {
            var context = new SportsAppContext(dbContextOptions);
            DataDBInitializer db = new DataDBInitializer();
            db.Seed(context);
            repository = new TestListRepository(context);
            unitOfWork = new UnitOfWork(context);
 
        }

        [Fact]
        public async Task Add_Valid_Test_Id()
        {
            Models.Test test = new Models.Test()
            {
                Date = "300419",
                TestType = "Cooper Test"
            };

            Models.Test savedTest = await repository.AddAsync(test);
            await unitOfWork.Commit();
            Assert.NotNull(savedTest.Id);
            Assert.Equal(true, IsValidId(savedTest.Id));

        }

        [Fact]
        public async Task Add_Valid_Test_Date()
        {
            Models.Test test = new Models.Test()
            {
                Date = "300419",
                TestType = "Cooper Test"
            };

            Models.Test savedTest = await repository.AddAsync(test);
            await unitOfWork.Commit();
            char[] date=savedTest.Date.ToCharArray();
            Assert.Equal(6, date.Length);
            Assert.Equal(true, IsValidDate(date));
            Assert.NotNull(savedTest.Date);
        }

        [Fact]
        public async Task Add_Valid_TestType()
        {
            Models.Test test = new Models.Test()
            {
                Date = "300419",
                TestType = "Cooper Test"
            };

            Models.Test savedTest = await repository.AddAsync(test);
            await unitOfWork.Commit();
            Assert.Equal(true, IsValidTestType(savedTest.TestType));
            Assert.NotNull(savedTest.TestType);
        }

        [Fact]
        public async Task Add_Valid_TestDetail_Id()
        {
            TestDetail testDetail = new TestDetail()
            {
                AthleteName = "Randy Rondon",
                Distance = 4000,
                Rating= "Very Good",
                TestId=2
            };

            TestDetail savedTestDetail = await repository.Add(testDetail);
            await unitOfWork.Commit();
            Assert.Equal(true, IsValidId(savedTestDetail.Id));
            Assert.NotNull(savedTestDetail.Id);

        }

        [Fact]
        public async Task Add_Valid_TestDetail_Distance()
        {
            TestDetail testDetail = new TestDetail()
            {
                AthleteName = "Randy Rondon",
                Distance = 4000,
                Rating = "Very Good",
                TestId = 2
            };

            TestDetail savedTestDetail = await repository.Add(testDetail);
            await unitOfWork.Commit();
            Assert.Equal(true, IsValidDistance(savedTestDetail.Distance));
            Assert.NotNull(savedTestDetail.Distance);
        }

        [Fact]
        public async Task Add_Valid_TestDetail_AthleteName()
        {
            TestDetail testDetail = new TestDetail()
            {
                AthleteName = "Randy Rondon",
                Distance = 4000,
                Rating = "Very Good",
                TestId = 2
            };

            TestDetail savedTestDetail = await repository.Add(testDetail);
            await unitOfWork.Commit();
            Assert.Equal("Randy Rondon", savedTestDetail.AthleteName);
            Assert.NotNull(savedTestDetail.AthleteName);
        }

        [Fact]
        public async Task Add_Valid_TestDetail_Rating()
        {
            TestDetail testDetail = new TestDetail()
            {
                AthleteName = "Randy Rondon",
                Distance = 4000,
                Rating = "Very Good",
                TestId = 2
            };

            TestDetail savedTestDetail = await repository.Add(testDetail);
            await unitOfWork.Commit();
            Assert.Equal(true, IsValidRating(savedTestDetail.Distance, savedTestDetail.Rating));
            Assert.NotNull(savedTestDetail.Rating);
        }

        [Fact]
        public async Task Add_Valid_TestDetail_TestId()
        {
            TestDetail testDetail = new TestDetail()
            {
                AthleteName = "Randy Rondon",
                Distance = 4000,
                Rating = "Very Good",
                TestId = 2
            };

            TestDetail savedTestDetail = await repository.Add(testDetail);
            await unitOfWork.Commit();
            Assert.Equal(true, IsValidId(savedTestDetail.TestId));
            Assert.NotNull(savedTestDetail.TestId);
        }

        [Fact]
        public async Task Delete_TestDetail_Confirmation()
        {
            var testDetailId = 1;
            var data = await repository.DeleteAthleteConfirmed(testDetailId);
            await unitOfWork.Commit();
            Assert.NotNull(data);
        }

        [Fact]
        public async Task Delete_Test_Confirmation()
        {
            var testId = 2;
            var data = await repository.DeleteAthleteConfirmed(testId);
            await unitOfWork.Commit();
            Assert.NotNull(data);
        }

        [Fact]
        public void GetTestList_Not_Null()
        {
            var data = repository.GetTestList();
            Assert.NotNull(data);
        }

        [Fact]
        public void GetTestDetails_Not_Null()
        {
            var testId = 2;
            var data = repository.GetTestDetails(testId);
            Assert.NotNull(data);
        }

        [Fact]
        public void GetTest_Not_Null()
        {
            var testId = 2;
            var data = repository.Get(testId);
            Assert.NotNull(data);
        }

        [Fact]
        public void GetTestDetail_Not_Null()
        {
            var testDetailId = 1;
            var data = repository.GetTestDetail(testDetailId);
            Assert.NotNull(data);
        }

        private bool IsValidRating(float distance, string rating)
        {
            if (distance <= 1000 && rating == "Below average")
            {
                return true;
            }
            else if (distance <= 2000 && rating == "Average")
            {
                return true;
            }
            else if (distance <= 3500 && rating == "Good")
            {
                return true;
            }
            else if (distance > 3500 && rating == "Very good")
            {
                return true;
            }
            return false;
        }

        private bool IsValidDistance(float distance)
        {
            if (distance > 0)
                return true;
            return false;
        }

        public bool IsValidId(int id)
        {
            if (id > 0)
                return true;
            return false;
        }

        public bool IsValidDate(char[] date)
        {
            string day = date[0] + "" + date[1];
            string month = date[2] + "" + date[3];
            string year = "20" + date[4] + "" + date[5];
            int d = Int32.Parse(day);
            int m = Int32.Parse(month);
            int y = Int32.Parse(year);
            if (m == 4 || m == 6 || m == 9 || m == 11)
            {
                if (d > 0 && d <= 30)
                    return true;
            }
            else if(m==2) {
                if (DateTime.IsLeapYear(y))
                {
                    if (d > 0 && d <= 29)
                        return true;
                }
                else
                {
                    if (d > 0 && d <= 28)
                        return true;
                }
            }
            else
            {
                if (d > 0 && d <= 31)
                    return true;
            }
            return false;
        }

        public bool IsValidTestType(string testType)
        {
            if (testType == "Cooper Test" || testType == "100 meter sprint")
                return true;
            return false;
        }
    }
}