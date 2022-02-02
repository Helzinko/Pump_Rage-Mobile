using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int highscore { get; set; }

    public SaveData(int highscore)
    {
        this.highscore = highscore;
    }
}
