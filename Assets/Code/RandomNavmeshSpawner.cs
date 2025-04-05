using UnityEngine;
using UnityEngine.AI;

public class RandomNavmeshSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] float spawnRadius = 1000;
    [SerializeField] int maxTries = 300;

    [SerializeField] int number_to_spawn = 5000;

    void Start()
    {
        for (int i = 0; i < number_to_spawn; i++)
            SpawnAtRandomLocation();
    }

    void SpawnAtRandomLocation()
    {
        if (GetRandomPointOnNavMesh(transform.position, spawnRadius, out Vector3 randomPoint))
        {
            Instantiate(prefabToSpawn, randomPoint + new Vector3(0,1,0), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Could not find a valid point on the NavMesh.");
        }
    }

    bool GetRandomPointOnNavMesh(Vector3 center, float radius, out Vector3 result)
    {
        for (int i = 0; i < maxTries; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }
}
