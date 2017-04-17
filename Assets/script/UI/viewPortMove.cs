using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使用时需要挂载在mainCamera上面
/// 通过触摸移动视口，可以在inspector面板配置速度，最大上下左右偏移
/// 还有一个reset回初始位置的接口
/// </summary>
public class viewPortMove : MonoBehaviour {
    public GameObject mainCamera;
    private float speed = -0.1f;
    Vector3 cameraInitPos;
    public float totalDistanceX = 0f;
    public float totalDistanceY = 0f;
    float offsetX = 0f;
    float offsetY = 0f;
    public float maxDistancePositiveX = 5f;
    public float maxDistancePositiveY = 5f;
    public float maxDistanceMinusX = -5f;
    public float maxDistanceMinusY = -5f;
    public bool canMoveX = true;
    public bool canMoveY = true;
    int count;
    Vector3 touchposition;

    public static viewPortMove _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        mainCamera = this.GetComponent<Camera>().gameObject;
        cameraInitPos = mainCamera.transform.position;

    }

    void Update()
    {
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
            offsetX = (touchposition.x * speed);
            offsetY = (touchposition.y * speed);

            canMoveX = ((totalDistanceX + offsetX) <= maxDistancePositiveX) && ((totalDistanceX + offsetX) >= maxDistanceMinusX);
            canMoveY = ((totalDistanceY + offsetY) <= maxDistancePositiveY) && ((totalDistanceY + offsetY) >= maxDistanceMinusY);
            if (canMoveX & canMoveY)
            {
                mainCamera.transform.Translate(offsetX, offsetY, 0);
                totalDistanceX += offsetX;
                totalDistanceY += offsetY;
            }
            else if (canMoveX & !canMoveY)
            {
                mainCamera.transform.Translate(offsetX, 0, 0);
                totalDistanceX += offsetX;
            }
            else if (!canMoveX & canMoveY)
            {
                mainCamera.transform.Translate(0, offsetY, 0);
                totalDistanceY += offsetY;
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
}
