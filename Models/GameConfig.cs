using Minesweeper.Enum;
using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Models;
internal class GameConfig
{
    [Range(2, int.MaxValue, ErrorMessage = "Row number need to be greater than 2")]
    public int RowNumber { get; set; }
    [Range(2, int.MaxValue, ErrorMessage = "Column number need to be greater than 2")]
    public int ColumnNumber { get; set; }
    public Difficulty Difficulty { get; set; }
}
