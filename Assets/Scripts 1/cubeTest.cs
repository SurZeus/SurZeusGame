using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeTest : MonoBehaviour
{
    //1 = left right/ 2= roatate
    public int cubeState;
    [SerializeField]
    private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        cubeState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(cubeState == 1)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (cubeState == 2)
        {
            anim.SetBool("isRotating", true);
        }
        else
        {
            anim.SetBool("isRotating", false);
        }
    }
}
