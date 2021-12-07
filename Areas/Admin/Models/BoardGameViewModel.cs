using BoardGames_FinalProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Areas.Admin.Models
{
    public class BoardGameViewModel : IValidatableObject
    {
        public BoardGame BoardGame { get; set; }
        public int[] SelectedAuthors { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx) {
            if (SelectedAuthors == null)
            {
                yield return new ValidationResult(
                  "Please select at least one author.",
                  new[] { nameof(SelectedAuthors) });
            }
        }

    }
}
