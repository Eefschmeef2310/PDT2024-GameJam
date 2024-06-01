using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Control the movement speed of the enemy.")]
    [SerializeField] private float speed = 5.0f;

    [Tooltip("Control the intensity of the jitter effect.")]
    [SerializeField] private float jitterIntensity = 0.2f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 jitter = new Vector3(
            Random.Range(-jitterIntensity, jitterIntensity),
            Random.Range(-jitterIntensity, jitterIntensity),
            Random.Range(-jitterIntensity, jitterIntensity)
        );

        Vector3 moveDirection = (direction + jitter).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;
        transform.forward = moveDirection;
    }
}
