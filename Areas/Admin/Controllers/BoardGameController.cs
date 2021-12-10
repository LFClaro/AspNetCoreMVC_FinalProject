using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BoardGamestore.Models;
using BoardGames_FinalProject.Models.DataLayer;
using BoardGames_FinalProject.Models;
using Areas.Admin.Models;

namespace Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BoardGameController : Controller
    {
        private BoardGamestoreUnitOfWork data { get; set; }
        public BoardGameController(BoardGameContext ctx) => data = new BoardGamestoreUnitOfWork(ctx);

        public ViewResult Index() {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid) {
                var search = new SearchData(TempData) {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }  
            else {
                return RedirectToAction("Index");
            }   
        }

        [HttpGet]
        public ViewResult Search() 
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm) {
                var vm = new SearchViewModel {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<BoardGame> { };
                if (search.IsBoardGame) { 
                    options.Where = b => b.name.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for board game '{vm.SearchTerm}'";
                }
                vm.BoardGames = data.BoardGames.List(options);
                return View("SearchResults", vm);
            }
            else {
                return View("Index");
            }     
        }

        [HttpGet]
        public ViewResult Add(string id) => GetBoardGame(id, "Add");

        [HttpPost]
        public IActionResult Add(BoardGameViewModel vm)
        {
            if (ModelState.IsValid) {
                data.BoardGames.Insert(vm.BoardGame);
                data.Save();

                TempData["message"] = $"{vm.BoardGame.name} added to BoardGames.";
                return RedirectToAction("Index");  
            }
            else {
                Load(vm, "Add");
                return View("BoardGame", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => GetBoardGame(id, "Edit");
        
        [HttpPost]
        public IActionResult Edit(BoardGameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.BoardGames.Update(vm.BoardGame);
                data.Save();

                TempData["message"] = $"{vm.BoardGame.name} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("BoardGame", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(string id) => GetBoardGame(id, "Delete");

        [HttpPost]
        public IActionResult Delete(BoardGameViewModel vm)
        {
            data.BoardGames.Delete(vm.BoardGame); 
            data.Save();
            TempData["message"] = $"{vm.BoardGame.name} removed from Books.";
            return RedirectToAction("Search");  
        }

        private ViewResult GetBoardGame(string id, string operation)
        {
            var boardGame = new BoardGameViewModel();
            Load(boardGame, operation, id);
            return View("BoardGame", boardGame);
        }
        private void Load(BoardGameViewModel vm, string op, string? id = null)
        {
            if (Operation.IsAdd(op)) { 
                vm.BoardGame = new BoardGame();
            }
            else {
                vm.BoardGame = data.BoardGames.Get(new QueryOptions<BoardGame>
                {
                    Where = b => b.ID == (id ?? vm.BoardGame.ID)
                });
            }

        }

    }
}