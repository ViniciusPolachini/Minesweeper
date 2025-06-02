using Minesweeper.Interfaces;

namespace Minesweeper.Components;

public partial class CellComponent : ContentView
{	
	public CellComponent(ICell cell)
	{
        InitializeComponent();
        BindingContext = cell;
	}
}