using Minesweeper.Enum;

using Minesweeper.Models;
using System;

namespace Minesweeper.Components;

public partial class FieldComponent : ContentView
{
	private CellComponent[,] cells;
	private GameConfig config;
	private int row = 0;
	private int column = 0;

	public FieldComponent()
	{
		InitializeComponent();
		GameConfig config = new()
		{
			RowNumber = 20,
			ColumnNumber = 20,
			Difficulty = Enum.Difficulty.Easy
		};

		this.config = config;

        AddCells();
    }

	private void AddCells()
	{
		cells = new CellComponent[config.RowNumber, config.ColumnNumber];

		for(int row = 0; row < config.RowNumber; row++)
			for (int col = 0; col < config.ColumnNumber; col++)
				cells[row, col] = new();

        for (row = 0; row < config.RowNumber; row++)
            CreateRow();
	}

	private void CreateRow()
	{
		HorizontalStackLayout rowLayout = new();

		for (column = 0; column < config.ColumnNumber; column++)
			AddCellsToRow(rowLayout);

		CellsContainer.Children.Add(rowLayout);
	}

	private void AddCellsToRow(HorizontalStackLayout rowLayout) 
	{
		var neighbords = GetNeighbords();
		var cellType = ChooseCellType();

        var cellComponent = cells[row, column];
        rowLayout.Children.Add(cellComponent);
        cellComponent.ConfigCell(cellType, neighbords);

	}

	private CellType ChooseCellType()
	{
		var isMine = CheckIfIsMine(config.Difficulty);

		if (isMine)
			return CellType.Mine;

		return CellType.Safe;
    }

	private bool CheckIfIsMine(Difficulty difficulty)
	{
        Random random = new Random();

        double chance = random.NextDouble();

        return chance <= ((double)difficulty / 100.0);
    }

	private CellComponent[] GetNeighbords()
	{
		List<CellComponent> cellList = [];

		for (var i = row - 1; i <= row + 1; i++)
			for (var j = column - 1; j <= column + 1; j++)
				if (CheckIfIsAValidField(i, j))
					cellList.Add(cells[i,j]);

		return cellList.ToArray();
	}

	private bool CheckIfIsAValidField(int i, int j)
	{
		var rowIsValid = i >= 0 && i < config.RowNumber;
		var columnIsValid = j >= 0 && j < config.ColumnNumber;
		var isNotCurrentCell = i != 0 || j != 0;


        return rowIsValid && columnIsValid && isNotCurrentCell;
    }
}