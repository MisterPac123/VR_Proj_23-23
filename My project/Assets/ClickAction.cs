using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAction : MonoBehaviour, IPointerClickHandler
{
    public GameObject wall;
    public Material material;
  
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        wall.GetComponent<MeshRenderer>().material = material;
    }
}
