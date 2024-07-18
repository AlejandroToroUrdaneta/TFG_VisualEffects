using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PovAttack : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>();
    public StarterAssetsInputs _input;

    private int _currentCameraIndex = 0;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_input.changeCameraRight)
            {
                ChangeCamera(1);
                _input.changeCameraRight = false;
            }
            else if (_input.changeCameraLeft) 
            {
                ChangeCamera(-1);
                _input.changeCameraLeft = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SetActiveCamera(0);
    }

    private void ChangeCamera(int direction)
    {
        SetActiveCamera(_currentCameraIndex + direction);
    }

    private void SetActiveCamera(int index)
    {
        cameras[_currentCameraIndex].SetActive(false);
        
        _currentCameraIndex = (index + cameras.Count) % cameras.Count;
        
        cameras[_currentCameraIndex].SetActive(true);
    }
}
