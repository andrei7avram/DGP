using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoarding : MonoBehaviour
{
   private Camera mainCamera;

void Start()
{
    mainCamera = Camera.main;
     Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

}

void LateUpdate()
{
    transform.rotation = mainCamera.transform.rotation;
}
}
