using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, List<GameObject>> activeObjects = new Dictionary<string, List<GameObject>>();

    [SerializeField] private List<GameObject> prefabsToCreate;
    [SerializeField] private int additionalPoolSize = 2; // Cuántos objetos crear si el pool se queda vacío

    public void RegisterPool(string poolID, List<GameObject> prefabs, int initialSize)
    {
        if (!pools.ContainsKey(poolID))
        {
            pools[poolID] = new Queue<GameObject>();
            activeObjects[poolID] = new List<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = CreateNewObject(poolID, prefabs);
                pools[poolID].Enqueue(obj);
            }
        }
    }

    private GameObject CreateNewObject(string poolID, List<GameObject> prefabs)
    {
        if (prefabs == null || prefabs.Count == 0)
        {
            Debug.LogError($"Error: La lista de prefabs en el pool '{poolID}' está vacía. Verifica que los objetos están asignados en el Inspector.");
            return null;
        }

        int prefabIndex = Random.Range(0, prefabs.Count);
        GameObject obj = Instantiate(prefabs[prefabIndex]);
        obj.SetActive(false);
        return obj;
    }


    public GameObject GetObject(string poolID)
    {
        if (!pools.ContainsKey(poolID))
        {
            Debug.LogWarning($"El pool con ID '{poolID}' no existe.");
            return null;
        }

        if (pools[poolID].Count == 0)
        {
            Debug.Log($"El pool '{poolID}' se ha quedado sin objetos. Creando {additionalPoolSize} más.");
            for (int i = 0; i < additionalPoolSize; i++)
            {
                GameObject newObj = CreateNewObject(poolID, prefabsToCreate);
                pools[poolID].Enqueue(newObj);
            }
        }

        GameObject obj = pools[poolID].Dequeue();
        obj.SetActive(true);
        activeObjects[poolID].Add(obj);
        return obj;
    }

    public void ReturnObject(string poolID, GameObject obj)
    {
        if (!pools.ContainsKey(poolID))
        {
            Debug.LogWarning($"Intento de regresar un objeto a un pool inexistente: '{poolID}'.");
            return;
        }

        obj.SetActive(false);
        activeObjects[poolID].Remove(obj);
        pools[poolID].Enqueue(obj);
    }
}
