using Minesweeper.Enum;
using Minesweeper.Interfaces;
using Minesweeper.Models;
using System;

namespace Minesweeper.Components;

public partial class FieldComponent : ContentView
{
	private ICell[,] cells;
	private GameConfig config;
	private int row = 0;
	private int column = 0;

	public FieldComponent()
	{
		InitializeComponent();
		GameConfig config = new()
		{
			RowNumber = 10,
			ColumnNumber = 10,
			Difficulty = Enum.Difficulty.Easy
		};

		this.config = config;

        AddCells();
    }

	private void AddCells()
	{
		cells = new ICell[config.RowNumber, config.ColumnNumber];

		for(int row = 0; row < config.RowNumber; row++)
			for (int col = 0; col < config.ColumnNumber; col++)
				cells[row, col] = ChooseCellType();

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

        var cell = cells[row, column];
		cell.SetNeighbords(neighbords);
		var cellComponent = new CellComponent(cell);
        rowLayout.Children.Add(cellComponent);

	}

	private ICell ChooseCellType()
	{
		var isMine = CheckIfIsMine(config.Difficulty);

		if (isMine)
			return new MineCell();

		return new SafeCell();
    }

	private bool CheckIfIsMine(Difficulty difficulty)
	{
        Random random = new Random();

        double chance = random.NextDouble();

        return chance <= ((double)difficulty / 100.0);
    }

	private ICell[] GetNeighbords()
	{
		List<ICell> cellList = [];

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