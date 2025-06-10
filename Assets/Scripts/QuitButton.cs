using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitApplication()
    {
        // 게임이 실행된 상태에서만 종료 가능
        Debug.Log("게임 종료됨!");
        Application.Quit();

        // 에디터에서 실행 중일 때는 종료되지 않지만,
        // 종료 로그를 콘솔에 출력하는 방법으로 확인 가능
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
