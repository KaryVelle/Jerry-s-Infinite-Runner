using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabsToCreate; // Lista de prefabs
    [SerializeField] private int initialPoolSize;
    private List<GameObject> createdObjects = new List<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabsToCreate[i]);
            obj.SetActive(false);
            createdObjects.Add(obj);
        }
    }

    public GameObject AskForObject()
    {
        List<GameObject> inactiveObjects = createdObjects.FindAll(obj => !obj.activeInHierarchy);
        
        if (inactiveObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveObjects.Count);
            GameObject pooledObject = inactiveObjects[randomIndex];
            SetupObstacle(pooledObject);
            return pooledObject;
        }

        // Si no hay objetos disponibles, crear uno nuevo aleatorio
        int prefabIndex = Random.Range(0, prefabsToCreate.Count);
        GameObject newObject = Instantiate(prefabsToCreate[prefabIndex]);
        createdObjects.Add(newObject);
        SetupObstacle(newObject);
        return newObject;
    }

    public void SetupObstacle(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
