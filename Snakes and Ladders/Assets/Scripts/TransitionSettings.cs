using UnityEngine;

public class TransitionSettings : MonoBehaviour
{
    public TransitionData[] TransitionDatas = new TransitionData[0];
    public int GetTransitionResultCellId(int startCellId)
    {
        // ѕроходим в цикле по всему массиву с данными о перемещени€х по зме€м и лестницам
        for (int i = 0; i < TransitionDatas.Length; i++)
        {
            // ѕолучаем новый номер €чейки из TransitionData дл€ данного значени€ startCellId
            int resultCellId = TransitionDatas[i].GetTransitionResultCellId(startCellId);

            // ≈сли resultCellId больше или равен 0
            if (resultCellId >= 0)
            {
                // «начит, перемещение возможно
                return resultCellId;
            }
        }
        // ≈сли условие не выполнилось, возвращаем -1 Ч это значит, что перемещени€ по змее или лестнице нет 
        return -1;
    }
}
