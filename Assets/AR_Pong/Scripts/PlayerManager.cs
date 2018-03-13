﻿using UnityEngine;
using ExitGames.Client.Photon;
using Photon;
using TMPro;
using InControl;

namespace Battlerock
{

    public class PlayerManager : PunBehaviour
    {
        #region Public Variables
        public string playerName;
        public Color playerColor;

        public float movementSpeed = 2f;

        public TextMeshPro tmPro;
        #endregion

        #region Private Variables
        private PaddleActions Actions;
        private Rigidbody m_rigidbody;
        #endregion

        #region Unity Methods
        // Use this for initialization
        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();

            InitializeInput();


            for (int i = 0; i < MultiplayerManager.Instance.NumberOfPlayers; i++)
            {
                if (PhotonNetwork.playerList[i].ID == photonView.owner.ID)
                {
                    playerName = PhotonNetwork.playerList[i].NickName;
                }
            }

            if (photonView.isMine == true)
            {
                playerColor = ES3.Load<Color>(PhotonNetwork.playerName);
                photonView.RPC("SetName", PhotonTargets.AllBuffered, playerName);
                photonView.RPC("SetColor", PhotonTargets.AllBuffered, playerColor.r, playerColor.g, playerColor.b);
            }
        }

        public void Update()
        {
            if (photonView.isMine)
            {
                MovementInput(Actions.Move.Value);
            }
        }

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
     
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets the local player's position to the local player's ARCamera Device location.
        /// </summary>
        /// <param name="x">Direction for movement.</param>
        private void MovementInput(float x)
        {
            Vector3 vel = m_rigidbody.velocity;
            vel.x = x * movementSpeed;

            m_rigidbody.velocity = vel;
        }

        /// <summary>
        /// Apply a custon property it's new value to the player
        /// </summary>
        /// <param name="value"></param> Hashtable of { "PropertyName", propertyValue }
        private void SetCustomProperty(Hashtable value)
        {
            PhotonNetwork.player.SetCustomProperties(value);
        }

        private void InitializeInput()
        {
            Actions = new PaddleActions();
            Actions.Left.AddDefaultBinding(InputControlType.DPadLeft);
            Actions.Right.AddDefaultBinding(InputControlType.DPadRight);
            Actions.Left.AddDefaultBinding(Key.LeftArrow);
            Actions.Right.AddDefaultBinding(Key.RightArrow);

            if (MultiplayerManager.Instance.LocalPlayer.IsMasterClient == true)
            {
                Actions.Left.AddDefaultBinding(InputControlType.LeftStickRight);
                Actions.Right.AddDefaultBinding(InputControlType.LeftStickLeft);
            }
            else
            {
                Actions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
                Actions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
            }
        }
        #endregion
    }
}