using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nau : MonoBehaviour
{
    private float velNau;
    /*
     Limit_esquerraA=(-16.X),(6,7.y)
     Limit_dretA=(16.X),(6,7.y)
    Limit_esquerraB=(-16.X),(-9.5.y)
    Limit_dretB=(16.X),(-9.5.y)
      */
    
    private const float Limit_dret = 16f; // Eix x.
    private const float Limit_esquerra = -16f; // Eix z.
    private const float Limit_inferior = -9.5f; // Eix y.
    private const float Limit_superior = 6.7f; // Eix y.
    private int numEscuts;
    public GameObject textEscuts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velNau = 10f;
        //numEscuts = 3;
        ValorsGlobals.numeroEscuts = 3; 
    }
    void Update()
    {
        Movimentnau();
        ControlLimitsPantalla();
    }
    void ControlLimitsPantalla()
    {
        if (transform.position.z < Limit_esquerra)
        {
            transform.position = new Vector3(
                Limit_esquerra,
                transform.position.y,
                transform.position.x
            );
        }

        if (transform.position.z > Limit_dret)
        {
            transform.position = new Vector3(
                Limit_dret,
                transform.position.y,
                transform.position.x
            );
        }

        if (transform.position.y < Limit_inferior)
        {
            transform.position = new Vector3(
                transform.position.x,
                Limit_inferior,
                transform.position.z
            );
        }

        if (transform.position.y > Limit_superior)
        {
            transform.position = new Vector3(
                transform.position.x,
                Limit_superior,
                transform.position.z
            );
        }
    }

    void Movimentnau()
    {
        float movimentHoritzontal = Input.GetAxisRaw("Horizontal");
        float movimentVertical = Input.GetAxisRaw("Vertical");
        
        Vector3 vectorDireccio = new Vector3(movimentHoritzontal, movimentVertical,0);
        vectorDireccio = vectorDireccio.normalized;

        Vector3 nouDesplazament = new Vector3(
            vectorDireccio.x * velNau * Time.deltaTime,
            vectorDireccio.y * velNau * Time.deltaTime,
            0
            );
        transform.position += nouDesplazament;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if (ValorsGlobals.numeroEscuts > 0)
            {
                ValorsGlobals.numeroEscuts--;
                textEscuts.GetComponent<TMPro.TextMeshProUGUI>().text = "Escuts: "+ ValorsGlobals.numeroEscuts.ToString();
            }
            else
            {
                
                Debug.Log("Nau Destruida");
                SceneManager.LoadScene("Escena resultats");
            }
               
        }
    }
}
