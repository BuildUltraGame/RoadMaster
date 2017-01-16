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
        if (other.gameObject.tag == "moveObj")
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

        if (Vector3.Distance(collider.transform.position, pointList[0]) <
            Vector3.Distance(collider.transform.position, pointList[pointList.Count - 1]))
        {
            obj.addRoadsPoint(new List<Vector3>(pointList));

        }
        else
        {
            List<Vector3> vs = new List<Vector3>(pointList);
            vs.Reverse();
            obj.addRoadsPoint(vs);
        }

     //   obj.printRoad();
    
    }

   
    

    
}
