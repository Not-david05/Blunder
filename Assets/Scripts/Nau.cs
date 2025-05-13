using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Nau : MonoBehaviour
{
    private float velNau;

    [Header("Límites de la pantalla")]
    private const float Limit_dret = 16f;
    private const float Limit_esquerra = -16f;
    private const float Limit_inferior = -9.5f;
    private const float Limit_superior = 6.7f;

    [Header("UI Elements")]
    public TextMeshProUGUI textEscuts;

    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;

    void Start()
    {
        velNau = 10f;
        ValorsGlobals.numeroEscuts = 3;

        textEscuts.text = "Escuts: " + ValorsGlobals.numeroEscuts;
    }

    void Update()
    {
        Movimentnau();
        ControlLimitsPantalla();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void ControlLimitsPantalla()
    {
        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, Limit_esquerra, Limit_dret);
        pos.y = Mathf.Clamp(pos.y, Limit_inferior, Limit_superior);
        transform.position = pos;
    }

    void Movimentnau()
    {
        float movimentHoritzontal = Input.GetAxisRaw("Horizontal");
        float movimentVertical = Input.GetAxisRaw("Vertical");
        Vector3 vectorDireccio = new Vector3(movimentHoritzontal, movimentVertical, 0).normalized;
        Vector3 nouDesplazament = vectorDireccio * velNau * Time.deltaTime;
        transform.position += nouDesplazament;
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile p = proj.GetComponent<Projectile>();
            if (p != null)
                p.speed = projectileSpeed;
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab o FirePoint no asignados en el Inspector.");
        }
    }

    public void UpdateShieldUI()
    {
        textEscuts.text = "Escuts: " + ValorsGlobals.numeroEscuts;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if (ValorsGlobals.numeroEscuts > 0)
            {
                ValorsGlobals.numeroEscuts--;
                UpdateShieldUI();
            }
            else
            {
                Debug.Log("Nau Destruida");
                SceneManager.LoadScene("Escena resultats");
            }
        }
    }
}
