using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float speed = 20f;

    void Start()
    {
        // Se autodestruye tras 5 segundos
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);

            // Calculamos bonus por minutos vividos
            int minutes = Mathf.FloorToInt((Time.time - ValorsGlobals.startTime) / 60f);
            int extra = minutes * 5;
            int pointsToAdd = 10 + extra;

            ValorsGlobals.score += pointsToAdd;
            Destroy(gameObject);
        }
    }
}