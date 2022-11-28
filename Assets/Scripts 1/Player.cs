using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
public class Player : MonoBehaviour
{
    [SerializeField]
    public int playerHealth;
    public GameObject WeaponHolder;
    public static Player player;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        player = this;
        Application.targetFrameRate =60;
    }

    // Update is called once per frame
    void Update()
    {
        /* Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, 5f) && hit.collider.tag == "Usable" && TCKInput.GetAction("useBtn", EActionEvent.Down))
         {
             Debug.Log("dadad");
             Debug.Log(hit.collider.transform.gameObject.name);

             //Ambulans

                 Debug.Log("key: " + hit.transform.parent.gameObject.name);
             hit.transform.parent.gameObject.GetComponent<Ambulance>().Use(hit.collider.transform.gameObject);


         }
     }*/

    }
}
