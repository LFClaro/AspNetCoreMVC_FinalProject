using BoardGames_FinalProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Areas.Admin.Models
{
    public class SearchViewModel
    {
        public IEnumerable<BoardGame> BoardGames { get; set; }
        [Required(ErrorMessage = "Please enter a search term.")]
        public string SearchTerm { get; set; }
        public string Type { get; set; }
        public string Header { get; set; }
    }
}
