
using System.ComponentModel;

namespace Minesweeper.Interfaces;
public interface ICell : INotifyPropertyChanged
{
    void CellInteraction();
    void SetNeighbors(IEnumerable<ICell> neighbors);
    bool CheckIfItHasNotBeenInteracted();
}
