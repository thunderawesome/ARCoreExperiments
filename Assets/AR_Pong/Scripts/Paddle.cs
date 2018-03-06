using Photon;
using UnityEngine;

namespace Battlerock
{
    public class Paddle : PunBehaviour
    {
        #region Public Variables
        public float offset = .02f;
        #endregion

        Color synccolor; Vector3 tempcolor;

        private void Awake()
        {
            Vector3 camPos = GoogleARCore.Experiments.ARController.Instance.FirstPersonCamera.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, camPos.z + offset);
        }

        private void Start()
        {
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
            //if (photonView.isMine)
            //{
            //    Vector3 camPos = GoogleARCore.Experiments.ARController.Instance.FirstPersonCamera.transform.position;
            //    transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
            //}

            if (!photonView.isMine)
            {
                gameObject.GetComponent<Renderer>().material.color = synccolor;
                return;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                //send color
                tempcolor = new Vector3(gameObject.GetComponent<Renderer>().material.color.r, gameObject.GetComponent<Renderer>().material.color.g, gameObject.GetComponent<Renderer>().material.color.b);

                stream.Serialize(ref tempcolor);
            }
            else
            {
                //get color
                stream.Serialize(ref tempcolor);

                synccolor = new Color(tempcolor.x, tempcolor.y, tempcolor.z, 1.0f);

            }
        }
    }
}