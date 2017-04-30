using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 异步加载场景脚本
/// 使用方法：先设置Globe.LOADINGSCENCE，然后加载场景LoadingGame
/// </summary>
public class LoadingGame : MonoBehaviour {

    AsyncOperation async;
    private float progress = 0;//场景加载到多少了
    public UIScrollBar scrollBar;

    void Start()
    {
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        async = SceneManager.LoadSceneAsync("wf");
        yield return async;
    }

    void Update()
    {
        scrollBar.value = progress;
    }


}
