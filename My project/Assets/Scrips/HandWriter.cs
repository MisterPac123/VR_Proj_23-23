using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace Assets
{
    public class HandWriter : MonoBehaviour
    {
        private WallCreator wallCreater;
        private InputData inputData;
        private GameObject wall;
        private bool controllerCalibrated;
        [SerializeField]
        private GameObject debugSphere;
        [SerializeField]
        private Marker marker;
        private Vector3 wallDirection;

        private float distance = 0.0f;
        // Use this for initialization
        void Start()
        {
            wallCreater = GetComponent<WallCreator>();
            inputData = GetComponent<InputData>();
        }

        // Update is called once per frame
        void Update()
        {
            if (/*wallCreater.wallCreated &&*/ !controllerCalibrated)
            {
                wall = GameObject.Find("WritableWall");
                calibrateController();
            } else
            {
                writeOnWall();
            }
        }
        
        private void calibrateController()
        { 
            inputData._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);
            if(gripPressed) {
                inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition);
                wallDirection = wall.transform.GetChild(0).transform.up * -1;
                RaycastHit hit;
                if (Physics.Raycast(controllerPosition, wallDirection, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(controllerPosition, wallDirection * hit.distance, Color.yellow);
                    distance = hit.distance;
                    Debug.Log("Did Hit " + hit.point + " distance " + distance);
                    controllerCalibrated = true;
                } else
                {
                    Debug.DrawRay(controllerPosition, wallDirection * 1000, Color.black);
                    Debug.Log("Did not Hit " + wallDirection);
                }
            }  
        }
        private void writeOnWall()
        {
            inputData._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed);
            if (gripPressed)
            {
                inputData._rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 controllerPosition);
                RaycastHit hit;
                if (Physics.Raycast(controllerPosition, wallDirection, out hit, distance + 0.05f))
                {
                    marker.Draw(hit);
                }
            }
        }
    }
}