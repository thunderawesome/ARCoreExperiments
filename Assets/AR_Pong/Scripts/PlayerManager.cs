
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

public class PlayerManager : MonoBehaviour
{

    public string playerName;
    public Color playerColor;
    public int playerScore;


    // Use this for initialization
    public void Initialize()
    {
        playerName = PhotonNetwork.playerName;
        var col = (Vector3)PhotonNetwork.player.CustomProperties["PlayerColor"];
        playerColor = new Color(col.x, col.y, col.z, 1);

        //set the Game objects underneath the Player like the paddle to the players color
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = playerColor;
        }

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
        SetCustomProperty(new Hashtable() { { "PlayerScore", playerScore } });
    }

    /// <summary>
    /// Reduce the players score for when they score on themselves
    /// </summary>
    public void ReduceScore()
    {
        if (playerScore > 0)
        {
            playerScore--;
            SetCustomProperty(new Hashtable() { { "PlayerScore", playerScore } });
        }
    }

    /// <summary>
    /// Apply a custon property it's new value to the player
    /// </summary>
    /// <param name="value"></param> Hashtable of { "PropertyName", propertyValue }
    private void SetCustomProperty(Hashtable value)
    {
        PhotonNetwork.player.SetCustomProperties(value);
    }
}
