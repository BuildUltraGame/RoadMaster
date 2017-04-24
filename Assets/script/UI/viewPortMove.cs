using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cameraEulerAnglesMode//如果是一个能够正常玩的摄像机视口，欧拉角有四种方式(还有别的方式，但那些玩不了，比如摄像机朝天)
{
    XminusZminus = 0,//抱歉，这个我还没想好如何描述
    ZXminus = 1,
    XZ = 2,
    ZminusX = 3
}

/// <summary>
/// 使用时需要挂载在mainCamera上面
/// 通过触摸移动视口，可以在inspector面板配置速度，最大上下左右偏移
/// 还有一个reset回初始位置的接口
/// </summary>
public class viewPortMove : MonoBehaviour {
    private GameObject mainCamera;
    public float speed = 1f;
    private float speedX;
    private float speedZ;
    Vector3 cameraInitPos;
    private float totalDistanceX = 0f;
    private float totalDistanceZ = 0f;
    float offsetX = 0f;
    float offsetZ = 0f;
    public float maxDistancePositiveX = 50f;//X轴最大正向移动距离
    public float maxDistancePositiveZ = 50f;//Z轴最大正向移动距离
    public float maxDistanceMinusX = -50f;//X轴最大负向移动距离
    public float maxDistanceMinusZ = -50f;//Z轴最大负向移动距离
    private bool canMoveX = true;
    private bool canMoveZ = true;
    int count;
    private cameraEulerAnglesMode cameraEulerState;
    private bool changeXZ = false;
    
    
    Vector3 touchposition;

    public static viewPortMove _instance;

    void Awake()
    {
        _instance = this;
        judgeCameraRoateMode();
        setCameraMoveMode();

    }

    void Start()
    {
        mainCamera = this.GetComponent<Camera>().gameObject;
        cameraInitPos = mainCamera.transform.position;

    }

    void Update()
    {
        if (Physics.Raycast(UICamera.mainCamera.ScreenPointToRay(Input.mousePosition), 20))//检测是否点击到NGUI
        {
            return;
        }
        moveCameraWithMouse();
       
    }

    void moveCameraWithMouse()
    {
        if (Input.touchCount > 0)
        {
            count += Input.touchCount;
        }
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            touchposition = Input.GetTouch(0).deltaPosition;

           if (changeXZ)
            {
                offsetZ = (touchposition.x * speedZ);
                offsetX = (touchposition.y * speedX);
            }
            else
            {
                offsetX = (touchposition.x * speedX);
                offsetZ = (touchposition.y * speedZ);
            }
            
            canMoveX = ((totalDistanceX + offsetX) <= maxDistancePositiveX) && ((totalDistanceX + offsetX) >= maxDistanceMinusX);
            canMoveZ = ((totalDistanceZ + offsetZ) <= maxDistancePositiveZ) && ((totalDistanceZ + offsetZ) >= maxDistanceMinusZ);
            if (canMoveX & canMoveZ)
            {
                mainCamera.transform.Translate(offsetX, 0, offsetZ, Space.World);
                totalDistanceX += offsetX;
                totalDistanceZ += offsetZ;
            }
            else if (canMoveX & !canMoveZ)
            {
                mainCamera.transform.Translate(offsetX, 0, 0 ,Space.World);
                totalDistanceX += offsetX;
            }
            else if (!canMoveX & canMoveZ)
            {
                mainCamera.transform.Translate(0, 0 , offsetZ, Space.World);
                totalDistanceZ += offsetZ;
            }
            else
            {
                //do nothing;
            }
        }
    }

    public void resetCamera()
    {
        mainCamera.transform.position = cameraInitPos;
    }

    void judgeCameraRoateMode()
    {
        float angleY = this.gameObject.transform.eulerAngles.y;
        if (((angleY >= 0) && (angleY < 45)) || (angleY >= 315))
        {
            cameraEulerState = cameraEulerAnglesMode.XminusZminus;
        }
        else if ((angleY >= 45) && (angleY < 135))
        {
            cameraEulerState = cameraEulerAnglesMode.ZXminus;
        }
        else if ((angleY >= 135) && (angleY < 225))
        {
            cameraEulerState = cameraEulerAnglesMode.XZ;
        }
        else if ((angleY >= 225) && (angleY < 315))
        {
            cameraEulerState = cameraEulerAnglesMode.ZminusX;
        }
    }

    void setCameraMoveMode()
    {
        switch (cameraEulerState)
        {
            case cameraEulerAnglesMode.XminusZminus:
                speedX = speed * -1;
                speedZ = speedX * -1;
                break;
            case cameraEulerAnglesMode.ZXminus:
                speedX = speed * -1;
                speedZ = speed;
                break;
            case cameraEulerAnglesMode.XZ:
                speedX = speed;
                speedZ = speedX;
                break;
            case cameraEulerAnglesMode.ZminusX:
                speedX = speed;
                speedZ = speedX * -1;
                break;
        }
        if ((cameraEulerState == cameraEulerAnglesMode.ZXminus) || (cameraEulerState == cameraEulerAnglesMode.ZminusX))
        {
            changeXZ = true;
        }
    }
}
