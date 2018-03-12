using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battlerock
{
    public class MultiplayerManager : PunBehaviour
    {
        #region Public Variables
        public static MultiplayerManager Instance;

        public bool isReady = false;       

        public PhotonPlayer LocalPlayer
        {
            get
            {
                if (m_localPlayer == null)
                {
                    m_localPlayer = PhotonNetwork.player;
                    return m_localPlayer;
                }
                else
                {
                    return m_localPlayer;
                }
            }
        }

        public PhotonPlayer OtherPlayer
        {
            get
            {
                if(m_otherPlayer == null)
                {
                    if(PhotonNetwork.playerList.Length > 1)
                    {
                        m_otherPlayer = PhotonNetwork.playerList[1];
                        return m_otherPlayer;
                    }
                    else
                    {
                        Debug.LogError("No OTHER player has joined the server yet.");
                        return null;
                    }
                }
                else
                {
                    return m_otherPlayer;
                }
            }
        }

        public int NumberOfPlayers
        {
            get { return PhotonNetwork.playerList.Length; }
        }
        #endregion

        #region Private Variables
        private PhotonPlayer m_localPlayer;
        private PhotonPlayer m_otherPlayer;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before anything else)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Called after disconnecting from the Photon server.
        /// </summary>
        /// <remarks>
        /// In some cases, other callbacks are called before OnDisconnectedFromPhoton is called.
        /// Examples: OnConnectionFail() and OnFailedToConnectToPhoton().
        /// </remarks>
        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogError(GetType() + ":Disconnected");
            SceneManager.LoadScene(LEVEL.PongLauncher.ToString());
        }
        #endregion
    }
}