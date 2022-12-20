using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPaperButton : MonoBehaviour
{
    public GameObject wallpaperPicker;
    private bool wallpaperPickerState = false;
    // Start is called before the first frame update
    void Start()
    {
        wallpaperPicker = GameObject.Find("ColorPicker_cvs");
        wallpaperPicker.SetActive(false);
    }

    public void onButtonClick()
    {

        wallpaperPickerState = !wallpaperPickerState;
        wallpaperPicker.SetActive(wallpaperPickerState);
        Debug.Log("Button clicked");
    }
}
