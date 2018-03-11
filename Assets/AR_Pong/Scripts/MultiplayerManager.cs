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

        public PhotonPlayer localPlayer;
        public PhotonPlayer otherPlayer;

        public int NumberOfPlayers
        {
            get { return PhotonNetwork.playerList.Length; }
        }
        #endregion

        #region Private Variables
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