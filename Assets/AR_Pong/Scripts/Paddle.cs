using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlerock
{
    public class Paddle : MonoBehaviour
    {
        #region Public Variables
        public float offset = .02f;
        #endregion

        #region Private Variables
        private PhotonView m_photonView;
        #endregion

        private void Awake()
        {
            Vector3 camPos = GoogleARCore.Experiments.ARController.Instance.FirstPersonCamera.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, camPos.z + offset);
        }

        private void Start()
        {
            m_photonView = GetComponent<PhotonView>();

            //if (m_photonView.isMine)
            //{
            //    var renderers = GetComponentsInChildren<Renderer>();

            //    for (int i = 0; i < renderers.Length; i++)
            //    {
            //        renderers[i].enabled = false;
            //    }               
            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (m_photonView.isMine)
            {
                Vector3 camPos = GoogleARCore.Experiments.ARController.Instance.FirstPersonCamera.transform.position;
                transform.position = new Vector3(camPos.x, camPos.y, camPos.z + offset);
            }
        }
    }
}