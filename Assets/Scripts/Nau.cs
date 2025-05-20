using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Nau : MonoBehaviour
{
    private float velNau;
    private float lastRegenTime;
    private const float REGEN_INTERVAL = 60f;
    private const float Limit_dret = 16f;
    private const float Limit_esquerra = -16f;
    private const float Limit_inferior = -9.5f;
    private const float Limit_superior = 6.7f;
    public const int MAX_ESCUTS = 3;

    [Header("UI Elements")]
    public TextMeshProUGUI textEscuts;
    public TextMeshProUGUI textTiempo;
    public TextMeshProUGUI textScore;

    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 190f;

    [Header("Jefe")]
    public GameObject bossObject; // ← Referencia desde el inspector

    private bool bossSpawned = false;

    void Start()
    {
        velNau = 15f;
        ValorsGlobals.numeroEscuts = MAX_ESCUTS;
        ValorsGlobals.score = 0;
        ValorsGlobals.startTime = Time.time;
        lastRegenTime = Time.time;

        textEscuts.text = "Escuts: " + ValorsGlobals.numeroEscuts;
        textTiempo.text = "Tiempo: 00:00";
        textScore.text = "Puntuación: 0";
    }

    void Update()
    {
        Movimentnau();
        ControlLimitsPantalla();
        RegenerateShield();
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();

        // Activar jefe cuando se llega a 100 puntos
        if (!bossSpawned && ValorsGlobals.score >= 100)
        {
            if (bossObject != null)
            {
                bossObject.transform.position = new Vector3(0f, 0f, 100f);
                bossObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("El objeto del jefe no está asignado en el Inspector.");
            }

            bossSpawned = true;
        }
    }

    void UpdateUI()
    {
        float t = Time.time - ValorsGlobals.startTime;
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        textTiempo.text = string.Format("Tiempo: {0:00}:{1:00}", minutes, seconds);
        textScore.text = "Puntuación: " + ValorsGlobals.score;
    }

    void RegenerateShield()
    {
        if (Time.time - lastRegenTime >= REGEN_INTERVAL)
        {
            if (ValorsGlobals.numeroEscuts < MAX_ESCUTS)
            {
                ValorsGlobals.numeroEscuts++;
                textEscuts.text = "Escuts: " + ValorsGlobals.numeroEscuts;
            }
            lastRegenTime += REGEN_INTERVAL;
        }
    }

    void Movimentnau()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v, 0).normalized;
        transform.position += dir * velNau * Time.deltaTime;
    }

    void ControlLimitsPantalla()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, Limit_esquerra, Limit_dret);
        pos.y = Mathf.Clamp(pos.y, Limit_inferior, Limit_superior);
        transform.position = pos;
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile p = proj.GetComponent<Projectile>();
            if (p != null) p.speed = projectileSpeed;
        }
        else Debug.LogWarning("ProjectilePrefab o FirePoint no asignados.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("COLISIÓN con: " + other.name + " | escudos: " + ValorsGlobals.numeroEscuts);
            if (ValorsGlobals.numeroEscuts > 0)
            {
                ValorsGlobals.numeroEscuts--;
                textEscuts.text = "Escuts: " + ValorsGlobals.numeroEscuts;
            }
            else
            {
                ValorsGlobals.timeAlive = Time.time - ValorsGlobals.startTime;
                ValorsGlobals.highScores.Add(new HighScoreEntry(ValorsGlobals.playerName, ValorsGlobals.score));
                ValorsGlobals.highScores.Sort((a, b) => b.score.CompareTo(a.score));
                if (ValorsGlobals.highScores.Count > 4)
                    ValorsGlobals.highScores.RemoveRange(4, ValorsGlobals.highScores.Count - 4);

                SceneManager.LoadScene("Escena resultats");
            }
        }
    }
}
