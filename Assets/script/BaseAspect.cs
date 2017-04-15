using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(UICamera))]
public class BaseAspect : MonoBehaviour
{
     float standard_width = 1280f;
     float standard_height = 720f;
     float device_width = 0f;
     float device_height = 0f;

     private Camera camera;

     void Awake()
     {
         device_width = Screen.width;
         device_height = Screen.height;
          camera=GetComponent<Camera>();
         SetCameraSize();
     }
 
     private void SetCameraSize()
     {
         float adjustor = 0f;
         float standard_aspect = standard_width / standard_height;
         float device_aspect = device_width / device_height;

         if (device_aspect < standard_aspect)
         {
             adjustor = standard_aspect / device_aspect;
             camera.orthographicSize = standard_height/100/2;
         }
         else {
             camera.orthographicSize = device_height / 100 / 2;
            
         
         }
     }


}