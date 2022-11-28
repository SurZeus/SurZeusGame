using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float speed = 50.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
