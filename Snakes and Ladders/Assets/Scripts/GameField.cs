using UnityEngine;

public class GameField : MonoBehaviour
{
    private Vector2[] cellsPosition;

    public int CellCount = 100;
    public int CellsInRow = 10;

    public Vector2 CellSize;
    public Transform FirstCellPoint;

    public void FillCellsPosition()
    {
        cellsPosition = new Vector2[CellCount];

        bool right = true;

        cellsPosition[0] = FirstCellPoint.position;

        for (int i = 1; i < cellsPosition.Length; i++)
        {
            bool needUp = i % CellsInRow == 0;

            if (needUp == true)
            {
                right = !right;

                cellsPosition[i] = cellsPosition[i - 1] + Vector2.up * CellSize.y;
            }
            else 
            {
                float xSign = right ? 1 : -1;
                float deltaX = xSign * CellSize.x;

                cellsPosition[i] = cellsPosition[i - 1] + Vector2.right * deltaX;
            }
        }
    }
    public Vector2 GetCellPosition(int id)
    {
        if (id < 0 || id >= cellsPosition.Length)
        {
            return Vector2.zero;
        }

        return cellsPosition[id];
    }
}
