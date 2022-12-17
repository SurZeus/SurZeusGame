using UnityEngine;

public class OnMouseOverAtribute : MonoBehaviour
{
    Outline outline;
    public LayerMask layer;
    Ray pickUpRay;
    RaycastHit hit;
    private void Update()
    {
       
   
    }
    private void Awake()
    {
        
        Debug.Log("xD");
        outline = this.GetComponent<Outline>();
    }
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        outline.enabled = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        outline.enabled = false;
    }
}