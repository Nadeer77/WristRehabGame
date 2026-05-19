using UnityEngine;

public class KnifeInput : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("isCutting", true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("isCutting", false);
        }
    }
}