using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
public class Sway : MonoBehaviour
{
    // Start is called before the first frame update
    public float intensity;
    public float smooth;
    public Quaternion _originRotation;

    private void Start()
    {
        _originRotation = transform.localRotation;
    }
    private void Update()
    {
        UpdateSway();
    }

    private void UpdateSway()
    {
        float t_x_mouse = TCKInput.GetAxis("Touchpad").x;
        float t_y_mouse = TCKInput.GetAxis("Touchpad").y;

       Quaternion t_x_adj = Quaternion.AngleAxis(intensity * t_x_mouse, Vector3.up);
       Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion targetRotation = _originRotation * t_x_adj * t_y_adj;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);

        
    }
}
