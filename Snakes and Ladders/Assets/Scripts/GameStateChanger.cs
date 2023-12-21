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
        // �������� ����� �������� ������
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();

        // ������� ����� �������� ������ �� �������� ����� �����
        PlayersChipsMover.MoveChip(currentPlayerId, steps);

        // ��������� ����������� ������� �����
        SetThrowButtonInteractable(false);
    }

    // ����� ��� ����������� �� ������
    public void RestartGame()
    {
        // ������� ����� �������
        PlayersChipsCreator.Clear();

        // �������� ����� ����
        StartGame();
    }

    // ���������� ��� ������� ����
    private void Start()
    {
        // ������ ��������� ��������� ����
        FirstStartGame();
    }

    // ����� ��� ������� ������� ���� 
    private void FirstStartGame()
    {
        // ��������� ������� ������ �� ������� ����
        GameField.FillCellsPosition();

        // �������� ����� ���� 
        StartGame();
    }

    // ����� ��� ������ ����� ����
    private void StartGame()
    {
        // ������ ����� ������� � ��������� �� � �������
        PlayerChip[] playersChips = PlayersChipsCreator.SpawnPlayersChips(PlayersCount);

        // ������� ������ ����� � ������ PlayersTurnChanger
        PlayersTurnChanger.StartGame(playersChips);

        // ������� ������ ����� � ������ PlayersChipsMover
        PlayersChipsMover.StartGame(playersChips);

        // ���������� ����� ����
        SetScreens(true);

        SetThrowButtonInteractable(true);
    }

    // ����� ��� ���������� ����
    private void EndGame()
    {
        // ���������� ����� ����� ����
        SetScreens(false);
    }

    // ����� ��� ��������� ��������� ������� �������
    private void SetScreens(bool inGame)
    {
        // ���� ���� � ��������, ���������� ����� ���� � �������� ����� ����� ����
        GameScreenGO.SetActive(inGame);

        // ����� �������� ����� ���� � ���������� ����� ����� ���� 
        GameEndScreenGO.SetActive(!inGame);
    }

    // ����� ��� ��������� ������� � ������
    private void SetWinText(int playerId)
    {
        // ���������� ����� � ������� ����������� ������
        WinText.text = $"����� {playerId + 1} �������!";
    }
    public void ContinueGameAfterChipAnimation()
    {
        // �������� ����� �������� ������
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();

        // ����������, ������ �� ����� ������
        bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId);

        // ���� ����� �� ������
        if (isPlayerFinished)
        {
            // ������������� ������� � ������
            SetWinText(currentPlayerId);

            // ��������� � ������ ����� ����
            EndGame();
        }
        // �����
        else
        {
            // ������� ��� ���������� ������
            PlayersTurnChanger.MovePlayerTurn();

            // ��������� ��� ������� �����
            SetThrowButtonInteractable(true);
        }
    }

    private void SetThrowButtonInteractable(bool value)
    {
        // ��������� ��� ���������� ������ � ����������� �� value
        ThrowButton.interactable = value;
    }

}
