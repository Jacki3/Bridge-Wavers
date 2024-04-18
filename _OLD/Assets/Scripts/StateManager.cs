using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateManager
{
    public enum State
    {
        Menu,
        Playing,
        EndGame,
        Paused
    }

    public static bool paused;

    public static State gameState;
}
