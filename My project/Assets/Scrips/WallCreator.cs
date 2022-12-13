using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.XR;

public class WallCreator : MonoBehaviour
{
    
    public InputData inputData;

    private bool isPressing = false;
    private bool positionsStored = false;
    public bool wallCreated = false;

    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private GameObject debugCube;

    private Vector3 pos1 = new Vector3(0,0,0);
    private Vector3 pos2;

    // Start is called before the first frame update
    void Start()
    {
        inputData= GetComponent<InputData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!positionsStored)
        {
            getPositions();
        }
        else if(!wallCreated)
        {
            createWall();  
        }

        
    }

    private void getPositions()
    {
        inputData._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed);

        if (triggerPressed && !isPressing)
        {
            isPressing = true;
            inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
            if (pos1 == Vector3.zero)
            {
                pos1 = position;
                Instantiate(debugCube, pos1, Quaternion.identity);
                Debug.Log("Pos1 " + pos1);
            }
            else
            {
                pos2 = position;
                Instantiate(debugCube, pos2, Quaternion.identity);
                Debug.Log("Pos2 " + pos2);
                positionsStored = true;
            }
        }
        else if (!triggerPressed)
        {
            isPressing = false;
        }
    }
    private void createWall()
    {
        Vector3 middlePos =  Vector3.Lerp(pos2, pos1, 0.5f);
        Vector3 minPosition = Vector3.Min(pos1, pos2);
        Instantiate(debugCube, middlePos, Quaternion.identity);
        inputData._HMD.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 personPosition);
        Vector3 rotation = middlePos - personPosition;
        rotation.x = 0f;
        Instantiate(wall, minPosition, Quaternion.Euler(rotation));
        wallCreated= true;
        Vector3 scale = wall.transform.localScale;
        scale.Set(Mathf.Abs(pos2.x - pos1.x) / 2, Mathf.Abs(pos2.y - pos1.y) / 2, 1.0f);
        wall.transform.localScale = scale;
        Debug.Log("Wall created at: " + middlePos + " with scale x " + Mathf.Abs(pos2.x - pos1.x) / 2 + " y " + Mathf.Abs(pos2.y - pos1.y) / 2);
        Debug.Log(wall.transform.localScale);
    }
}
