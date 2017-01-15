using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// 铁路上自动移动脚本,只要带这个脚本的物体都可以在铁路上自动移动
/// </summary>
public class Railwaymovable : MonoBehaviour {

    private float speed = 5f;//速度

    public List<Vector3> points = new List<Vector3>();

    private Railway roadStand=null;



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
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(points[0].x, transform.position.y, points[0].z), speed * Time.deltaTime);
            }
        }
	}


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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.RAILWAY)
        {
            roadStand = collision.gameObject.GetComponent<Railway>();

        }
    }

    public void printRoad()
    {
        foreach(Vector3 v in points){
            print(v);

        }
    }
   



    
}
