
using System.ComponentModel;

namespace Minesweeper.Interfaces;
public interface ICell : INotifyPropertyChanged
{
    void CellInteraction();
    void SetNeighbords(IEnumerable<ICell> neighbords);
    bool CheckIfAlreadyBeInteracted();
}
