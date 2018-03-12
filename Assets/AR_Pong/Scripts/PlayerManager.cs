
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon;
using Vuforia;
using GoogleARCore;
using TMPro;

namespace Battlerock
{

    public class PlayerManager : PunBehaviour
    {
        #region Public Variables
        public string playerName;
        public Color playerColor;
        public int playerScore;

        public TextMeshPro tmPro;
        #endregion

        #region Unity Methods
        // Use this for initialization
        public void Start()
        {
            MultiplayerManager.Instance.localPlayer = PhotonNetwork.player;

            for (int i = 0; i < MultiplayerManager.Instance.NumberOfPlayers; i++)
            {
                if (PhotonNetwork.playerList[i].ID == photonView.owner.ID)
                {
                    playerName = PhotonNetwork.playerList[i].NickName;
                }
                else
                {
                    MultiplayerManager.Instance.otherPlayer = PhotonNetwork.playerList[i];
                }
            }

            if (photonView.isMine == true)
            {
                playerColor = ES3.Load<Color>(PhotonNetwork.playerName);
                photonView.RPC("SetName", PhotonTargets.AllBuffered, playerName);
                photonView.RPC("SetColor", PhotonTargets.AllBuffered, playerColor.r, playerColor.g, playerColor.b);
            }


            //reset players score to zero
            playerScore = 0;
            Hashtable setPlayerScore = new Hashtable() { { "PlayerScore", playerScore } };
            PhotonNetwork.player.SetCustomProperties(setPlayerScore);
        }

        //public void Update()
        //{
        //    if (photonView.isMine)
        //    {
        //        SetPosition();
        //    }
        //}

        /// <summary>
        /// Updates the color across the network so all players can see which color the other players are.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        [PunRPC]
        public void SetColor(float r, float g, float b)
        {
            //set the Game objects underneath the Player like the paddle to the players color
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            {
                rend.material.color = new Color(r, g, b, 1);
            }

            tmPro.faceColor = new Color(r, g, b, 1);
        }

        /// <summary>
        /// Updates the player's name so other players can see it.
        /// </summary>
        /// <param name="name"></param>
        [PunRPC]
        public void SetName(string name)
        {
            tmPro.text = name;
        }
        #endregion

        #region Public Methods
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the local player's position to the local player's ARCamera Device location.
        /// </summary>
        private void SetPosition()
        {
            Vector3 camPos = Camera.main.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
        }

        /// <summary>
        /// Apply a custon property it's new value to the player
        /// </summary>
        /// <param name="value"></param> Hashtable of { "PropertyName", propertyValue }
        private void SetCustomProperty(Hashtable value)
        {
            PhotonNetwork.player.SetCustomProperties(value);
        }
        #endregion
    }
}