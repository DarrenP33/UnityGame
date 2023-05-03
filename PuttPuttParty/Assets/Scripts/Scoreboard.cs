using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI names, score;

    private PlayerRecords playerRecord;

    void Start()
    {
        playerRecord = GameObject.Find("Player Record").GetComponent<PlayerRecords>();
        names.text = "";
        score.text = "";
        foreach (var player in playerRecord.GetScoreboardList())
        {
            names.text += player.name + "\n";
            score.text += player.totalPutts + "\n";
        }
    }

    void Update()
    {
        score.fontSize = names.fontSize;
    }

    public void ButtonReturnMenu()
    {
        Destroy(playerRecord.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
