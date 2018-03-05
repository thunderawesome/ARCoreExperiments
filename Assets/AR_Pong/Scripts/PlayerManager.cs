
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PlayerManager : MonoBehaviour {

    public string playerName;
    public Color playerColor;
    public int playerScore;


	// Use this for initialization
	void Start () {
        playerName = PhotonNetwork.playerName;
        playerColor = (Color)PhotonNetwork.player.CustomProperties["PlayerColor"];

        //reset players score to zero
        playerScore = 0;
        Hashtable setPlayerScore = new Hashtable() { { "PlayerScore", playerScore } };
        PhotonNetwork.player.SetCustomProperties(setPlayerScore);
    }
	
    /// <summary>
    /// up the players score when they get the puck in the opponents goal
    /// </summary>
    public void AddScore()
    {
        playerScore++;
        Hashtable addPlayerScore = new Hashtable() { { "PlayerScore", playerScore } };
        PhotonNetwork.player.SetCustomProperties(addPlayerScore);
    }

    /// <summary>
    /// Reduce the players score for when they score on themselves
    /// </summary>
    public void ReduceScoer()
    {
        if(playerScore > 0)
        {
            playerScore--;
            Hashtable subtractPlayerScore = new Hashtable() { { "PlayerScore", playerScore } };
            PhotonNetwork.player.SetCustomProperties(subtractPlayerScore);
        }
    }
}
