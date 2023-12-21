using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    public GameObject GameScreenGO;

    public GameObject GameEndScreenGO;

    public TextMeshProUGUI WinText;

    public int PlayersCount = 2;

    public PlayersChipsCreator PlayersChipsCreator;

    public PlayersTurnChanger PlayersTurnChanger;

    public PlayersChipsMover PlayersChipsMover;

    public GameField GameField;

    public Button ThrowButton;

    public void DoPlayerTurn(int steps)
    {
        // Получаем номер текущего игрока
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();

        // Двигаем фишку текущего игрока на заданное число шагов
        PlayersChipsMover.MoveChip(currentPlayerId, steps);

        // Блокируем возможность бросить кубик
        SetThrowButtonInteractable(false);
    }

    // Метод для перезапуска по кнопке
    public void RestartGame()
    {
        // Удаляем фишки игроков
        PlayersChipsCreator.Clear();

        // Начинаем новую игру
        StartGame();
    }

    // Вызывается при запуске игры
    private void Start()
    {
        // Делаем первичную настройку игры
        FirstStartGame();
    }

    // Метод для первого запуска игры 
    private void FirstStartGame()
    {
        // Заполняем позиции клеток на игровом поле
        GameField.FillCellsPosition();

        // Начинаем новую игру 
        StartGame();
    }

    // Метод для начала новой игры
    private void StartGame()
    {
        // Создаём фишки игроков и сохраняем их в массиве
        PlayerChip[] playersChips = PlayersChipsCreator.SpawnPlayersChips(PlayersCount);

        // Передаём массив фишек в скрипт PlayersTurnChanger
        PlayersTurnChanger.StartGame(playersChips);

        // Передаём массив фишек в скрипт PlayersChipsMover
        PlayersChipsMover.StartGame(playersChips);

        // Показываем экран игры
        SetScreens(true);

        SetThrowButtonInteractable(true);
    }

    // Метод для завершения игры
    private void EndGame()
    {
        // Показываем экран конца игры
        SetScreens(false);
    }

    // Метод для установки видимости игровых экранов
    private void SetScreens(bool inGame)
    {
        // Если игра в процессе, показываем экран игры и скрываем экран конца игры
        GameScreenGO.SetActive(inGame);

        // Иначе скрываем экран игры и показываем экран конца игры 
        GameEndScreenGO.SetActive(!inGame);
    }

    // Метод для установки надписи о победе
    private void SetWinText(int playerId)
    {
        // Отображаем текст с номером победившего игрока
        WinText.text = $"Игрок {playerId + 1} победил!";
    }
    public void ContinueGameAfterChipAnimation()
    {
        // Получаем номер текущего игрока
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();

        // Определяем, достиг ли игрок финиша
        bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId);

        // Если игрок на финише
        if (isPlayerFinished)
        {
            // Устанавливаем надпись о победе
            SetWinText(currentPlayerId);

            // Переходим к экрану конца игры
            EndGame();
        }
        // Иначе
        else
        {
            // Передаём ход следующему игроку
            PlayersTurnChanger.MovePlayerTurn();

            // Разрешаем ему бросить кубик
            SetThrowButtonInteractable(true);
        }
    }

    private void SetThrowButtonInteractable(bool value)
    {
        // Блокируем или активируем кнопку в зависимости от value
        ThrowButton.interactable = value;
    }

}
