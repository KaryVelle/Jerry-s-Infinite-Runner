using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 0.5f;
    [SerializeField] private float textureUnitSizeX;
    [SerializeField] private float sceneSpeed = 5f; 

    [SerializeField]private Vector3 startPosition;
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        textureUnitSizeX = spriteRenderer.bounds.size.x;
    }
    
    private void OnEnable()
    {
        ScoreKeeper.OnScoreThresholdReached += IncreaseSpeed;
    }

    private void OnDisable()
    {
        ScoreKeeper.OnScoreThresholdReached -= IncreaseSpeed;
    }

    private void IncreaseSpeed()
    {
        sceneSpeed += 0.5f;
    }

    private void Update()
    {
        transform.position -= new Vector3(sceneSpeed * parallaxSpeed * Time.deltaTime, 0, 0);
        float distanceMoved = Mathf.Abs(transform.position.x - startPosition.x);
        if (distanceMoved >= textureUnitSizeX)
        {
            transform.position = startPosition;
        }
    }
}