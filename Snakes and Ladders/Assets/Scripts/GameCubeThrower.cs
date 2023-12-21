using UnityEngine;

public class GameCubeThrower : MonoBehaviour
{
    public GameStateChanger GameStateChanger;

    public GameCube GameCubePrefab;

    public Transform GameCubePoint;

    private GameCube _gameCube;

    // Скрипт анимации кубика
    public CubeThrowAnimator CubeThrowAnimator;

    // Значение, которое выпало на кубике
    private int _cubeValue;

    public void ThrowCube()
    {
        // Сохраняем результат броска
        _cubeValue = _gameCube.ThrowCube();

        // Проигрываем анимацию броска
        CubeThrowAnimator.PlayAnimation();
    }

    private void Start()
    {
        // Создаём новый кубик при запуске игры
        CreateGameCube();
    }

    private void CreateGameCube()
    {
        // Создаём новый кубик в указанной позиции и с указанным углом вращения
        _gameCube = Instantiate(GameCubePrefab, GameCubePoint.position, GameCubePoint.rotation, GameCubePoint);

        // Скрываем кубик, чтобы его не было видно в начале игры
        _gameCube.HideCube();
    }

    public void ContinueAfterCubeAnimation()
    {
        // Передаём значение, которое выпало на кубике, в скрипт изменения состояния игры
        GameStateChanger.DoPlayerTurn(_cubeValue);
    }
}
