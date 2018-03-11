// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Let the player input their color to be saved a network custom property, viewed by alls players above each  when in the same room. 
// </summary>
// <author>adam_s</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using Photon;
using ExitGames.Client.Photon;
using ExitGames.Demos.DemoAnimator;

namespace Battlerock
{
    /// <summary>
    /// Player color. Let the user input their, will overide above the player's puck and goal in the game.
    /// </summary>
    [RequireComponent(typeof(Color))]
    public class PlayerColor : PunBehaviour
    {
        #region Private Variables
        private string playerColorPrefKey = "PlayerColor";
        #endregion

        #region Public Variables
        /// <summary>
        /// Color this player selected. Defaults to grey.
        /// </summary>
        public Vector3 myColor;
        #endregion

        #region Public Methods

        /// <summary>
        /// Set the players color as a Player Custom Property the the PhotonNetwork
        /// </summary>
        public void SetPlayerColor()
        {
            playerColorPrefKey = PhotonNetwork.playerName;
            var currentColor = GetComponent<ColorPicker>().CurrentColor;

            ES3.Save<Color>(playerColorPrefKey, currentColor);
        }

        #endregion
    }
}
