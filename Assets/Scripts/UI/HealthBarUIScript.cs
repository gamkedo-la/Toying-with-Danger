using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIScript : MonoBehaviour
{

    [SerializeField] Image fillBar;
    HealthComponent healthComponent;
    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponentInParent<HealthComponent>();
        if(healthComponent is null){
            Debug.LogWarning($"{transform.name} has a HealthBarUIScript but cannot find HealthComponent in parent.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = healthComponent.HealthFraction;
    }
}
