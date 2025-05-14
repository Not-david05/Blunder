using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private const float Limit_Z = -10f;
    private float baseSpeed = -15f;
    private float increaseRate = 0.02f; // incremento por segundo

    void Update()
    {
        // Aumenta velocidad con el tiempo de juego
        float t = Time.time - ValorsGlobals.startTime;
        float vel = baseSpeed * (1f + increaseRate * t);

        transform.position += new Vector3(0f, 0f, vel * Time.deltaTime);
        if (transform.position.z < Limit_Z) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nau")) Destroy(gameObject);
    }
}
