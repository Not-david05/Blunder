using UnityEngine;

public class GeneradorObstacle : MonoBehaviour
{
    private const float Limit_Anterior = 144f; // Eix z.
    private const float Limit_dret = 16f; // Eix x.
    private const float Limit_esquerra = -16f; // Eix z.
    private const float Limit_inferior = -9.5f; // Eix y.
    private const float Limit_superior = 6.7f; // Eix y.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject obstacle;
    void Start()
    {
        InvokeRepeating("Obstacle", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Obstacle()
    {
        GameObject Pobstacle = Instantiate(obstacle);
        Pobstacle.transform.position = new Vector3(Random.Range(Limit_esquerra,Limit_dret),Random.Range(Limit_inferior,Limit_superior),Limit_Anterior);
    }
}
