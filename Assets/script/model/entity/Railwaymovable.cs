using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// 铁路上自动移动脚本,只要带这个脚本的物体都可以在铁路上自动移动
/// 使用方法:
/// 1. 物体挂载本脚本
/// 2.设置物体的层为載具层
/// 3.物体要与铁路接触
/// </summary>
public class Railwaymovable : MonoBehaviour {

    public float speed = 5f;//速度

    public List<Vector3> points = new List<Vector3>();

    private Railway roadStand=null;//当前所在路
    private Railway lastRoad = null;//前一条所在路,用于回头



	// Use this for initialization
	void Start () {
       
	}
 
	// Update is called once per frame
	void Update () {

        if(points.Count>=1){

            Vector3 v2 = new Vector3(points[0].x, transform.position.y, points[0].z);
            
            if (Vector3.Distance(transform.position, v2) < 0.09f)
            {
                points.RemoveAt(0);

            }
            else
            {
                LookAtHorizontal(points[0]);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(points[0].x, transform.position.y, points[0].z), speed * Time.deltaTime);
            }
        }
	}


    /// <summary>
    /// 设置转向,使得车头永远指向行车方向
    /// TODO:目前有bug
    /// </summary>
    /// <param name="v">要面对的方向</param>
    private void LookAtHorizontal(Vector3 v)
    {
        transform.LookAt(new Vector3(v.x,transform.position.y,v.z));
    }


    public void addRoadsPoint(List<Vector3> points)
    {
        this.points.AddRange(points);  
    }

    public void addRoadPoint(Vector3 point)
    {
        this.points.Add(point);
    }

    public void removeRoadsPoint(List<Vector3> points)
    {
        foreach(Vector3 v in points){
            this.points.Remove(v);            
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == Tags.RAILWAY)
        {
            lastRoad = roadStand;
            roadStand = collision.gameObject.GetComponent<Railway>();            

            

        }
    }
    /// <summary>
    /// 回头
    /// </summary>
    public void Back()
    {
        lastRoad.getRoad(this,transform.position);
    }

    /// <summary>
    /// 测试用,打印路线
    /// TODO:记得删除
    /// </summary>
    public void printRoad()
    {
        foreach(Vector3 v in points){
            print(v);

        }
    }
   



    
}
