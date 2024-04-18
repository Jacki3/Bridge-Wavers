using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    bool IsBridge();

    int GetCarScore();

    void CarWaved();
}
