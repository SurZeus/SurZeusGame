using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDisableAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(DisableUI());
    }

    private void OnDisable()
    {
        StopCoroutine(DisableUI());
    }

    IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
