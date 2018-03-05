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
        private IEnumerator Start()
        {
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.connected)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(NetworkSettings.Instance.level.ToString());
                yield return null;
            }

            _GameManager.Instance.gameMode = GameMode.Preparation;


            while (MultiplayerManager.Instance.anchor == null)
            {
                yield return null;
            }

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            GameObject player = PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0);

            player.transform.parent = MultiplayerManager.Instance.anchor.transform;
        }
    }
    #endregion
}