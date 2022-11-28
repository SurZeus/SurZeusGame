using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemyToGetHealthFrom;
    public Image image;
    void Start()
    {
        
        image.fillAmount = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = enemyToGetHealthFrom.Health / 100;
    }
}
