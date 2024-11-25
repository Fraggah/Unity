using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightTransition : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Color targetColor = Color.red; 
    [SerializeField] private float targetIntensity = 0.5f;  

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 10f; 
    [SerializeField] private float waitTimeBeforeTransition = 30f; 

    private Light2D globalLight; 

    private void Start()
    {
        globalLight = FindObjectOfType<Light2D>();

        if (globalLight == null)
        {
            Debug.LogError("falta luz 2D.");
            return;
        }

        StartCoroutine(ChangeLightAfterWait());
    }

    private IEnumerator ChangeLightAfterWait()
    {
        yield return new WaitForSeconds(waitTimeBeforeTransition);

        StartCoroutine(ChangeLightColorAndIntensity());
    }

    private IEnumerator ChangeLightColorAndIntensity()
    {

        Color initialColor = globalLight.color;
        float initialIntensity = globalLight.intensity;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            globalLight.color = Color.Lerp(initialColor, targetColor, t);
            globalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, t);

            yield return null;
        }

        globalLight.color = targetColor;
        globalLight.intensity = targetIntensity;
    }
}
