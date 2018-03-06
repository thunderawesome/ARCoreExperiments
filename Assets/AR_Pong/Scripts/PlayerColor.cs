// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Let the player input their color to be saved a network custom property, viewed by alls players above each  when in the same room. 
// </summary>
// <author>adam_s</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using Photon;
using ExitGames.Client.Photon;

namespace Battlerock
{
    /// <summary>
    /// Player color. Let the user input their, will overide above the player's puck and goal in the game.
    /// </summary>
    [RequireComponent(typeof(Color))]


    public class PlayerColor : PunBehaviour
    {

        #region Private Variables
        private const string ColorProp = "PlayerColor";
        #endregion

        #region Public Variables
        /// <summary>
        /// Color this player selected. Defaults to grey.
        /// </summary>
        public Color MyColor = Color.grey;
        #endregion

        #region Public Methods

        /// <summary>
        /// Set the players color as a Player Custom Property the the PhotonNetwork
        /// </summary>
        public void SetPlayerColor()
        {
            MyColor = GetComponent<ColorPicker>().CurrentColor;

            Hashtable setPlayerColor = new Hashtable() { { ColorProp, MyColor } };
            PhotonNetwork.player.SetCustomProperties(setPlayerColor);

        }

        #endregion
    }
}
