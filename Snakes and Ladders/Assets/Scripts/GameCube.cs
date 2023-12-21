using UnityEngine;

public class GameCube : MonoBehaviour
{
    public Vector3[] cubeSidesEulers;

    public void ShowCube()
    {
        SetActiveCube(true);
    }

    public void HideCube()
    {
        SetActiveCube(false);
    }

    private void SetActiveCube(bool value)
    {
        gameObject.SetActive(value);
    }

    public int ThrowCube()
    {
        ShowCube();

        int randomValue = Random.Range(0, cubeSidesEulers.Length);

        RotateCube(cubeSidesEulers[randomValue]);

        return randomValue + 1;
    }

    private void RotateCube(Vector3 cubeEuler)
    {
        transform.eulerAngles = cubeEuler;
    }
}
