using UnityEngine;

public class KnifeInput : MonoBehaviour
{
    private Animator animator;

    private bool hasPushedSlices = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        VegetableSliceController currentVeg =
            FindFirstObjectByType<VegetableSliceController>();

        // CUTTING
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!hasPushedSlices)
            {
                animator.SetBool("isCutting", true);

                if (currentVeg != null)
                {
                    currentVeg.CutSlice();
                }
            }
        }

        // KNIFE RETURN UP
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!hasPushedSlices)
            {
                animator.SetBool("isCutting", false);
            }
        }

        // PUSH TO VESSEL
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!hasPushedSlices)
            {
                animator.SetBool("isPushing", true);

                if (currentVeg != null)
                {
                    currentVeg.MoveSlicesToVessel();
                }

                hasPushedSlices = true;
            }
        }

        // RETURN LEFT + SPAWN NEXT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (hasPushedSlices)
            {
                animator.SetBool("isPushing", false);

                animator.SetTrigger("ReturnLeft");

                // WAIT A LITTLE BEFORE SPAWNING
                Invoke(nameof(SpawnNextVegetable), 0.5f);

                hasPushedSlices = false;    
            }
        }
    }
    void SpawnNextVegetable()
    {
        VegetableSpawner.Instance.SpawnVegetable();
    }
}