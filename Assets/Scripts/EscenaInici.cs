using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaInicii : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ObrirEscenaJugar()
    {
        SceneManager.LoadScene("Escena Joc"); 
    }
    public static void SortirAplicacio()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
