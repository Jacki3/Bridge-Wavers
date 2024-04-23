using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOperatedZone : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The corresponding zone that determines if the player is in the right location")]
    private WaveOperatedZone playerZone;

    [SerializeField]
    private string currentPlayerName; //eventually a dict or array for multiplayer

    public bool CheckCorrespodingZone(string playerName)
    {
        //IF there is a player zone to check against and this zone matches the player name (s)
        if(playerZone == null)
        {
            return false;
        }
        if (playerZone.currentPlayerName != playerName)
        {
            return false;
        }
        return true;
    }

    public void SetPlayerName(string playerName) => currentPlayerName = playerName;
}
