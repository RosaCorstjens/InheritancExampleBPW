using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOverTimeGroup : SpawnerOverTime
{
    [SerializeField] private int minAmount = 20;
    [SerializeField] private int maxAmount = 100;

    // current amount is set by GameManager
    internal int currentAmount;

    protected override IEnumerator SpawnRoutine()
    {
        // don't call base, it's too different

        while (true)
        {
            // spawn until we have the min amount
            while(currentAmount < minAmount)
            {
                Spawn();
            }

            // keep spawning on intervals until we reach the max amount
            if(currentAmount < maxAmount)
            {
                yield return new WaitForSeconds(spawnEvery);
                Spawn();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected override GameObject Spawn()
    {
        currentAmount++;
        return base.Spawn();
    }
}
