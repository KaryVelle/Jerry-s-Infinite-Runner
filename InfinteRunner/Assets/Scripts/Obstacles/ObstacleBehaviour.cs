using System;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleBehaviour : MonoBehaviour
    {
        [SerializeField] public Obstacle obstacle;
        [SerializeField] private bool isDown;
        private SpriteRenderer sr;

        public static Action<Obstacle, GameObject> OnTurnOn;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            OnTurnOn?.Invoke(obstacle, gameObject);
            var randomInt = Random.Range(0, obstacle.sprites.Count);
            sr.sprite = obstacle.sprites[randomInt];
            var tempPosy = obstacle.posToSpawn.y;
            if (isDown)
            {
                tempPosy *= -1;
            }
            transform.position = new Vector3(obstacle.posToSpawn.x,tempPosy, obstacle.posToSpawn.z );
        }
    }
}