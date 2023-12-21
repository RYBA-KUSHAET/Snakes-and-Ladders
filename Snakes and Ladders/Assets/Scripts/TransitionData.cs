using UnityEngine;

public class TransitionData : MonoBehaviour
{

    public int[] CellsStartIds;

    public int[] CellsEndIds;
    public int GetTransitionResultCellId(int cellId)
    {
        // Проходим в цикле по всем начальным ячейкам
        for (int i = 0; i < CellsStartIds.Length; i++)
        {
            // Если номер начальной ячейки совпадает с переданным
            if (CellsStartIds[i] == cellId)
            {
                // Возвращаем номер конечной ячейки для этой змеи или лестницы
                return CellsEndIds[i];
            }
        }
        // Если условие не выполнилось, возвращаем -1 — это значит, что перемещения по змее или лестнице нет
        return -1;
    }
}
