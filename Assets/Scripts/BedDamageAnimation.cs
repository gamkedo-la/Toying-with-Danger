using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedDamageAnimation : MonoBehaviour
{
    // Note: May want to combine with BedDestinationScript depending on if more functionality is added there

    public Renderer bed;
    private Material bedMaterial;
    public Color damageColor;

    #region event subscriptions
    private void OnEnable()
    {
        EventManagerScript.ToyReachedBedEvent += HandleTakeDamage;
    }

    private void OnDisable()
    {
        EventManagerScript.ToyReachedBedEvent -= HandleTakeDamage;
    }
    #endregion

    private void Start()
    {
        // Create a copy of material and assign copy back to renderer so that material changes during runtime aren't saved
        bedMaterial = new Material(bed.material);
        bed.material = bedMaterial;
    }

//#if UNITY_EDITOR
//    private void Update()
//    {
//        // For testing purposes - will remove later
//        if (Input.GetKeyDown(KeyCode.T))
//        {
//            HandleTakeDamage(this.gameObject);
//        }
//    }
//#endif

    public void HandleTakeDamage(GameObject toy)
    {
        // Stop any ongoing animation and start a new one
        StopCoroutine("AnimateBed");
        StartCoroutine("AnimateBed");
    }

    // Smooth/gradient animation
    //private IEnumerator AnimateBed()
    //{
    //    float flashDuration = 0.5f; // Time in seconds
    //    float animationSpeedScale = 10f; // Controls how rapidly the color changes
    //    float animationTimeStep = 0.0f;

    //    while (animationTimeStep < flashDuration)
    //    {
    //        bedMaterial.color = Color.Lerp(Color.white, damageColor, Mathf.PingPong(animationTimeStep * animationSpeedScale, 1f));
    //        animationTimeStep += Time.deltaTime;
    //        yield return null;
    //    }

    //    bedMaterial.color = Color.white;
    //}

    // Flashing animation
    private IEnumerator AnimateBed()
    {
        int animationCount = 3;
        float animationSpeed = 0.1f;

        for (int i = 0; i < animationCount; i++)
        {
            bedMaterial.color = damageColor;
            yield return new WaitForSeconds(animationSpeed);
            bedMaterial.color = Color.white;
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}