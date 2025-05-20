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

    private Vector3 originalPosition;
    private float originalX;
    private float movementSpeed = 5f;

    private enum BossState { Idle, Approaching, Retreating }
    private BossState currentState = BossState.Idle;

    private float approachTimer = 0f;
    private float approachCooldown = 10f;
    private Vector3 approachTarget;

    // Nueva lógica de escape por daño y retorno por puntos
    private float damageThreshold = 0.95f; // cada 5% de daño
    private float scoreThreshold = 100f;   // puntos necesarios para que vuelva
    private float scoreMultiplier = 1.5f;
    private float lastScoreAtReturn = 0f;
    private bool isTemporarilyGone = false;
    private bool hasEnteredCombat = false;


void Start()
{
    currentHealth = maxHealth;
    transform.position = new Vector3(0f, 0f, 40f); // Posición de aparición forzada
    originalPosition = transform.position;
    originalX = transform.position.x;
    approachTarget = originalPosition + Vector3.forward * 5f;

    if (healthBar != null)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    hasEnteredCombat = false;
}


void Update()
{
    if (isTemporarilyGone)
    {
        if (ValorsGlobals.score >= lastScoreAtReturn + scoreThreshold)
        {
            isTemporarilyGone = false;
            scoreThreshold *= scoreMultiplier;
            lastScoreAtReturn = ValorsGlobals.score;

            // Volver al centro
            transform.position = new Vector3(originalX, transform.position.y, originalPosition.z);
            currentState = BossState.Idle;
            approachTimer = 0f;
            hasEnteredCombat = true;
        }

        return;
    }

    if (hasEnteredCombat)
    {
        HandleMovement();
    }

    if (Time.time >= nextFireTime && !isTemporarilyGone)
    {
        FireObstacleWall();
        nextFireTime = Time.time + fireInterval;
    }

    if (healthBar != null)
        healthBar.value = currentHealth;
}


    void HandleMovement()
    {
        switch (currentState)
        {
            case BossState.Idle:
                approachTimer += Time.deltaTime;
                if (approachTimer >= approachCooldown)
                {
                    currentState = BossState.Approaching;
                    approachTimer = 0f;
                }
                break;

            case BossState.Approaching:
                transform.position = Vector3.MoveTowards(transform.position, approachTarget, movementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, approachTarget) < 0.1f)
                {
                    currentState = BossState.Retreating;
                }
                break;

            case BossState.Retreating:
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, movementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
                {
                    currentState = BossState.Idle;
                    approachTimer = 0f;
                }
                break;
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

        float percent = (float)currentHealth / maxHealth;

        if (percent <= damageThreshold && currentHealth > 0)
        {
            damageThreshold -= 0.05f;
            EscapeTemporarily();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EscapeTemporarily()
    {
        isTemporarilyGone = true;

        // Se teletransporta rápidamente fuera del alcance del jugador (X = ±45)
        float newX = (Random.value < 0.5f) ? 45f : -45f;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
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
