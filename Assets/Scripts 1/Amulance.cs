using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulance : MonoBehaviour
{

    public GameObject frontLeftDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("xDD");
        if(other.tag=="Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                OpenLeftDoor();
            }
        }
    }

    public void Use()
    {

    }
    public void OpenLeftDoor()
    {
        if (!frontLeftDoor.GetComponent<Animator>().GetBool("Open"))
        {
            frontLeftDoor.GetComponent<Animator>().SetBool("Open", true);
        }
        else
        {
            frontLeftDoor.GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
