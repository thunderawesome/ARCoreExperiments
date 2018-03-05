using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Variables
        public Transform[] prefabs;
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

            for (int i = 0; i < prefabs.Length; i++)
            {
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                GameObject player = PhotonNetwork.Instantiate(prefabs[i].name, transform.position, Quaternion.identity, 0);
            }
        }
    }
    #endregion
}