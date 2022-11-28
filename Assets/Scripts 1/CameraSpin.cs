using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.eulerAngles.x > -7)
        {
            transform.Rotate(Vector3.left * speed * Time.deltaTime);
            Debug.Log(transform.eulerAngles.x);
        }
        
    }
}
