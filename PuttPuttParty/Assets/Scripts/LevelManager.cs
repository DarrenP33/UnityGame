using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public BallHandler golfBall;
    public TextMeshProUGUI labelPlayerName;

    private PlayerRecords playerRecord;
    private int playerIndex;

    private void Start()
    {
        playerRecord = GameObject.Find("Player Record").GetComponent<PlayerRecords>();
        playerIndex = 0;
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        golfBall.SetBall(playerRecord.playerColors[playerIndex]);
        labelPlayerName.text = playerRecord.playerList[playerIndex].name;
    }

    public void nextPlayer(int previousPutts)
    {
        playerRecord.AddPutts(playerIndex, previousPutts);
        if (playerIndex < playerRecord.playerList.Count-1)
        {
            playerIndex++;
            SetupPlayer();
            golfBall.SetBall(playerRecord.playerColors[playerIndex]);
        }
        else
        {
            if (playerRecord.levelIndex == playerRecord.levels.Length - 1)
            {
                Debug.Log("Scoreboard");
            }
            else
            {
                playerRecord.levelIndex++;
                SceneManager.LoadScene(playerRecord.levels[playerRecord.levelIndex]);
            }
        }
    }
}
