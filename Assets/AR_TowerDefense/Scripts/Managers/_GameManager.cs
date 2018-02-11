using UnityEngine;

public class _GameManager : Singleton<_GameManager>
{
    #region Public Variables

    #endregion

    #region Private Variables

    #endregion

    #region Public Methods
    public void GameOver()
    {

    }

    public void Win()
    {

    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        InitializeSubSystems();
    }

    private void InitializeSubSystems()
    {
        if(Battlerock.WavesController.Instance == null)
        {
            GameObject waveSubSystem = new GameObject();
            waveSubSystem.name = "_WavesController_";
            waveSubSystem.AddComponent<Battlerock.WavesController>();
            waveSubSystem.transform.parent = transform;
        }

        if(Battlerock.AudioController.Instance == null)
        {
            GameObject audioSubSystem = new GameObject();
            audioSubSystem.name = "_AudioController_";
            audioSubSystem.AddComponent<Battlerock.AudioController>();
            audioSubSystem.transform.parent = transform;
        }
    }
    #endregion
}
