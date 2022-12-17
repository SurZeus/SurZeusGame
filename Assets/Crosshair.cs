using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{ 
   
    // Star is called before the first frame update
   

    // Update is called once per frame
   /* void Update()
    {
        // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100.0f, Color.yellow);
        if (Camera.main != null)
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, layer);
        if (currentOutline == null)
            currentOutline = hit.collider.gameObject.GetComponent<Outline>();
        else
        {
            currentOutline.OutlineWidth = 0;
            currentOutline = hit.collider.gameObject.GetComponent<Outline>();
        }
        if (currentOutline != null)
            currentOutline.OutlineWidth = 10;
    }*/
}
