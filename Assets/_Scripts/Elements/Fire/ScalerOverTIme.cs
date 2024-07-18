using System.Collections;
using UnityEngine;

public class ScalerOverTIme : MonoBehaviour
{
    private float scaleTime = 1f; // Tiempo en segundos para escalar

    private void Start()
    {
        ScaleTo(new Vector3(40f, 40f, 40f), scaleTime);
    }

    private void ScaleTo(Vector3 targetScale, float time)
    {
        StartCoroutine(ScaleOverTime(targetScale, time));
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale, float time)
    {
        Vector3 originalScale = transform.localScale;
        float currentTime = 0f;

        while (currentTime <= time)
        {
            float t = currentTime / time;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Asegurarse de que termine en el escala objetivo exacta
    }
}
