using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float chargeSpeed = 3f;
    public float returnSpeed = 10f;
    public float minScaleY = 0.5f;

    private Vector3 basePosition;
    private Vector3 baseScale;
    private float currentScaleY;
    private bool isCharging = false;
    private float originalHeight;

    void Start()
    {
        basePosition = transform.localPosition;
        baseScale = transform.localScale;
        currentScaleY = baseScale.y;
        originalHeight = baseScale.y;
    }

    void Update()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;

        basePosition.x += horizontalInput * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            isCharging = true;
        if (Input.GetKeyUp(KeyCode.DownArrow))
            isCharging = false;

        if (isCharging)
        {
            currentScaleY -= chargeSpeed * Time.deltaTime;
            if (currentScaleY < minScaleY)
                currentScaleY = minScaleY;
        }
        else
        {
            currentScaleY += returnSpeed * Time.deltaTime;
            if (currentScaleY > baseScale.y)
                currentScaleY = baseScale.y;
        }

        float heightDiff = originalHeight - (originalHeight * currentScaleY);
        Vector3 pos = basePosition;
        pos.y -= heightDiff / 2f;  // 아래 방향으로 위치 보정
        transform.localPosition = pos;

        transform.localScale = new Vector3(baseScale.x, currentScaleY, baseScale.z);
    }
}
