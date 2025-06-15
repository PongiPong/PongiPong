using UnityEngine;
using TMPro;
using System.Collections;

public class ColorFlashEffect : MonoBehaviour
{
    public float flashSpeed = 0.2f;
    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        while (true)
        {
            textMesh.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}