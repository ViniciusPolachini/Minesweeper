# Minesweeper
## Rules
### Board: 
- NxM matrix, with N ∈ Z & M ∈ Z ∈ N >= 3 ∈ M >= 3
- The matrix have K cells that are mines with:
> K = d\*N\*MA

> 0.15 >= d >= 0.35
### Rules:
- When the user clicks on a cell, there is one of this results:
	1. Is a cell without a mine and without any mine as a neighbor.
    2. Is a cell without a mine, but with a mine as a neighbor.
	3. Is a mine
- If is the option i, mark all the region until reaching the case ii cell.
- If is the option ii, mark the cell reveal how many mines are around this cell.
- If is the option iii, the user **LOST** the game.
- The player **WIN** the game if all the not mine cells are marked.


