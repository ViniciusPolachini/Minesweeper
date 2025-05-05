using Minesweeper.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Minesweeper.Models;
internal class MineCell : ICell
{

    private string _cellText;
    public string CellText {
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
    private IEnumerable<ICell> neighbords;

    public MineCell()
    {
        CellText = "";
        this.neighbords = [];
        CellInteractionCommand = new Command(CellInteraction);
    }

    public void CellInteraction()
    {
        LoseTheGame();
    }
    public void SetNeighbords(IEnumerable<ICell> neighbords)
    {
        this.neighbords = neighbords;
    }
    public bool CheckIfAlreadyBeInteracted()
    {
        return string.IsNullOrEmpty(CellText);
    }

    private void LoseTheGame()
    {
        var page = Application.Current?.MainPage;
        CellText = $"X";
        var decision = page.DisplayAlert("You lose!!", "Want to try again???", "Yes", "No");
    }


    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
