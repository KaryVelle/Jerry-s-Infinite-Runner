using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable Objects/Obstacle")]
public class Obstacle : ScriptableObject
{
    public List<Sprite> sprites;
    public Vector3 posToSpawn;
    public ObjectType obstacleType;
    
    public enum ObjectType
    {
        Jump,
        Crouch,
        ChangeSides
    }
}
