using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOverTime : AbstractSpawner
{
    [SerializeField] protected float spawnEvery = 5f;

    protected virtual IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnEvery);
            Spawn();         
        }
    }

    public override void StartLevel()
    {
        StartCoroutine(SpawnRoutine());
    }

    public override void EndLevel()
    {
        StopAllCoroutines();
    }
}
