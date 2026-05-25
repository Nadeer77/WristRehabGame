using UnityEngine;

public class KnifeInput : MonoBehaviour
{
    private Animator animator;

    private bool hasPushedSlices = false;

    private VegetableSliceController cachedVeg;

    public AudioSource audioSource;

    public AudioClip knifeCutSFX;
    public AudioClip vesselSFX;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Cut();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            ReturnUp();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            PushToVessel();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            ReturnAndSpawn();
    }

    // =====================================
    // CUT
    // =====================================
    public void Cut()
    {
        if (hasPushedSlices)
            return;

        VegetableSliceController currentVeg =
            FindFirstObjectByType<VegetableSliceController>();

        animator.SetBool("isCutting", true);

        if (currentVeg != null)
        {
            currentVeg.CutSlice();

            if (audioSource != null && knifeCutSFX != null)
                audioSource.PlayOneShot(knifeCutSFX);
        }
    }

    // =====================================
    // KNIFE UP
    // =====================================
    public void ReturnUp()
    {
        if (hasPushedSlices)
            return;

        animator.SetBool("isCutting", false);
    }

    // =====================================
    // PUSH TO VESSEL
    // =====================================
    public void PushToVessel()
    {
        if (hasPushedSlices)
            return;

        VegetableSliceController currentVeg =
            FindFirstObjectByType<VegetableSliceController>();

        animator.SetTrigger("Push");

        if (currentVeg != null)
        {
            cachedVeg = currentVeg;

            Invoke(nameof(MoveSlicesWithDelay), 0.45f);
        }

        hasPushedSlices = true;
    }

    // =====================================
    // RETURN LEFT + SPAWN
    // =====================================
    public void ReturnAndSpawn()
    {
        if (!hasPushedSlices)
            return;

        animator.SetTrigger("ReturnLeft");

        Invoke(nameof(SpawnNextVegetable), 0.5f);

        hasPushedSlices = false;
    }

    // =====================================
    // MOVE SLICES AFTER PUSH
    // =====================================
    void MoveSlicesWithDelay()
    {
        if (cachedVeg != null)
        {
            cachedVeg.MoveSlicesToVessel();

            if (audioSource != null && vesselSFX != null)
                audioSource.PlayOneShot(vesselSFX);
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