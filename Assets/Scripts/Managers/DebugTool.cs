using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    public void Restart()
    {
        GameManager._instance.RestartGameplay();
    }
}
