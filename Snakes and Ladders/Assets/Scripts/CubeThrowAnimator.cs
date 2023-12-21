using UnityEngine;

public class CubeThrowAnimator : MonoBehaviour
{
    // Ссылка на компонент анимации кубика
    public Animation CubeAnimation;

    // Скрипт бросков кубика
    public GameCubeThrower GameCubeThrower;

    public void PlayAnimation()
    {
        // Проигрываем анимацию
        CubeAnimation.Play();
    }

    // Этот метод мы вызовем позже внутри анимации
    public void OnAnimationEnd()
    {
        // Продолжаем действия после анимации
        GameCubeThrower.ContinueAfterCubeAnimation();
    }
}
