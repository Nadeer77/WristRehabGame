using UnityEngine;

public class KnifeInput : MonoBehaviour
{
    private Animator animator;

    // TRUE after slices pushed
    private bool hasPushedSlices = false;

    // STORE CURRENT VEGETABLE
    private VegetableSliceController cachedVeg;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        VegetableSliceController currentVeg =
            FindFirstObjectByType<VegetableSliceController>();

        // =====================================
        // DOWN ARROW = CUT
        // =====================================
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

        // =====================================
        // UP ARROW = RETURN KNIFE UP
        // =====================================
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!hasPushedSlices)
            {
                animator.SetBool("isCutting", false);
            }
        }

        // =====================================
        // RIGHT ARROW = PUSH TO VESSEL
        // =====================================
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!hasPushedSlices)
            {
                // PLAY PUSH ANIMATION
                animator.SetTrigger("Push");

                // STORE CURRENT VEGETABLE
                if (currentVeg != null)
                {
                    cachedVeg = currentVeg;

                    // WAIT BEFORE MOVING SLICES
                    Invoke(nameof(MoveSlicesWithDelay), 0.45f);
                }

                hasPushedSlices = true;
            }
        }

        // =====================================
        // LEFT ARROW = RETURN LEFT + SPAWN NEXT
        // =====================================
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (hasPushedSlices)
            {
                // PLAY RETURN ANIMATION
                animator.SetTrigger("ReturnLeft");

                // SPAWN NEXT VEGETABLE AFTER DELAY
                Invoke(nameof(SpawnNextVegetable), 0.5f);

                hasPushedSlices = false;
            }
        }
    }

    // =====================================
    // MOVE SLICES AFTER PUSH ANIMATION
    // =====================================
    void MoveSlicesWithDelay()
    {
        if (cachedVeg != null)
        {
            cachedVeg.MoveSlicesToVessel();
        }
    }

    // =====================================
    // SPAWN NEXT VEGETABLE
    // =====================================
    void SpawnNextVegetable()
    {
        VegetableSpawner.Instance.SpawnVegetable();
    }
}