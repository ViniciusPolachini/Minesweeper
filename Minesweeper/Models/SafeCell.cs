using Minesweeper.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Minesweeper.Models;
internal class SafeCell : ICell
{

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
    private IEnumerable<ICell> neighbords;

    public SafeCell()
    {
        CellText = "";
        CellInteractionCommand = new Command(CellInteraction);
        this.neighbords = [];
    }

    public void CellInteraction()
    {

        var mines = neighbords.ToList().FindAll(cell => cell is MineCell);

        CellText = $"{mines.Count}";

        if (mines.Count == 0)
            foreach (var cell in neighbords)
                if(cell.CheckIfAlreadyBeInteracted())
                    cell.CellInteraction();
    }
       
    public void SetNeighbords(IEnumerable<ICell> neighbords)
    {
        this.neighbords = neighbords;
    }
    public bool CheckIfAlreadyBeInteracted()
    {
        return string.IsNullOrEmpty(CellText);
    }
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

