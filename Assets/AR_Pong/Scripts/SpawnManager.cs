using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Variables
        public Transform playerPrefab;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called right after Awake)
        /// </summary>
        private void Start()
        {
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.connected)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(NetworkSettings.Instance.level.ToString());
                return;
            }

            _GameManager.Instance.gameMode = GameMode.Preparation;

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            GameObject player = PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0);

            //MultiplayerManager.Instance.players.Add(player.GetComponent<PhotonPlayer>());
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                Debug.Log(PhotonNetwork.playerList[i]);
            }
            
            //player.transform.parent = MultiplayerManager.Instance.anchor.transform;
        }
    }
    #endregion
}