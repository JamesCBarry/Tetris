using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;

    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text levelText;
    [SerializeField] public GameObject gameOver;

    private float score = 0;
    private int rowsCleared = 0;
    public float level = 1;

    public GameObject spawner;

    public void UpdateScore()
    {
        score += 1000 * (1 + (level * 0.1f));
        scoreText.text = "Score: " + score;
        UpdateRowsCleared();
    }

    public void NextLevel()
    {
        level++;
        levelText.text = "Level " + level;
        FindObjectOfType<Background>().UpdateBackground();
    }

    public void UpdateRowsCleared()
    {
        rowsCleared += 1;
        print(rowsCleared);
        if (rowsCleared % 10 == 0)
        {
            Playfield.deletePlayfield();
            NextLevel();
        }
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        spawner.SetActive(false);
    }

    public void RestartGame()
    {
        score = 0;
        level = 1;
        gameOver.SetActive(false);

        Playfield.deletePlayfield();
        FindObjectOfType<Background>().UpdateBackground();

        FindObjectOfType<Spawner>().NextBlock();
    }
}

/* Running to build list:

 * 1. Add a way to restart the game. Right now, the problem is there's a chance for the "down" movement (or auto-movement) to summon an unweildly large amount of blocks.
 * 2. Fix the required double-click feature.
 * 3. Add a way to loop through multiple sets of music.

*/