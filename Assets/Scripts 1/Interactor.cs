using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting { get; private set; }

    private void Update()
    {

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            //if (Physics.Raycast(raycast, out raycastHit))
            if (Physics.Raycast(raycast, out raycastHit, 3f, LayerMask.GetMask("Item")))
            {
                if (raycastHit.collider.CompareTag("Item"))
                {

                    raycastHit.collider.gameObject.GetComponent<ItemPickUp>().PickUpItem(GameManager.Instance.player.playerInventory);
                }
            }
            if (Physics.Raycast(raycast, out raycastHit, 3f, LayerMask.GetMask("Interactable")))
            {
                if (raycastHit.collider.CompareTag("Chest"))
                {
                    var interactable = raycastHit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        StartInteraction(interactable);
                    }
                    //raycastHit.collider.gameObject.GetComponent<ItemPickUp>().PickUpItem(GameManager.Instance.player.playerInventory);
                }
            }

        }

       /* var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            for(int i =0; i< colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if(interactable != null)
                {
                    StartInteraction(interactable);
                }
            }
        }*/
    }
    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccesful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;

    }
}
