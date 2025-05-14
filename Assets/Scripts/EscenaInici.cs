using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EscenaInicii : MonoBehaviour
{
    public TMP_InputField inputPlayerName;  // Campo de texto para nombre

    public void ObrirEscenaJugar()
    {
        // Guardar nombre antes de cargar escena
        ValorsGlobals.playerName = inputPlayerName.text;
        SceneManager.LoadScene("Escena Joc");
    }

    public void SortirAplicacio()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
