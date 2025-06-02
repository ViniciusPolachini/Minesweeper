using Minesweeper.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Colors = Minesweeper.Helpers.Colors;

namespace Minesweeper.Models;
internal class SafeCell : ICell
{
    private string? _cellColor;
    public string? CellColor
    {
        get => _cellColor;
        set
        {
            if (_cellColor != value)
            {
                _cellColor = value;
                OnPropertyChanged();
            }
        }
    }
    private string _cellText;
    public string CellText
    {
        get => _cellText;
        set
        {
            if (_cellText != value)
            {
                _cellText = value;
                OnPropertyChanged();
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public ICommand CellInteractionCommand { get; set; }
    private IEnumerable<ICell> neighbors;

    public SafeCell()
    {
        CellText = "";
        CellColor = Colors.UntouchedCell;
        CellInteractionCommand = new Command(CellInteraction);
        this.neighbors = [];
    }

    public void CellInteraction()
    {
        if (!CheckIfItHasNotBeenInteracted())
            return;

        var mines = neighbors.ToList().FindAll(cell => cell is MineCell);

        CellColor = Colors.TouchedCell;

        if(mines.Count != 0)
            CellText = $"{mines.Count}";

        if (mines.Count == 0)
            foreach (var cell in neighbors)
                if (cell.CheckIfItHasNotBeenInteracted())
                    cell.CellInteraction();
    }
       
    public void SetNeighbors(IEnumerable<ICell> neighbors)
    {
        this.neighbors = neighbors;
    }
    public bool CheckIfItHasNotBeenInteracted()
    {
        return CellColor == Colors.UntouchedCell;
    }
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

