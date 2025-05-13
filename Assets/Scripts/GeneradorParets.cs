using UnityEngine;

public class GeneradorParets: MonoBehaviour
{
    private const float Limit_Anterior = 144f; // Eix z.
    private const float Limit_dret = 56f; // Eix x.
    private const float Limit_esquerra = -56f; // Eix z.
    private const float Limit_inferior = -27f; // Eix y.
    private const float Limit_superior = 26f; // Eix y.
    public GameObject prefabParet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("GeneraParets",0f,1f);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GeneraParets()
    {
        // Paret superior dreta.
        GameObject paretSuperiorDreta = Instantiate(prefabParet);
        paretSuperiorDreta.transform.position = new Vector3( Limit_dret,Limit_superior, Limit_Anterior);
        // Paret superior esquerra.
        GameObject paretSuperiorEsquerra = Instantiate(prefabParet);
        paretSuperiorEsquerra.transform.position = new Vector3(Limit_esquerra, Limit_superior, Limit_Anterior);

        // Paret inferior dreta.
        GameObject paretInferiorDreta = Instantiate(prefabParet);
        paretInferiorDreta.transform.position = new Vector3(Limit_dret, Limit_inferior, Limit_Anterior);

        // Paret inferior esquerra.
        GameObject paretInferiorEsquerra = Instantiate(prefabParet);
        paretInferiorEsquerra.transform.position = new Vector3(Limit_esquerra, Limit_inferior, Limit_Anterior);
    }

}
