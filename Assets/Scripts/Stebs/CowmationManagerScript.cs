using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowmationManagerScript : MonoBehaviour
{
    private GameObject cowmationSpawnPoint;
    [SerializeField] GameObject cowmationPrefab;

    private float timer = 0.0f;

    private void Awake()
    {
        cowmationSpawnPoint = GameObject.FindGameObjectWithTag("CowmationSpawnPoint");
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cowmationPrefab);
        cowmationPrefab.transform.position = cowmationSpawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Spawn_A_Cowmation_Intermittently();
    }

    private void Spawn_A_Cowmation_Intermittently()
    {
        if (timer >= GameManagerScript.GameManagerScriptInstance.cowmationSpawnInterval)
        {
            Instantiate(cowmationPrefab);

            timer = 0;
        }
    }
}
