using Minesweeper.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Colors = Minesweeper.Helpers.Colors;

namespace Minesweeper.Models;
internal class MineCell : ICell
{

    private string? _cellColor;
    public string? CellColor {
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

    private string? _cellText;
    public string? CellText
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

    public ICommand CellInteractionCommand { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;
    private IEnumerable<ICell> neighbors;

    public MineCell()
    {
        CellColor = Colors.UntouchedCell;
        CellText = "";
        this.neighbors = [];
        CellInteractionCommand = new Command(CellInteraction);
    }

    public void CellInteraction()
    {
        LoseTheGame();
    }
    public void SetNeighbors(IEnumerable<ICell> neighbors)
    {
        this.neighbors = neighbors;
    }
    public bool CheckIfItHasNotBeenInteracted()
    {
        return string.IsNullOrEmpty(CellText);
    }

    private void LoseTheGame()
    {
        var page = Application.Current?.MainPage;
        CellColor = Colors.TouchedCell;
        CellText = $"X";
        var decision = page.DisplayAlert("You lose!!", "Want to try again???", "Yes", "No");
    }


    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
