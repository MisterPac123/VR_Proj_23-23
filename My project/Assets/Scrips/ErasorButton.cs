using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ErasorButton : MonoBehaviour
{
    private Marker marker;
    private GameObject rController;
    private int n;
    private int pensize;
    private Color[] init_colors;
    private bool pressed;
    public Button btn;

    void Start()
    {
        rController = GameObject.Find("RightController");
        marker = rController.GetComponent<Marker>();
        pressed = false;
    }


    public void OnButtonPress()
    {
        pensize = marker._pensize;

        if (pressed == false)
        {
            pressed = true;
            init_colors = marker._colors;
            marker._colors = Enumerable.Repeat(Color.white, pensize * pensize).ToArray();
        }

        else if (pressed == true)
        {
            pressed = false;
            marker._colors = init_colors;

        }
        n++;

        Debug.Log("Button clicked " + n + " times.");

    }
}

