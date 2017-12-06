using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class notifyAddGold : MonoBehaviour,IListener<ScoreAddEvent>
{

    private UISprite coin;

    private int numOfAddCoin = 0;

    // Use this for initialization
    void Start()
    {
        UnityEventCenter.Register<ScoreAddEvent>(this);
        coin = gameObject.GetComponent<UISprite>();
        coin.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfAddCoin!=0) {
            //闪烁
            coin.alpha = coin.alpha + 1;
            if (coin.alpha>=255)
            {
                coin.alpha = 0; 
                numOfAddCoin--;
            }
        }

    }

    public void Handle(ScoreAddEvent message)
    {
        if (message.getOwner() == GameobjBase.PLAYER)
        {
            numOfAddCoin++;
        }

    }


    void OnDisable()
    {
        UnityEventCenter.UnRegister<ScoreEvent>(this);
        UnityEventCenter.UnRegister<cancelMountainEvent>(this);
    }

 
}
