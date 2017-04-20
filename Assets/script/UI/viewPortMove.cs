using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使用时需要挂载在mainCamera上面
/// 通过触摸移动视口，可以在inspector面板配置速度，最大上下左右偏移
/// 还有一个reset回初始位置的接口
/// </summary>
public class viewPortMove : MonoBehaviour {
    private GameObject mainCamera;
    public float speed = -1f;
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
            offsetZ = (touchposition.y * speed);

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
}
