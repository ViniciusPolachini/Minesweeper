using Minesweeper.Enum;
using Minesweeper.Interfaces;
using Minesweeper.Models;
using System.Windows.Input;

namespace Minesweeper.Components;

public partial class CellComponent : ContentView
{	
	public CellComponent(ICell cell)
	{
		int x;
		if (cell is MineCell)
			x = 0;
		
        InitializeComponent();
        BindingContext = cell;
	}
}