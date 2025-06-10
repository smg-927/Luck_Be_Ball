using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 꼭 필요합니다!

public class TextChange : MonoBehaviour
{
    // 인스펙터 창에서 텍스트 UI 오브젝트를 이 변수에 연결해줘야 합니다.
    public TextMeshProUGUI displayText;

    // 첫 번째로 보여줄 텍스트
    private string text1 = "A ball that was meant to be born as a pinball, but when it opened its eyes... it found itself a rugby ball.";

    // 두 번째로 보여줄 텍스트
    private string text2 = "Despite fate’s cruel joke, it shouts out loud — ‘I can be a pinball too!";

    // 대기할 시간 (초 단위)
    private float waitTime = 6f;

    // 게임이 시작될 때 딱 한 번 호출되는 함수입니다.
    void Start()
    {
        // 텍스트를 순서대로 바꾸는 코루틴을 시작합니다.
        StartCoroutine(ChangeTextSequence());
    }

    // 텍스트를 순서대로 변경하는 코루틴 함수
    IEnumerator ChangeTextSequence()
    {
        // 1. 첫 번째 텍스트를 표시합니다.
        displayText.text = text1;

        // 2. 지정된 시간(6초)만큼 기다립니다. 이 부분이 게임을 멈추지 않고 기다리게 해줍니다.
        yield return new WaitForSeconds(waitTime);

        // 3. 6초가 지난 후, 두 번째 텍스트로 변경합니다.
        displayText.text = text2;
    }
}