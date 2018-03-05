using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlerock
{
    public class Paddle : MonoBehaviour
    {
        #region Public Variables
        public float offset = .01f;
        #endregion

        // Update is called once per frame
        void Update()
        {
            Vector3 camPos = GoogleARCore.Battlerock.ARController.Instance.FirstPersonCamera.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, camPos.z + offset);
        }
    }
}