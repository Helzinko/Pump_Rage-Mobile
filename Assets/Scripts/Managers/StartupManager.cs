using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Collections;

public class StartupManager : MonoBehaviour
{
    public static StartupManager instance;
    [SerializeField] private Slider progressBar;

    private int _delayTime = 5000;

    private float _taskCount = 2f;

    private void Start()
    {
        Invoke(nameof(Loading), 1f);
    }

    private async void Loading()
    {
        var sceneLoading = SceneManager.LoadSceneAsync((int) GameTypes.Scenes.main);
        sceneLoading.allowSceneActivation = false;
        UpdateProgress(.25f);

        await PoolManager.instance.Init();
        UpdateProgress(.25f);

        StartCoroutine(Delay(sceneLoading));
    }
    private void UpdateProgress(float value)
    {
        progressBar.value += value;
    }

    private IEnumerator Delay(AsyncOperation sceneLoading)
    {
        float seconds = 0;

        while (seconds < 1)
        {
            seconds += .1f;
            progressBar.value += .1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        sceneLoading.allowSceneActivation = true;
    }
}
