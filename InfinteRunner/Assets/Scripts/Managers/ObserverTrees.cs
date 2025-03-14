using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public class ObserverTrees : MonoBehaviour
{
    private Dictionary<Obstacle, GameObject> obstacleDictionary = new Dictionary<Obstacle, GameObject>();

    private void OnEnable()
    {
        ObstacleBehaviour.OnTurnOn += HandleOnTurnOn;
    }

    private void OnDisable()
    {
        ObstacleBehaviour.OnTurnOn -= HandleOnTurnOn;
    }

    private void HandleOnTurnOn(Obstacle obstacle, GameObject obj)
    {
        Debug.Log($"Obstacle activado: {obstacle.name}, Tipo: {obstacle.obstacleType}");

        if (obstacle.obstacleType == Obstacle.ObjectType.ChangeSides)
        {
            // 🛑 Verificar si ya hay un ChangeSides activo
            foreach (var entry in obstacleDictionary)
            {
                if (entry.Key.obstacleType == Obstacle.ObjectType.ChangeSides && entry.Value.activeSelf)
                {
                    Debug.Log($"<color=red>Ya hay uno activo ({entry.Key.name}), desactivando el nuevo ({obstacle.name})</color>");
                    obj.SetActive(false);
                    return; // Salimos sin agregarlo al diccionario
                }
            }
        }

        // 🟢 Si no hay otro ChangeSides activo, lo agregamos al diccionario
        obstacleDictionary[obstacle] = obj;

        // 🗑 Limpiar referencias a objetos desactivados o destruidos
        var keysToRemove = new List<Obstacle>();
        foreach (var entry in obstacleDictionary)
        {
            if (entry.Value == null || !entry.Value.activeSelf)
            {
                keysToRemove.Add(entry.Key);
            }
        }
        foreach (var key in keysToRemove)
        {
            obstacleDictionary.Remove(key);
        }
    }
}