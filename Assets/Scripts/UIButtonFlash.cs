using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonFlash : MonoBehaviour
{
    private Image image;

    private Color normalColor;
    private Color pressedColor;

    void Start()
    {
        image = GetComponent<Image>();

        normalColor = Color.white;

        pressedColor = new Color(
            0.7f,
            0.7f,
            0.7f,
            1f
        );
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        image.color = pressedColor;

        yield return new WaitForSeconds(0.15f);

        image.color = normalColor;
    }
}