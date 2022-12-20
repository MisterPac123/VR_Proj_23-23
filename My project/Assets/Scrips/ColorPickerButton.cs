using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class ColorPickerButton : MonoBehaviour
{

    public FlexibleColorPicker fcp;
    public GameObject colorPicker;
    private bool colorPickerState = false;
    private GameObject rController;
    private Marker marker;

    // Start is called before the first frame update
    void Start()
    {
        colorPicker = GameObject.Find("ColorPicker_cvs");
        colorPicker.SetActive(false);
        rController = GameObject.Find("RightController");
        marker = rController.GetComponent<Marker>();
    }

    // Update is called once per frame
    void Update()
    {
        int pensize = marker._pensize;
        marker._colors = Enumerable.Repeat(fcp.color, pensize * pensize).ToArray();
        Debug.Log(marker._colors);
    }

    public void onButtonClick()
    {

        colorPickerState = !colorPickerState;
        colorPicker.SetActive(colorPickerState);
        Debug.Log("Button clicked");
    }
}
