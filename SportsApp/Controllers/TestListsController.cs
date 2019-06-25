using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsApp.Models;

namespace SportsApp.Controllers
{
    public class TestListsController : Controller
    {
        private readonly ITestListRepository _TestListRpository;
        private readonly IUnitOfWork _UnitOfWork;
        public TestListsController(ITestListRepository TestList, IUnitOfWork UnitOfWork)
        {
            _TestListRpository = TestList;
            _UnitOfWork = UnitOfWork;
        }

        // GET: Test
        public IActionResult Index()
        {
            var testList = _TestListRpository.GetTestList();
            if (testList == null)
            {
                return NotFound();
            }
            return View(testList);
        }

        // GET: Test/Details/5
        public IActionResult Details(int Id)
        {
            TestDetail td = new TestDetail();
            td.TestId = Id;
            var testDetail = _TestListRpository.GetTestDetails(Id);
            return View(testDetail);

        }

        // GET: Test/Create
        public IActionResult Create(int Id)
        {
            TestDetail td = new TestDetail();
            td.TestId = Id;
            return View(td);
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AthleteName,Distance,Rating,TestId")] TestDetail testDetail)
        {
            if (ModelState.IsValid)
            {
                await _TestListRpository.Add(testDetail);
                await _UnitOfWork.Commit();
                return Redirect("https://localhost:44340/TestLists/Details/" + testDetail.TestId);
            }
            return View(testDetail);
        }

        // GET: Test/Create
        public IActionResult CreateTest()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest([Bind("Id,Date,TestType")] Test test)
        {
            if (ModelState.IsValid)
            {
                await _TestListRpository.AddAsync(test);
                await _UnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDetail = await _TestListRpository.GetTestDetail(id ?? 1);
            if (testDetail == null)
            {
                return NotFound();
            }
            return View(testDetail);
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AthleteName,Distance,Rating,TestId")] TestDetail testDetail)
        {
            if (id != testDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _TestListRpository.Update(testDetail);
                    await _UnitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return Redirect("https://localhost:44340/TestLists/Details/" + testDetail.TestId);
            }
            return View(testDetail);
        }


        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _TestListRpository.Get(id ?? 1);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _TestListRpository.DeleteConfirmed(id);
            await _UnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> DeleteAthlete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _TestListRpository.GetTestDetail(id ?? 1);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("DeleteAthlete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAthleteConfirmed(int id)
        {
            await _TestListRpository.DeleteAthleteConfirmed(id);
            await _UnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

    }
}
