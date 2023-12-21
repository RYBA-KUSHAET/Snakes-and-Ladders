using UnityEngine;

public class PlayersChipsAnimator : MonoBehaviour
{
    // ������ ��������� ��������� ����
    public GameStateChanger GameStateChanger;

    // ������ �������� ����
    public GameField GameField;

    // ������������ ����������� ����� ��������
    public float CellMoveDuration = 0.3f;

    // ������ ����� �������� ������
    private PlayerChip _playerChip;

    // ����, ������� ���������, ����������� �� ������ ��������
    private bool isAnimateNow;

    // ������ �����, �� ������� ����� �������������
    private int[] _movementCells;

    // ������ ������� ������, ������� ���������
    private int _currentCellId;

    // ��������� ������� ��� ��������
    private float _cellMoveTimer;

    // ��������� ������� �����������
    private Vector2 _startPosition;

    // �������� ������� �����������
    private Vector2 _endPosition;

    // Start is called before the first frame update
    public void AnimateChipMovement(PlayerChip playerChip, int[] movementCells)
    {
        // ��������� ���������� ����� � ����������
        _playerChip = playerChip;

        // �������� ������ �����, ����� ������� ����� ������ ������
        _movementCells = movementCells;

        // ����� ����, ������� ���������, ��� �������� ��� ������
        isAnimateNow = true;

        // ������������� ��������� �������� ������� ������
        _currentCellId = -1;

        // �������� �������� � ��������� ������
        ToNextCell();
    }

    private void Update()
    {
        // �������� ����� ���������� ���������
        Animation();
    }

    private void Animation()
    {
        // ���� �������� ������ �� �����������
        if (!isAnimateNow)
        {
            // ������� �� ������
            return;
        }
        // ���� ������ �������� ����� ������ �������� 1
        if (_cellMoveTimer >= 1)
        {
            // ��������� � ��������� ������
            ToNextCell();
        }
        // ��������� ������������� ������� ����� ����� ��������� � �������� ��������
        _playerChip.SetPosition(Vector2.Lerp(_startPosition, _endPosition, _cellMoveTimer));

        // ����������� ������ �� ������ ���������� �������
        _cellMoveTimer += Time.deltaTime / CellMoveDuration;
    }

    private void ToNextCell()
    {
        // ����������� ������� ����� ������ �� 1
        _currentCellId++;

        // ���� ������� ����� ������ ������ ��� ����� ������ ���������� ����� - 1
        if (_currentCellId >= _movementCells.Length - 1)
        {
            // ��������� ��������
            EndAnimation();

            // ������� �� ������
            return;
        }
        // �������� ��������� ������� ��� �������� �� ������ GameField � ������� �������� ������ ������
        _startPosition = GameField.GetCellPosition(_movementCells[_currentCellId]);

        // �������� �������� ������� ��� �������� �� ������ GameField � ������� ���������� ������ ������
        _endPosition = GameField.GetCellPosition(_movementCells[_currentCellId + 1]);

        // ���������� ������ ����������� �� 0
        _cellMoveTimer = 0;
    }

    private void EndAnimation()
    {
        // ����� ����� isAnimateNow �������� false, �� ���� ���������, ��� �������� �����������
        isAnimateNow = false;

        // ���������� ���� ����� �������� ����� � ������� ������� ContinueGameAfterChipAnimation() �� ������ GameStateChanger
        GameStateChanger.ContinueGameAfterChipAnimation();
    }

}
