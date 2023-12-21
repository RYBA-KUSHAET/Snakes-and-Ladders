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
        // ����������� ������ ����� �������
        _playersChips = playersChips;

        // ������ ������ ��� �������� ������� ������� �����
        _playersChipsCellsIds = new int[playersChips.Length];

        // �������� ����� ��� ���������� ������� ���� �����
        RefreshChipsPositions();
    }

    public void RefreshChipsPositions()
    {
        // �������� � ����� �� ���� ������ �������
        for (int i = 0; i < _playersChips.Length; i++)
        {
            // �������� ����� ��� ���������� ������� ����� � ������� i
            RefreshChipPosition(i);
        }
    }

    public void MoveChip(int playerId, int steps)
    {
        // ��������� ������� ������� �����
        int startCellId = _playersChipsCellsIds[playerId];

        // ����������� ������� ������� ����� �� �������� ����� �����
        _playersChipsCellsIds[playerId] += steps;

        // ���� ������� ������� ����� ��������� ���������� ����� �� ������� ����
        if (_playersChipsCellsIds[playerId] >= GameField.CellCount)
        {
            // ������������� ����� �� ��������� ������
            _playersChipsCellsIds[playerId] = GameField.CellCount - 1;
        }
        // ��������� ����� ������� �����
        int lastCellId = _playersChipsCellsIds[playerId];

        // ���������, ���� �� ��� ������� �� ���� ��� ��������
        TryApplyTransition(playerId);

        // ��������� ������� ����� ����� ���������� ��������
        int afterTransitionCellId = _playersChipsCellsIds[playerId];

        // �������� ������ �����, �� ������� ����� �����
        int[] movementCells = GetMovementCells(startCellId, lastCellId, afterTransitionCellId);

        // ��������� �������� �������� �����
        PlayersChipsAnimator.AnimateChipMovement(_playersChips[playerId], movementCells);
    }

    private void RefreshChipPosition(int playerId)
    {
        // �������� ������� ������ �� ������� ����, ������� ������������� ������� ������� �����
        Vector2 chipPosition = GameField.GetCellPosition(_playersChipsCellsIds[playerId]);

        // ������������� ����� �� ���������� �������
        _playersChips[playerId].SetPosition(chipPosition);
    }

    private void TryApplyTransition(int playerId)
    {
        // �������� ����� ����� ������ ����� ���� ������
        int resultCellId = TransitionSettings.GetTransitionResultCellId(_playersChipsCellsIds[playerId]);

        // ���� ����� ������ 0
        if (resultCellId < 0)
        {
            // ������� �� ���� ��� �������� �� �����
            return;
        }

        // ������������� ����� ����� ������
        _playersChipsCellsIds[playerId] = resultCellId;
    }

    public bool CheckPlayerFinished(int playerId)
    {
        // ���������� true, ���� ����� ������� ������ ���������� ������ ������ ��� ����� ���������� ������ �� ������� ���� - 1
        return _playersChipsCellsIds[playerId] >= GameField.CellCount - 1;
    }

    private int[] GetMovementCells(int startCellId, int lastCellId, int afterTransitionCellId)
    {
        // ��������� ���������� �����, ������� ������ �������� �����
        int cellsCount = lastCellId - startCellId + 1;

        // ���������, ���� �� ������� �� ���� ��� �������� � ���������� ��������� ������ � ������ ����� ��������
        bool hasTransition = lastCellId != afterTransitionCellId;

        // ���� ���� ������� �� ���� ��� ��������
        if (hasTransition)
        {
            // ����������� ���������� ����� �� 1
            cellsCount++;
        }

        // ������ ������ � ��������� ����������� �����
        int[] movementCells = new int[cellsCount];

        // �������� �� ����� ������� �����
        for (int i = 0; i < movementCells.Length; i++)
        {
            // ���� ��� ��������� ������ � �� ��� ���� �������
            if (i == movementCells.Length - 1 && hasTransition)
            {
                // ���������� ����� ������ ����� ��������
                movementCells[i] = afterTransitionCellId;
            }
            // �����
            else
            {
                // ���������� ����� ������� ������
                movementCells[i] = startCellId + i;
            }
        }
        // ���������� ������ � �������� ����� ��� ��������
        return movementCells;
    }
}
