using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject obstaclePrefab;
    public Transform firePoint;
    public float fireInterval = 2f;

    public Slider healthBar;

    private float nextFireTime;

    private enum BossState { Idle, Approaching, Retreating }
    private BossState currentState = BossState.Idle;

    private float approachTimer = 0f;
    private float approachCooldown = 10f;
    private Vector3 originalPosition;
    private Vector3 approachTarget;
    private float movementSpeed = 5f;

    void Start()
    {
        currentHealth = maxHealth;
        originalPosition = transform.position;
        approachTarget = originalPosition + Vector3.back * 20f;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        HandleMovement();

        if (Time.time >= nextFireTime)
        {
            FireObstacleWall();
            nextFireTime = Time.time + fireInterval;
        }

        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    void HandleMovement()
    {
        approachTimer += Time.deltaTime;

        if (currentState == BossState.Idle && approachTimer >= approachCooldown)
        {
            currentState = BossState.Approaching;
            approachTimer = 0f;
        }

        if (currentState == BossState.Approaching)
        {
            transform.position = Vector3.MoveTowards(transform.position, approachTarget, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, approachTarget) < 0.1f)
            {
                currentState = BossState.Retreating;
            }
        }

        if (currentState == BossState.Retreating)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                currentState = BossState.Idle;
                approachTimer = 0f;
            }
        }
    }

    void FireObstacleWall()
    {
        for (int i = -2; i <= 2; i++)
        {
            Vector3 pos = firePoint.position + new Vector3(i * 2f, 0f, 0f);
            Instantiate(obstaclePrefab, pos, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(2);
            Destroy(other.gameObject);
        }
    }
}
