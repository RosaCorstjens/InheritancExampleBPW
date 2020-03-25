using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private BoxCollider spawnArea;
    [SerializeField] private Transform spawnParent;

    protected virtual void Start()
    {
        GameManager.Instance.spawners.Add(this);
    }

    public abstract void StartLevel();

    public abstract void EndLevel();

    protected virtual GameObject Spawn()
    {
        GameObject go = GameObject.Instantiate(spawnPrefab);
        go.transform.position = RandomSpawnPoint();
        go.transform.parent = spawnParent;

        return go;
    }

    private Vector3 RandomSpawnPoint()
    {
        return new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            GameManager.Instance.floor.position.y,
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
        );
    }
}
