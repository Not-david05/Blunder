// Resultats.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Resultats : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI displayText;
    public Button nextButton;

    private List<HighScoreEntry> scores;
    private int index = -1;

    void Start()
    {
        scores = ValorsGlobals.highScores;
        nextButton.onClick.AddListener(ShowNext);
        ShowNext();
    }

    void ShowNext()
    {
        if (index == -1)
        {
            int finalScore = ValorsGlobals.score;
            float t = ValorsGlobals.timeAlive;
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            // Usar interpolaci�n de cadenas para evitar FormatException
            displayText.text = $" Jugador: {ValorsGlobals.playerName} \n Puntuaci�n: {finalScore} \n Tiempo: {minutes: 00}:{seconds: 00}";
            index++;
        }
        else if (index < scores.Count)
        {
            var entry = scores[index];
            displayText.text = $"Top {index + 1}: {entry.playerName} - {entry.score}";
            index++;
        }
        else
        {
            // Mostrar mensaje final en rojo y tama�o mayor
            displayText.text = "<size=36><color=red>Fin de la lista de puntuaciones</color></size>";

            // Cambiar bot�n a "Volver"
            if (nextButton.TryGetComponent<TextMeshProUGUI>(out var btnText))
            {
                btnText.text = "Volver";
            }
            else
            {
                // Si el texto est� en hijo
                var childText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
                if (childText != null) childText.text = "Volver";
            }

            // Configurar bot�n para volver a escena inicial
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(() => SceneManager.LoadScene("Escena inici"));
        }
    }
}