using UnityEngine;

public class PlayersChipsMover : MonoBehaviour
{
    public GameField GameField;

    private PlayerChip[] _playersChips;

    private int[] _playersChipsCellsIds;

    public TransitionSettings TransitionSettings;

    public PlayersChipsAnimator PlayersChipsAnimator;
    public void StartGame(PlayerChip[] playersChips)
    {
        // Присваиваем массив фишек игроков
        _playersChips = playersChips;

        // Создаём массив для хранения текущих позиций фишек
        _playersChipsCellsIds = new int[playersChips.Length];

        // Вызываем метод для обновления позиций всех фишек
        RefreshChipsPositions();
    }

    public void RefreshChipsPositions()
    {
        // Проходим в цикле по всем фишкам игроков
        for (int i = 0; i < _playersChips.Length; i++)
        {
            // Вызываем метод для обновления позиции фишки с номером i
            RefreshChipPosition(i);
        }
    }

    public void MoveChip(int playerId, int steps)
    {
        // Сохраняем текущую позицию фишки
        int startCellId = _playersChipsCellsIds[playerId];

        // Увеличиваем текущую позицию фишки на заданное число шагов
        _playersChipsCellsIds[playerId] += steps;

        // Если текущая позиция фишки превышает количество ячеек на игровом поле
        if (_playersChipsCellsIds[playerId] >= GameField.CellCount)
        {
            // Устанавливаем фишку на последнюю ячейку
            _playersChipsCellsIds[playerId] = GameField.CellCount - 1;
        }
        // Сохраняем новую позицию фишки
        int lastCellId = _playersChipsCellsIds[playerId];

        // Проверяем, есть ли там переход по змее или лестнице
        TryApplyTransition(playerId);

        // Сохраняем позицию фишки после возможного перехода
        int afterTransitionCellId = _playersChipsCellsIds[playerId];

        // Получаем номера ячеек, по которым пойдёт фишка
        int[] movementCells = GetMovementCells(startCellId, lastCellId, afterTransitionCellId);

        // Запускаем анимацию движения фишки
        PlayersChipsAnimator.AnimateChipMovement(_playersChips[playerId], movementCells);
    }

    private void RefreshChipPosition(int playerId)
    {
        // Получаем позицию ячейки на игровом поле, которая соответствует текущей позиции фишки
        Vector2 chipPosition = GameField.GetCellPosition(_playersChipsCellsIds[playerId]);

        // Устанавливаем фишку на полученную позицию
        _playersChips[playerId].SetPosition(chipPosition);
    }

    private void TryApplyTransition(int playerId)
    {
        // Получаем новый номер ячейки после хода игрока
        int resultCellId = TransitionSettings.GetTransitionResultCellId(_playersChipsCellsIds[playerId]);

        // Если номер меньше 0
        if (resultCellId < 0)
        {
            // Переход по змее или лестнице не нужен
            return;
        }

        // Устанавливаем новый номер ячейки
        _playersChipsCellsIds[playerId] = resultCellId;
    }

    public bool CheckPlayerFinished(int playerId)
    {
        // Возвращаем true, если номер текущей клетки указанного игрока больше или равен количеству клеток на игровом поле - 1
        return _playersChipsCellsIds[playerId] >= GameField.CellCount - 1;
    }

    private int[] GetMovementCells(int startCellId, int lastCellId, int afterTransitionCellId)
    {
        // Вычисляем количество ячеек, которые должна посетить фишка
        int cellsCount = lastCellId - startCellId + 1;

        // Проверяем, есть ли переход по змее или лестнице — сравниваем последнюю ячейку и ячейку после перехода
        bool hasTransition = lastCellId != afterTransitionCellId;

        // Если есть переход по змее или лестнице
        if (hasTransition)
        {
            // Увеличиваем количество ячеек на 1
            cellsCount++;
        }

        // Создаём массив с указанным количеством ячеек
        int[] movementCells = new int[cellsCount];

        // Проходим по всему массиву ячеек
        for (int i = 0; i < movementCells.Length; i++)
        {
            // Если это последняя клетка и на ней есть переход
            if (i == movementCells.Length - 1 && hasTransition)
            {
                // Записываем номер ячейки после перехода
                movementCells[i] = afterTransitionCellId;
            }
            // Иначе
            else
            {
                // Записываем номер текущей ячейки
                movementCells[i] = startCellId + i;
            }
        }
        // Возвращаем массив с номерами ячеек для анимации
        return movementCells;
    }
}
