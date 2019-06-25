using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public class TestListRepository : ITestListRepository
    {
        private readonly SportsAppContext _context;

        public TestListRepository(SportsAppContext context)
        {
            _context = context;
        }

        public IEnumerable<TestDetail> GetTestDetails(int testId)
        {
            var testDetails = from d in _context.TestDetail
                              where d.TestId.Equals(testId)
                              orderby d.Distance descending
                              select d;
            //return await _context.TestDetail.FindAsync(testId);
            return testDetails;
        }

        public IEnumerable<TestList> GetTestList()
        {
            var tl = from test in _context.Test
                     join cnt in _context.TestDetail on test.Id equals cnt.TestId
                     into tmp
                     from t in tmp.DefaultIfEmpty()
                     select new TestList
                     {
                         Date = test.Date,
                         NumberOfParticipants=t!=null ? _context.TestDetail.Count(n => n.TestId == test.Id) : 0,
                         TestType=test.TestType,
                         TestId=test.Id
                     };
            tl = tl.OrderByDescending(t=>t.Date).Distinct();
            return tl;
        }

        public async Task<TestDetail> Add(TestDetail NewAthlete)
        {
            if(NewAthlete.Distance <= 1000)
            {
                NewAthlete.Rating = "Below average";
            }
            else if(NewAthlete.Distance <= 2000)
            {
                NewAthlete.Rating = "Average";
            }
            else if (NewAthlete.Distance <= 3500)
            {
                NewAthlete.Rating = "Good";
            }
            else if (NewAthlete.Distance > 3500)
            {
                NewAthlete.Rating = "Very good";
            }
            await _context.TestDetail.AddAsync(NewAthlete);
            return NewAthlete;
        }

        public async Task<Test> AddAsync(Test test)
        {
            await _context.Test.AddAsync(test);
            return test;

        }

        public async Task<Test> Get(int Id)
        {
            return await _context.Test.FindAsync(Id);
        }

        public async Task<TestDetail> GetTestDetail(int Id)
        {
            return await _context.TestDetail.FindAsync(Id);
        }

        public async Task<Test> DeleteConfirmed(int id)
        {
            _context.TestDetail.RemoveRange(_context.TestDetail.Where(x => x.TestId == id));
            var t = await _context.Test.FindAsync(id);
            _context.Test.Remove(t);
            return t;
        }

        public async Task<TestDetail> DeleteAthleteConfirmed(int id)
        {
            var t = await _context.TestDetail.FindAsync(id);
            _context.TestDetail.Remove(t);
            return t;
        }

        public TestDetail Update(TestDetail UpdatedTestDetail)
        {
            if (UpdatedTestDetail.Distance <= 1000)
            {
                UpdatedTestDetail.Rating = "Below average";
            }
            else if (UpdatedTestDetail.Distance <= 2000)
            {
                UpdatedTestDetail.Rating = "Average";
            }
            else if (UpdatedTestDetail.Distance <= 3500)
            {
                UpdatedTestDetail.Rating = "Good";
            }
            else if (UpdatedTestDetail.Distance > 3500)
            {
                UpdatedTestDetail.Rating = "Very good";
            }
            var t = _context.TestDetail.Attach(UpdatedTestDetail);
            t.State = EntityState.Modified;
            return UpdatedTestDetail;
        }
    }
}
