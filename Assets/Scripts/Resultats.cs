using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Resultats : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI resultatsFinalsText;  // Texto donde se muestran todos los resultados
    public Button returnButton;

    void Start()
    {
        MostrarResultats();
        returnButton.onClick.AddListener(() => SceneManager.LoadScene("Escena inici"));
    }

    void MostrarResultats()
    {
        List<HighScoreEntry> scores = ValorsGlobals.highScores;
        string resultats = "";

        // Mostrar datos del jugador actual
        int finalScore = ValorsGlobals.score;
        float t = ValorsGlobals.timeAlive;
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);

        resultats += $"<b>Jugador actual</b>:\n";
        resultats += $"Nombre: {ValorsGlobals.playerName}\n";
        resultats += $"Puntuación: {finalScore}\n";
        resultats += $"Tiempo: {minutes:00}:{seconds:00}\n\n";

        // Mostrar tabla de puntuaciones
        resultats += "<b>Tabla de puntuaciones</b>:\n";
        for (int i = 0; i < scores.Count; i++)
        {
            resultats += $"Top {i + 1}: {scores[i].playerName} - {scores[i].score}\n";
        }

        resultatsFinalsText.text = resultats;
    }
}
