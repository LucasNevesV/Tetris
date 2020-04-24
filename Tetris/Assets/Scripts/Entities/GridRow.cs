using System.Linq;
using UnityEngine;

public class GridRow
{
    public GridRow()
    {
        Row = new Transform[GameConfig.GridRowSize];
    }

    public Transform[] Row;

    public bool Compleat => !Row.Any(r => r == null);
}
