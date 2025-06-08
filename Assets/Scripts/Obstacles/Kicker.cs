using UnityEngine;
using System.Collections;

public class Kicker : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Kick"); // Space를 누르면 킥 애니메이션 재생
        }
    }
}
