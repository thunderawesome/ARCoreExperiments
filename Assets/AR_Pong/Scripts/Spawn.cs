using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlerock
{
    public class Spawn : MonoBehaviour
    {
        public Transform playerPrefab;

        public void Awake()
        {
            // in case we started this demo with the wrong scene being active, simply load the menu scene
            if (!PhotonNetwork.connected)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(LEVEL.PongGame.ToString());
                return;
            }

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0);
        }
    }
}