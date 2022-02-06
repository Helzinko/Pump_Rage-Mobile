using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTypes : MonoBehaviour
{
    public enum Bullets
    {
        normal = 0,
        shotgunShell = 1,
    }

    public enum Enemies
    {
        normal = 0,
        shooting = 1,
    }
    
    public enum Sounds
    {
        effect = 0,
        music = 1,
    }

    public enum Scenes
    {
        startup = 0,
        main = 1,
    }
}
