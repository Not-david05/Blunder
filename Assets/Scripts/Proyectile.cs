using UnityEngine;



public class Projectile : MonoBehaviour
{
    // Velocidad de la bala (en X o Z según tu escena)
    [HideInInspector]
    public float speed = 20f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Destruye el obstáculo
            Destroy(other.gameObject);
            // Añade puntos
            ValorsGlobals.score += 10;
            
            // Destruye el proyectil
            Destroy(gameObject);
        }
    }
}