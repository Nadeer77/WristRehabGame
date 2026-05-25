using UnityEngine;

public class VegetableSpawner : MonoBehaviour
{
    public GameObject bananaPrefab;
    public GameObject carrotPrefab;

    public Transform spawnPoint;

    private GameObject currentVegetable;
    private bool spawnBanana = true;

    public static VegetableSpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnVegetable();
    }

    public void SpawnVegetable()
    {
        // DO NOT DESTROY OLD VEGETABLE
        // Just keep it in vessel

        if (spawnBanana)
        {
            currentVegetable = Instantiate(
                bananaPrefab,
                spawnPoint.position,
                Quaternion.identity
            );
        }
        else
        {
            currentVegetable = Instantiate(
                carrotPrefab,
                spawnPoint.position,
                Quaternion.identity
            );
        }

        spawnBanana = !spawnBanana;
    }
}