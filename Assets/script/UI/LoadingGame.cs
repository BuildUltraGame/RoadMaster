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
        async = SceneManager.LoadSceneAsync(Globe.LOADINGSCENCE);
        async.allowSceneActivation = false;
        while (async.progress<0.9f)
        {
            Debug.Log(async.progress);
            while(progress<= async.progress)//这里不能用if
            {
                progress += 0.1f;
                setProgess(progress);
                yield return new WaitForEndOfFrame();
            }
        }
        setProgess(0.99f);
        yield return new WaitForEndOfFrame();
        async.allowSceneActivation = true;
        yield return null;
        
    }

    void setProgess(float progess)
    {
        scrollBar.value = progess;
    }

}
