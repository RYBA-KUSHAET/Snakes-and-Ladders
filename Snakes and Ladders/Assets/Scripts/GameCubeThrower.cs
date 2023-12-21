using UnityEngine;

public class GameCubeThrower : MonoBehaviour
{
    public GameStateChanger GameStateChanger;

    public GameCube GameCubePrefab;

    public Transform GameCubePoint;

    private GameCube _gameCube;

    // ������ �������� ������
    public CubeThrowAnimator CubeThrowAnimator;

    // ��������, ������� ������ �� ������
    private int _cubeValue;

    public void ThrowCube()
    {
        // ��������� ��������� ������
        _cubeValue = _gameCube.ThrowCube();

        // ����������� �������� ������
        CubeThrowAnimator.PlayAnimation();
    }

    private void Start()
    {
        // ������ ����� ����� ��� ������� ����
        CreateGameCube();
    }

    private void CreateGameCube()
    {
        // ������ ����� ����� � ��������� ������� � � ��������� ����� ��������
        _gameCube = Instantiate(GameCubePrefab, GameCubePoint.position, GameCubePoint.rotation, GameCubePoint);

        // �������� �����, ����� ��� �� ���� ����� � ������ ����
        _gameCube.HideCube();
    }

    public void ContinueAfterCubeAnimation()
    {
        // ������� ��������, ������� ������ �� ������, � ������ ��������� ��������� ����
        GameStateChanger.DoPlayerTurn(_cubeValue);
    }
}
