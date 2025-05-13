using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private float vel;          // Velocidad en Z
    private const float Limit_Z = -10f;  // Límite en Z (ajusta al valor que necesites)

    void Start()
    {
        vel = -15f;  // negativo para “hacia atrás” (eje Z negativo)
    }

    void Update()
    {
        // Desplazamos sólo en Z:
        float desplazamientoZ = vel * Time.deltaTime;
        transform.position += new Vector3(0f, 0f, desplazamientoZ);

        // Si cruza el límite en Z, destruimos el objeto
        if (transform.position.z < Limit_Z)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nau")
        {
            Destroy(gameObject);
        }
    }
}
