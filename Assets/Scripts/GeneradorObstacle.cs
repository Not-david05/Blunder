using UnityEngine;

public class GeneradorObstacle : MonoBehaviour
{
    private const float Limit_Anterior = 144f;
    private const float Limit_dret = 16f;
    private const float Limit_esquerra = -16f;
    private const float Limit_inferior = -9.5f;
    private const float Limit_superior = 6.7f;

    public GameObject obstacle;

    private float spawnInterval = 1f;           // Intervalo inicial
    private const float MIN_INTERVAL = 0.2f;     // Intervalo mínimo
    private const float DECREASE_RATE = 0.01f;   // Cuánto se reduce por segundo

    void Start()
    {
        // Inicia la repetición de Obstacle() con el intervalo actual
        InvokeRepeating(nameof(Obstacle), 0f, spawnInterval);
    }

    void Update()
    {
        // Calcula nuevo intervalo según tiempo de juego
        float t = Time.timeSinceLevelLoad;
        float newInterval = Mathf.Max(MIN_INTERVAL, 1f - DECREASE_RATE * t);

        // Si el intervalo ha cambiado de verdad, recarga el InvokeRepeating
        if (Mathf.Abs(newInterval - spawnInterval) > 0.01f)
        {
            spawnInterval = newInterval;
            CancelInvoke(nameof(Obstacle));
            InvokeRepeating(nameof(Obstacle), spawnInterval, spawnInterval);
        }
    }

    private void Obstacle()
    {
        // Genera un obstáculo en posición aleatoria dentro de tus límites
        GameObject pob = Instantiate(obstacle);
        float x = Random.Range(Limit_esquerra, Limit_dret);
        float y = Random.Range(Limit_inferior, Limit_superior);
        pob.transform.position = new Vector3(x, y, Limit_Anterior);
    }
}