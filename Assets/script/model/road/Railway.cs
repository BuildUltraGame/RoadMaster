using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class Railway : MonoBehaviour {

    protected List<Vector3> pointList = new List<Vector3>();


	// Use this for initialization
    protected void Start () {
		Transform[] ts=GetComponentsInChildren<Transform>();
       
        foreach(Transform t in ts){
            pointList.Add(t.position);

        }
        pointList.Remove(transform.position);

       
	}

   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.VEHICLE)
        {
            print(name);
            OnObjEnter(other);


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.VEHICLE)
        {

            Railwaymovable obj = other.gameObject.GetComponent<Railwaymovable>();
            obj.removeRoadsPoint(pointList);
        }

    }


    virtual protected void OnObjEnter(Collider collider)
    {
        Railwaymovable obj = collider.gameObject.GetComponent<Railwaymovable>();
        if(obj!=null){
            getRoad(obj,collider.transform.position);
        }

  

     //   obj.printRoad();
    
    }


    public void getRoad(Railwaymovable r,Vector3 v)
    {

        if (FromWhichPoint(pointList.ToArray(), r.transform.position) == 0)
        {
            r.addRoadsPoint(new List<Vector3>(pointList));
        }
        else
        {
            List<Vector3> vs = new List<Vector3>(pointList);
            vs.Reverse();
            r.addRoadsPoint(vs);
        }
    }


    /// <summary>
    /// 离数组中哪个点位置最近
    /// </summary>
    /// <param name="vs">数组</param>
    /// <param name="v">点</param>
    /// <returns></returns>
    public static int FromWhichPoint(Vector3[] vs, Vector3 v)
    {


        float minDistance = 99;
        int minNum = 0;
        for (int i = 0; i < vs.Length; i++)
        {
            float tempDist = Vector3.Distance(v, vs[i]);
            if (tempDist < minDistance)
            {
                minDistance = tempDist;
                minNum = i;
            }
        }

        return minNum;
    }
    

    
}
