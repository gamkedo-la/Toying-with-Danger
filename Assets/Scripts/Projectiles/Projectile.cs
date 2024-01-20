using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    #region Events
    public delegate void ProjectileDestroyed(GameObject gameObject);
    public event ProjectileDestroyed ProjectileDestroyedEvent;

    public delegate void SpeedChanged(GameObject gameObject);
    public event SpeedChanged SpeedChangedEvent;

    public delegate void DirectionChanged(GameObject gameObject);
    public event DirectionChanged DirectionChangedEvent;
    #endregion
    [SerializeField]
    Rigidbody rigidbody;

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
            SpeedChangedEvent(gameObject);
        }
    }
    float speed = 1f;

    public Quaternion ProjectileDirection
    {
        get
        {
            return projectileDirection;
        }
        set
        {
            projectileDirection = value;
            rigidbody.velocity = transform.forward * Speed;
        }
    }
    Quaternion projectileDirection;

    Quaternion lastDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = false;

        rigidbody.velocity = transform.forward*speed;

    }

    // Update is called once per frame
    void Update()
    {
        //if the rotation of the object ever changes update it's velocity with new direction
        if(lastDirection != gameObject.transform.rotation)
        {
            ProjectileDirection = gameObject.transform.rotation;
            lastDirection = gameObject.transform.rotation;
        }
    }
    /// <summary>
    /// This block will handle the logic of when the projectile collides with something
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (ProjectileDestroyedEvent == null)
            Debug.LogWarning($"Nothing subscribed to ProjectileDestroyedEvent on : {gameObject.name}");
        else
            ProjectileDestroyedEvent(gameObject);
        Destroy(gameObject);
    }
    
}
