using System;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Bubbler : MonoBehaviour
{
    [SerializeField] private ColorData _colorData;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject targetObject;

    private Ray debugRay;
    
    public void SpawnBubble(Bubble.BubbleType type)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        debugRay = ray;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) == false)
            return;
            
        //this.gameObject.transform.LookAt(hit.point);
        
        Quaternion rotation = Quaternion.LookRotation((hit.point - this.transform.position).normalized);

        InstantiateParameters parameters = new InstantiateParameters();
        parameters.worldSpace = true;
        parameters.parent = hit.collider.transform;
        
        GameObject target = Instantiate(targetObject, hit.point, Quaternion.identity, parameters);
        GameObject bubble = Instantiate(bubbles, this.transform.position, Quaternion.identity, hit.collider.transform);

        Vector3 scale;
        scale.x = bubble.transform.localScale.x / hit.collider.transform.localScale.x;
        scale.y = bubble.transform.localScale.y / hit.collider.transform.localScale.y;
        scale.z = bubble.transform.localScale.z / hit.collider.transform.localScale.z;

        bubble.transform.localScale = scale;
        bubble.GetComponent<Bubble>().target = target;
        bubble.GetComponent<Bubble>().type = type;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(debugRay.origin, debugRay.direction, Color.red);
    }
}
