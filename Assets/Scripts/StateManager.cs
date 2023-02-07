using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateManager
{
    public enum State
    {
        Menu,
        Playing,
        EndGame
    }

    public static bool paused;

    public static State gameState;
}
