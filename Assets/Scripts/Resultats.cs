using UnityEngine;

public class Resultats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject textEscuts = GameObject.Find("EscutsRestants");
        textEscuts.GetComponent<TMPro.TextMeshProUGUI>().text= "Escuts restants: "+ ValorsGlobals.numeroEscuts.ToString();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
