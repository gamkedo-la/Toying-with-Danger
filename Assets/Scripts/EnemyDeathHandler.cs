using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemyDeathHandler : MonoBehaviour
{
    public GameObject blockEnemyDeathPrefab;
    public GameObject teddyBearEnemyDeathPrefab;

    private void OnEnable()
    {
        EventManagerScript.EnemyGotDestroyedEvent += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EventManagerScript.EnemyGotDestroyedEvent -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        // TODO: Should only execute when enemy is killed by tower, not by reaching bed
        // If x or z is within -1.5 to 1.5, then it's a bed kill and should be ignored
        if (enemy.transform.position.x > -1.5f && enemy.transform.position.x < 1.5f)
        {
            return;
        }
        if (enemy.transform.position.z > -1.5f && enemy.transform.position.z < 1.5f)
        {
            return;
        }
        //EnemyBlock(Clone)
        //EnemyTeddyBear(Clone)
        GameObject effect;
        if (enemy.name == "EnemyBlock(Clone)")
        {
            effect = Instantiate(blockEnemyDeathPrefab, enemy.transform.position, enemy.transform.rotation);
            AudioManagerScript.Instance.PlaySfx("LegoEnemyDeath");
        }
        else // if (enemy.name == "EnemyTeddyBear(Clone)")
        {
            effect = Instantiate(teddyBearEnemyDeathPrefab, enemy.transform.position, enemy.transform.rotation);
            AudioManagerScript.Instance.PlaySfx("BearEnemyDeath");
        }
        StartCoroutine(WaitToRemoveParticleSystem(effect));
    }

    private IEnumerator WaitToRemoveParticleSystem(GameObject effect)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
    }

}