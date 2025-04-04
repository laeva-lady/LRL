using UnityEngine;

//* This is ONLY for particule systems

public class KillAfterSeconds : MonoBehaviour
{
    [SerializeField] float durationMax = 6f;
    [SerializeField] float fadeDuration = 2f; // Time taken to fade out
    private ParticleSystem ps;
    private float timeSpent = 0;
    private bool isFading = false;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        if (timeSpent >= (durationMax - fadeDuration) && !isFading)
        {
            StartCoroutine(FadeOutAndDestroy());
            isFading = true;
        }
    }

    private System.Collections.IEnumerator FadeOutAndDestroy()
    {
        var emission = ps.emission;
        emission.rateOverTime = 0; // Stop new particles

        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting); // Allow existing particles to fade
        yield return new WaitForSeconds(fadeDuration); // Wait for fade out

        Destroy(gameObject);
    }
}
