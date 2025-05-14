// ValorsGlobals.cs
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HighScoreEntry
{
    public string playerName;
    public int score;

    public HighScoreEntry(string name, int score)
    {
        playerName = name;
        this.score = score;
    }
}

public static class ValorsGlobals
{
    public static int numeroEscuts;
    public static int score;
    public static float startTime;
    public static float timeAlive;

    public static string playerName;  // Nuevo nombre del jugador
    public static List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}