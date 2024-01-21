using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class ProjectileImpactInitializer : MonoBehaviour
{
    Projectile projectile;
    [SerializeField]
    GameObject impactObject;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ProjectileDestroyedEvent += InstantiateEffect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateEffect(GameObject parent)
    {
        Instantiate(impactObject,parent.transform.position,Quaternion.identity);
    }
}
