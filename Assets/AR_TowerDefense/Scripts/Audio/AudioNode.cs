using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AUDIONODE_TYPE
{
    AUDIO = 0,
    MUSIC = 1
}

namespace Battlerock
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioNode : MonoBehaviour
    {

        private AudioSource AControl;
        private AudioMixerGroup AMG;

        private AUDIONODE_TYPE ANT;

        private static readonly Vector3 VECTOR_RESET = new Vector3(0, 0, 0);

        void Awake()
        {
            AControl = gameObject.GetComponent<AudioSource>();
            AMG = AudioController.Instance.SFX_GROUP;
        }

#region GETTERS / SETTERS
        public void setAudioClip(AudioClip clip){
            AControl.clip = clip;
        }
        public void setPitch(float pValue) {
            AControl.pitch = pValue;
        }
        public void setVolume(float vValue){
            AControl.volume = vValue;
        }
        public void setDoppler(float dValue){
            AControl.dopplerLevel = dValue;
        }
        public void setSpatialBlend(float sValue) {
            AControl.spatialBlend = sValue;
        }
        public void setLooping(bool lValue) {
            AControl.loop = lValue;
        }
        public void setAudioGroup(AudioMixerGroup group) {
            AControl.outputAudioMixerGroup = group;
        }
        public void setAudioNodeType(AUDIONODE_TYPE ant) {
            ANT = ant;
        }
        #endregion

        void OnDisable()
        {
            //DEFAULTS==============
            if (AControl.isPlaying)
                AControl.Stop();

            if (AControl.pitch != 1)
                AControl.pitch = 1;

            if (AControl.volume != 0)
                AControl.volume = 0;

            if (AControl.spatialBlend != 1)
                AControl.spatialBlend = 1;

            if (AControl.loop)
                AControl.loop = false;

            if (AControl.outputAudioMixerGroup != AMG)
                AControl.outputAudioMixerGroup = AMG;
            
            if (this.gameObject.transform.position != VECTOR_RESET)
                this.gameObject.transform.position = VECTOR_RESET;

            if (ANT == AUDIONODE_TYPE.AUDIO)
                AudioController.Instance.addAvailableAudioObjs();
            else if (ANT == AUDIONODE_TYPE.MUSIC)
            {
                AudioController.Instance.addAvailableMusicObjs();
                ANT = AUDIONODE_TYPE.AUDIO;
            }



        }
        public IEnumerator PlayAudio()
        {
            AControl.Play();
            bool isPlaying = true;
            while (isPlaying)
            {
                if (!AControl.isPlaying)
                    isPlaying = false;
                yield return null;
            }
            gameObject.SetActive(false);
            yield return null;
        }
    }
}