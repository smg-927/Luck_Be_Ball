using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitApplication()
    {
        // ������ ����� ���¿����� ���� ����
        Debug.Log("���� �����!");
        Application.Quit();

        // �����Ϳ��� ���� ���� ���� ������� ������,
        // ���� �α׸� �ֿܼ� ����ϴ� ������� Ȯ�� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
