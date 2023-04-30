using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecords : MonoBehaviour
{
    public List<Player> playerList;
    public string[] levels;
    public Color[] playerColors;
    [HideInInspector] public int levelIndex;

    void OnEnable()
    {
        playerList = new List<Player>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddPlayer(string name)
    {
        playerList.Add(new Player(name, playerColors[playerList.Count], levels.Length));
    }

    public void AddPutts(int playerIndex, int puttCount)
    {
        playerList[playerIndex].putts[levelIndex] = puttCount;
    }

    public class Player
    {

        public string name;
        public Color color;
        public int[] putts;

        public Player(string newName, Color newColor, int levelCount)
        {
            name = newName;
            color = newColor;
            putts = new int[levelCount];
        }     
    }
}

