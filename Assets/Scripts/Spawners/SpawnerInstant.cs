using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInstant : AbstractSpawner
{
    [SerializeField] private int spawnAmount = 1;
    private bool spawned = false;

    public override void StartLevel()
    {
        Spawn();
    }

    public override void EndLevel()
    {
        // reset spawned so 
        // we can spawn again on start
        spawned = false;
    }

    protected override GameObject Spawn()
    {
        // only spawn once
        if (spawned)
            return null;

        spawned = true;
        return base.Spawn();
    }
}
