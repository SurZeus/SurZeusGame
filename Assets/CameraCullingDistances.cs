using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCullingDistances : MonoBehaviour
{
    private void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[16] = 50;
        distances[13] = 50;

        camera.layerCullDistances = distances;
    }
}
