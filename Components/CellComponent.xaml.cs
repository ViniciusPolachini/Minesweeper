using Minesweeper.Enum;
using System.Windows.Input;

namespace Minesweeper.Components;

public partial class CellComponent : ContentView
{	
	public ICommand CellClickedCommand { get; set; }

    private IEnumerable<CellComponent> neighbords = [];
	private CellType cellType;

	public CellComponent()
	{
        CellClickedCommand = new Command(OnClick);
        InitializeComponent();
        BindingContext = this;
	}

	public void ConfigCell(CellType cellType, IEnumerable<CellComponent> neighbords)
	{
        this.cellType = cellType;
        this.neighbords = neighbords;
    }

	public bool IsMine()
	{
		return cellType.Equals(CellType.Mine);
	}

	public void OnClick()
	{
		if (IsMine())
		{
			LoseTheGame();
			return;
		}

		var mines = neighbords.ToList().FindAll(cell => cell.IsMine());

		buttonLabel.Text = $"{mines.Count}";

		if(mines.Count == 0)
			foreach(var cell in neighbords)
				cell.OnClick();
	}

	private void LoseTheGame()
	{
		var page = Application.Current?.MainPage;
        buttonLabel.Text = "X";
        var decision = page.DisplayAlert("You lose!!", "Want to try again???", "Yes", "No");
    }
}