using UnityEngine;
using System.Collections.Generic;

public class VegetableSliceController : MonoBehaviour
{
    // ALL SLICES
    public List<Transform> slices;

    // STACK AREA
    public Transform stackPoint;

    // VESSEL AREA
    private Transform vesselPoint;

    // CURRENT CUT INDEX
    private int currentSlice = 0;

    // CUT SLICES
    private List<Transform> movedSlices = new List<Transform>();

    // STACK SPACING
    public float spacing = 0.12f;

    // HOW MUCH BANANA MOVES EACH CUT
    public float moveAmount = 0.05f;

    void Start()
    {
        vesselPoint = GameObject.FindGameObjectWithTag("VesselPoint").transform;
    }

    // =========================
    // CUT
    // =========================
    public void CutSlice()
    {
        if (currentSlice >= slices.Count)
            return;

        Transform slice = slices[currentSlice];

        // REMOVE FROM BANANA BODY
        slice.SetParent(null);

        // STACK POSITION
        int column = currentSlice % 3;
        int row = currentSlice / 3;

        float xOffset = column * spacing;
        float zOffset = row * spacing;

        slice.position =
            stackPoint.position +
            new Vector3(xOffset, 0, zOffset);

        movedSlices.Add(slice);

        currentSlice++;

        // MOVE ENTIRE BANANA BODY
        transform.position += Vector3.left * 0.04f;
    }

    // =========================
    // MOVE TO VESSEL
    // =========================
    public void MoveSlicesToVessel()
    {
        for (int i = 0; i < movedSlices.Count; i++)
        {
            movedSlices[i].position =
                vesselPoint.position +
                new Vector3(
                    Random.Range(-0.1f, 0.1f),
                    i * 0.02f,
                    Random.Range(-0.1f, 0.1f)
                );
        }
    }
}