using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApp.Models
{
    public interface ITestListRepository
    {
        IEnumerable<TestList> GetTestList();
        IEnumerable<TestDetail> GetTestDetails(int testId);
        Task<TestDetail> Add(TestDetail testDetail);
        Task<Test> AddAsync(Test test);
        Task<Test> Get(int id);
        Task<TestDetail> GetTestDetail(int Id);
        TestDetail Update(TestDetail UpdatedTestDetail);
        Task<Test> DeleteConfirmed(int id);
        Task<TestDetail> DeleteAthleteConfirmed(int id);
    }
}
