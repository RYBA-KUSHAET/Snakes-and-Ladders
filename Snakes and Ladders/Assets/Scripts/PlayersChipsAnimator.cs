using UnityEngine;

public class PlayersChipsAnimator : MonoBehaviour
{
    // Скрипт изменения состояния игры
    public GameStateChanger GameStateChanger;

    // Скрипт игрового поля
    public GameField GameField;

    // Длительность перемещения между ячейками
    public float CellMoveDuration = 0.3f;

    // Префаб фишки текущего игрока
    private PlayerChip _playerChip;

    // Флаг, который указывает, выполняется ли сейчас анимация
    private bool isAnimateNow;

    // Массив ячеек, по которым нужно переместиться
    private int[] _movementCells;

    // Индекс текущей ячейки, которую анимируют
    private int _currentCellId;

    // Временной счётчик для анимации
    private float _cellMoveTimer;

    // Начальная позиция перемещения
    private Vector2 _startPosition;

    // Конечная позиция перемещения
    private Vector2 _endPosition;

    // Start is called before the first frame update
    public void AnimateChipMovement(PlayerChip playerChip, int[] movementCells)
    {
        // Сохраняем переданную фишку в переменную
        _playerChip = playerChip;

        // Получаем массив ячеек, через которые фишка должна пройти
        _movementCells = movementCells;

        // Задаём флаг, который указывает, что анимация идёт сейчас
        isAnimateNow = true;

        // Устанавливаем начальное значение текущей ячейки
        _currentCellId = -1;

        // Начинаем движение к следующей ячейке
        ToNextCell();
    }

    private void Update()
    {
        // Вызываем метод управления анимацией
        Animation();
    }

    private void Animation()
    {
        // Если анимация сейчас не выполняется
        if (!isAnimateNow)
        {
            // Выходим из метода
            return;
        }
        // Если таймер движения фишки достиг значения 1
        if (_cellMoveTimer >= 1)
        {
            // Переходим к следующей ячейке
            ToNextCell();
        }
        // Вычисляем промежуточную позицию фишки между начальной и конечной позицией
        _playerChip.SetPosition(Vector2.Lerp(_startPosition, _endPosition, _cellMoveTimer));

        // Увеличиваем таймер на основе прошедшего времени
        _cellMoveTimer += Time.deltaTime / CellMoveDuration;
    }

    private void ToNextCell()
    {
        // Увеличиваем текущий номер ячейки на 1
        _currentCellId++;

        // Если текущий номер ячейки больше или равен общему количеству ячеек - 1
        if (_currentCellId >= _movementCells.Length - 1)
        {
            // Завершаем анимацию
            EndAnimation();

            // Выходим из метода
            return;
        }
        // Получаем начальную позицию для анимации из класса GameField с помощью текущего номера ячейки
        _startPosition = GameField.GetCellPosition(_movementCells[_currentCellId]);

        // Получаем конечную позицию для анимации из класса GameField с помощью следующего номера ячейки
        _endPosition = GameField.GetCellPosition(_movementCells[_currentCellId + 1]);

        // Сбрасываем таймер перемещения на 0
        _cellMoveTimer = 0;
    }

    private void EndAnimation()
    {
        // Задаём флагу isAnimateNow значение false, то есть указываем, что анимация завершилась
        isAnimateNow = false;

        // Продолжаем игру после анимации фишки с помощью функции ContinueGameAfterChipAnimation() из класса GameStateChanger
        GameStateChanger.ContinueGameAfterChipAnimation();
    }

}
