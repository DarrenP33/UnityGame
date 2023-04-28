using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public PlayerRecords playerRecord;
    public Button buttonStart;
    public Button buttonQuit;
    public Button buttonAddPlayer;

    public void ButtonAddPlayer() 
    { 
        playerRecord.AddPlayer(inputPlayerName.text);
        buttonStart.interactable = true;
        inputPlayerName.text = "";

        if (playerRecord.playerList.Count == playerRecord.playerColors.Length)
        {
            buttonAddPlayer.interactable = false;
        }
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(playerRecord.levels[0]);
    }

    public void ButtonQuit()
    {
        Application.Quit(); 
    }
}
