using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public enum SOUNDS
{
    SMALL_HIT = 0,
    HEAVY_HIT = 1,
    SMALL_DEFLECT = 2,
    HEAVY_DEFLECT = 3,
    WOOSH = 4,
    STEP = 5,
    GONG = 6,
    DELETE = 7,
    DIEDIE = 8,
    HELLOTHERE = 9,
    IFOUNDYOU = 10,
    PLEASEDIENOW = 11,
    TERMINATE = 12,
    TIMETODIE = 13
};

public enum MUSIC
{
    MENU = 0,
    HEAVY_LVL = 1,
    LIGHT_LVL = 2
}

public enum SURFACE
{
    WOOD = 0,
    WATER = 1,
    GROUND = 2,
    GRAVEL = 3,
    CONCRETE = 4,
}

namespace Battlerock
{
    public class AudioController : MonoBehaviour
    {
        //AUDIO===========================
        private int pooledAudioCount = 30;
        private int availableAudioObjs;

        public AudioClip[] ALL_AUDIO;

        private List<GameObject> AudioObjs;
        public GameObject AudioObj;

        //MUSIC===========================
        private int pooledMusicCount = 2;
        private int availableMusicObjs;

        public AudioClip[] ALL_MUSIC;

        private List<GameObject> MusicObjs;

        //MIXER===========================
        public AudioMixer MASTER_MIXER;
        public AudioMixerGroup MUSIC_GROUP;
        public AudioMixerGroup SFX_GROUP;

        public static AudioController Instance;

        private static readonly Quaternion QUATERNION_IDENTITY = new Quaternion(0, 0, 0, 1);

        void Awake()
        {
            Instance = this;

            //AUDIO===================================
            availableAudioObjs = (pooledAudioCount - 1);
            AudioObjs = new List<GameObject>();
            for (int i = 0; i < pooledAudioCount; i++)
            {
                GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
                AudioObjs.Add(obj);
            }
            for (int j = 0; j < AudioObjs.Count; j++)
            {
                AudioObjs[j].transform.parent = gameObject.transform;
                AudioObjs[j].SetActive(false);
            }
            //MUSIC====================================
            availableMusicObjs = (pooledMusicCount - 1);
            MusicObjs = new List<GameObject>();
            for (int i = 0; i < pooledMusicCount; i++)
            {
                GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
                MusicObjs.Add(obj);
            }
            for (int j = 0; j < MusicObjs.Count; j++)
            {
                MusicObjs[j].transform.parent = gameObject.transform;
                MusicObjs[j].SetActive(false);
            }
            float volumne = 0;
            //INIT MusicVol====================
            if (PlayerPrefs.HasKey("MusicVol"))
                SetMusicVolume(PlayerPrefs.GetFloat("MusicVol"));
            else
            {
                MASTER_MIXER.GetFloat("MusicVol", out volumne);
                PlayerPrefs.SetFloat("MusicVol", volumne);
            }
            //INIT SFXVol======================
            if (PlayerPrefs.HasKey("SFXVol"))
                SetMusicVolume(PlayerPrefs.GetFloat("SFXVol"));
            else
            {
                MASTER_MIXER.GetFloat("SFXVol", out volumne);
                PlayerPrefs.SetFloat("SFXVol", volumne);
            }
        }
        #region AUDIO SFX
        public void PlayAudio(SOUNDS snd)
        {
            //DECLARE
            GameObject audObj = getActiveAudioObj();
            AudioNode ao = audObj.GetComponent<AudioNode>();
            //ASSIGN
            ao.setAudioClip(getAudioClip(snd));
            ao.setVolume(getVolume(snd));
            //EXCECUTE
            StartCoroutine(ao.PlayAudio());
        }
        public void PlayAudio2D(SOUNDS snd)
        {
            //DECLARE
            GameObject audObj = getActiveAudioObj();
            AudioNode ao = audObj.GetComponent<AudioNode>();
            //ASSIGN
            ao.setSpatialBlend(0);
            ao.setAudioClip(getAudioClip(snd));
            ao.setVolume(getVolume(snd));
            //EXCECUTE
            StartCoroutine(ao.PlayAudio());
        }
        public void PlayAudio(SOUNDS snd, Vector3 pos)
        {
            //DECLARE
            GameObject audObj = getActiveAudioObj();
            AudioNode ao = audObj.GetComponent<AudioNode>();
            //ASSIGN
            audObj.transform.position = pos;
            ao.setAudioClip(getAudioClip(snd));
            ao.setVolume(getVolume(snd));

            //EXCECUTE
            StartCoroutine(ao.PlayAudio());
        }
        public void PlayAudio(SOUNDS snd, Vector3 pos, bool isRandom)
        {
            //DECLARE
            GameObject audObj = getActiveAudioObj();
            AudioNode ao = audObj.GetComponent<AudioNode>();
            //ASSIGN
            audObj.transform.position = pos;
            ao.setAudioClip(getAudioClip(snd));
            ao.setPitch(getRandomPitch(snd));
            ao.setVolume(getVolume(snd));
            //EXCECUTE
            StartCoroutine(ao.PlayAudio());
        }

        #region GETTERS / SETTERS ======================================

        public GameObject getActiveAudioObj()
        {
            availableAudioObjs -= 1;
            for (int i = 0; i < AudioObjs.Count; i++)
            {
                if (!AudioObjs[i].activeSelf)
                {
                    AudioObjs[i].SetActive(true);
                    return AudioObjs[i];
                }
                else if (i == (AudioObjs.Count - 1))
                {
                    GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
                    AudioObjs.Add(obj);
                    pooledAudioCount += 1;
                    //Debug.Log(i);
                    AudioObjs[(pooledAudioCount - 1)].transform.parent = gameObject.transform;
                    AudioObjs[(pooledAudioCount - 1)].SetActive(false);
                }
            }
            return new GameObject();
        }
        public void addAvailableAudioObjs()
        {
            if (availableAudioObjs != (pooledAudioCount - 1))
                availableAudioObjs += 1;
        }
        private AudioClip getAudioClip(SOUNDS snd)
        {
            AudioClip audioReturn = new AudioClip();

            switch (snd)
            {
                case SOUNDS.SMALL_HIT: audioReturn = ALL_AUDIO[0]; break;
                case SOUNDS.HEAVY_HIT: audioReturn = ALL_AUDIO[1]; break;
                case SOUNDS.SMALL_DEFLECT: audioReturn = ALL_AUDIO[2]; break;
                case SOUNDS.HEAVY_DEFLECT: audioReturn = ALL_AUDIO[3]; break;
                case SOUNDS.WOOSH: audioReturn = ALL_AUDIO[4]; break;
                case SOUNDS.STEP: audioReturn = ALL_AUDIO[5]; break;
                case SOUNDS.GONG: audioReturn = ALL_AUDIO[6]; break;
                case SOUNDS.DELETE: audioReturn = ALL_AUDIO[7]; break;
                case SOUNDS.DIEDIE: audioReturn = ALL_AUDIO[8]; break;
                case SOUNDS.HELLOTHERE: audioReturn = ALL_AUDIO[9]; break;
                case SOUNDS.IFOUNDYOU: audioReturn = ALL_AUDIO[10]; break;
                case SOUNDS.PLEASEDIENOW: audioReturn = ALL_AUDIO[11]; break;
                case SOUNDS.TERMINATE: audioReturn = ALL_AUDIO[12]; break;
                case SOUNDS.TIMETODIE: audioReturn = ALL_AUDIO[13]; break;
                default: Debug.Log("AUDIO MANAGER EXCEPTION"); break;
            }
            return audioReturn;
        }
        private float getRandomPitch(SOUNDS snd)
        {
            float pitchReturn = 0.0F;

            switch (snd)
            {
                case SOUNDS.STEP:
                    float[] rando = new float[5] { 0.85F, 1.0F, 1.15F, 1.30F, 1.45F };
                    pitchReturn = rando[Random.Range(0, (rando.Length - 1))];
                    break;
                case SOUNDS.WOOSH:
                    float[] randoW = new float[3] { 0.9F, 1.0F, 1.1F };
                    pitchReturn = randoW[Random.Range(0, (randoW.Length - 1))];
                    break;
                case SOUNDS.HEAVY_HIT:
                    float[] randoHH = new float[3] { 0.8F, 0.9F, 1.0F };
                    pitchReturn = randoHH[Random.Range(0, (randoHH.Length - 1))];
                    break;
                case SOUNDS.SMALL_HIT:
                    float[] randoSH = new float[3] { 0.9F, 1.0F, 1.1F };
                    pitchReturn = randoSH[Random.Range(0, (randoSH.Length - 1))];
                    break;
                case SOUNDS.GONG: pitchReturn = 1.0F; break;
                case SOUNDS.DELETE:
                    float[] randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.DIEDIE:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.HELLOTHERE:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.IFOUNDYOU:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.PLEASEDIENOW:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.TERMINATE:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                case SOUNDS.TIMETODIE:
                    randoD = new float[3] { .75f, 1f, 1.25f };
                    pitchReturn = randoD[Random.Range(0, (randoD.Length - 1))];
                    break;
                default: Debug.Log("AUDIO MANAGER EXCEPTION"); break;
            }
            return pitchReturn;
        }
        private float getVolume(SOUNDS snd)
        {
            float volumeReturn = 0.0F;

            switch (snd)
            {
                //case SOUNDS.SMALL_DEFLECT: audioReturn = ALL_AUDIO[2]; break;
                //case SOUNDS.HEAVY_DEFLECT: audioReturn = ALL_AUDIO[3]; break;
                case SOUNDS.WOOSH: volumeReturn = 0.7F; break;
                case SOUNDS.STEP: volumeReturn = 0.5F; break;
                case SOUNDS.SMALL_HIT: volumeReturn = 0.1F; break;
                case SOUNDS.HEAVY_HIT: volumeReturn = 0.2F; break;
                case SOUNDS.GONG: volumeReturn = 1.0F; break;
                case SOUNDS.DELETE: volumeReturn = .5f; break;
                case SOUNDS.DIEDIE: volumeReturn = .5f; break;
                case SOUNDS.HELLOTHERE: volumeReturn = .5f; break;
                case SOUNDS.IFOUNDYOU: volumeReturn = .5f; break;
                case SOUNDS.PLEASEDIENOW: volumeReturn = .5f; break;
                case SOUNDS.TERMINATE: volumeReturn = .5f; break;
                case SOUNDS.TIMETODIE: volumeReturn = .5f; break;
                default: Debug.Log("AUDIO MANAGER EXCEPTION"); break;
            }
            return volumeReturn;
        }

        #endregion
        #endregion

        #region MUSIC
        public void PlayMusic(MUSIC snd, bool isLooping)
        {
            //DECLARE
            GameObject audObj = getActiveMusicObj();
            AudioNode ao = audObj.GetComponent<AudioNode>();
            //ASSIGN
            ao.setAudioGroup(MUSIC_GROUP);
            ao.setSpatialBlend(0);
            ao.setLooping(true);
            ao.setAudioClip(getMusicClip(snd));
            ao.setVolume(getMusicVolume(snd));
            ao.setAudioNodeType(AUDIONODE_TYPE.MUSIC);
            toggleMenu(true);
            //EXCECUTE
            StartCoroutine(ao.PlayAudio());
        }
        public void PlayMusicIfNot(MUSIC snd, bool isLooping, bool isMenu)
        {
            bool isMusic = false;
            for (int i = 0; i < MusicObjs.Count; i++)
            {
                if (MusicObjs[i].gameObject.activeSelf)
                {
                    isMusic = true;
                    break;
                }
            }

            if (!isMusic)
            {
                //DECLARE
                GameObject audObj = getActiveMusicObj();
                AudioNode ao = audObj.GetComponent<AudioNode>();
                //ASSIGN
                ao.setAudioGroup(MUSIC_GROUP);
                ao.setSpatialBlend(0);
                ao.setLooping(true);
                ao.setAudioClip(getMusicClip(snd));
                ao.setVolume(getMusicVolume(snd));
                ao.setAudioNodeType(AUDIONODE_TYPE.MUSIC);
                //EXCECUTE
                StartCoroutine(ao.PlayAudio());
            }
            toggleMenu(isMenu);
        }
        public void StopAllMusic()
        {
            for (int i = 0; i < MusicObjs.Count; i++)
            {
                MusicObjs[i].SetActive(false);
            }
        }

        #region GETTERS / SETTERS ======================================

        public GameObject getActiveMusicObj()
        {
            availableMusicObjs -= 1;
            for (int i = 0; i < MusicObjs.Count; i++)
            {
                if (!MusicObjs[i].activeSelf)
                {
                    MusicObjs[i].SetActive(true);
                    return MusicObjs[i];
                }
                else if (i == (MusicObjs.Count - 1))
                {
                    GameObject obj = (GameObject)Instantiate(AudioObj, gameObject.transform.position, QUATERNION_IDENTITY);
                    MusicObjs.Add(obj);
                    pooledMusicCount += 1;
                    //Debug.Log(i);
                    MusicObjs[(pooledMusicCount - 1)].transform.parent = gameObject.transform;
                    MusicObjs[(pooledMusicCount - 1)].SetActive(false);
                }
            }
            return new GameObject();
        }
        public void addAvailableMusicObjs()
        {
            if (availableMusicObjs != (pooledMusicCount - 1))
                availableMusicObjs += 1;
        }
        private AudioClip getMusicClip(MUSIC msc)
        {
            AudioClip audioReturn = new AudioClip();

            switch (msc)
            {
                case MUSIC.MENU: audioReturn = ALL_MUSIC[0]; break;
                case MUSIC.HEAVY_LVL: audioReturn = ALL_MUSIC[1]; break;
                case MUSIC.LIGHT_LVL: audioReturn = ALL_MUSIC[2]; break;
                default: Debug.Log("AUDIO MANAGER EXCEPTION"); break;
            }
            return audioReturn;
        }
        private float getMusicVolume(MUSIC msc)
        {
            float volumeReturn = 0.0F;

            switch (msc)
            {
                case MUSIC.MENU: volumeReturn = 0.8F; break;
                case MUSIC.HEAVY_LVL: volumeReturn = 0.6F; break;
                case MUSIC.LIGHT_LVL: volumeReturn = 0.6F; break;
                default: Debug.Log("AUDIO MANAGER EXCEPTION"); break;
            }
            return volumeReturn;
        }

        #endregion
        #endregion

        #region MIXER
        public void toggleMenu(bool isMenu)
        {
            if (isMenu)
                MASTER_MIXER.SetFloat("LowPassMenu", 600.00F);
            else
            {
                MASTER_MIXER.SetFloat("LowPassMenu", 20000.00F);
            }
        }
        public void SetMusicVolume(float vol)
        {
            MASTER_MIXER.SetFloat("MusicVol", -15.0F);
        }
        public void SetSFXVolume(float vol)
        {
            MASTER_MIXER.SetFloat("SFXVol", -5.0F);
        }
        public void SetMasterPitch(float pit)
        {
            MASTER_MIXER.SetFloat("MasterPitch", pit);
        }
        #endregion
    }
}