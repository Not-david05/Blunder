using UnityEngine;



public class Projectile : MonoBehaviour
{
    // Velocidad de la bala (en X o Z seg�n tu escena)
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
            // Destruye el obst�culo
            Destroy(other.gameObject);
            // A�ade puntos
            ValorsGlobals.score += 10;
            
            // Destruye el proyectil
            Destroy(gameObject);
        }
    }
}