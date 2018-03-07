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
                UnityEngine.SceneManagement.SceneManager.LoadScene(LEVEL.PongGame.ToString());
                yield return null;
            }



            _GameManager.Instance.gameMode = GameMode.Playing;

#if !UNITY_EDITOR
            while (MultiplayerManager.Instance.Anchor == null)
            {
                yield return null;
            }
#endif
            SpawnPlayer();
        }
        #endregion

        #region Public Methods
        public void SpawnPlayer()
        {
            Vector3 position = Vector3.zero;
            if(MultiplayerManager.Instance.Anchor != null)
            {
                position = MultiplayerManager.Instance.Anchor.transform.position;
            }

            PhotonNetwork.Instantiate(player.name, position, Quaternion.identity, 0);
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