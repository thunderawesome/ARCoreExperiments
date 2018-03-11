using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class SpawnManager : MonoBehaviour
    {
        #region Public Variables
        public GameObject player;
        public GameObject goal;
        public GameObject puck;

        public static SpawnManager Instance;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Unity's built-in method (Called before other methods)
        /// </summary>
        private void Awake()
        {
            Instance = this;
        }

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

#if UNITY_EDITOR
            SpawnPlayer();
#endif
        }
        #endregion

        #region Public Methods
        public void SpawnPlayer(Transform parent = null)
        {
            Vector3 position = Vector3.zero;

            GameObject obj = PhotonNetwork.Instantiate(player.name, position, Quaternion.identity, 0);
            obj.transform.parent = parent;

            _GameManager.Instance.gameMode = GameMode.Playing;
        }

        public void SpawnGoal()
        {

        }

        public void SpawnPuck()
        {

        }
        #endregion
    }

}