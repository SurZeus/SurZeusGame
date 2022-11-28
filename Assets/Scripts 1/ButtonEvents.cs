using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField]
    private Terrain terrain1;

    [SerializeField]
    private Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnEnable()
    {
       // playerCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuButton()
    {
        Debug.Log("menu button presssed");
    }




    //DevMenuOptions

    public void GrassDistance(System.Single x)
    {
        Terrain.activeTerrain.detailObjectDistance = x;
    }

    public void TreeDistance(System.Single y)
    {
        Terrain.activeTerrain.treeDistance = y;
    }

    public void RenderDistance(System.Single y)
    {
      //  playerCamera.farClipPlane = y;
    }

}
