using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    public Material flashMaterial;
    public float duration;

    private SpriteRenderer sr;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        originalMaterial = sr.material;
    }

    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        sr.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        sr.material = originalMaterial;

        flashRoutine = null;
    }
}
