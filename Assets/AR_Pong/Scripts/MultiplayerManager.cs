using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    #region Public Variables
    public static MultiplayerManager Instance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
