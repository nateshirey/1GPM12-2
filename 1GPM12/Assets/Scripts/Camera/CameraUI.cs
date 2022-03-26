using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraUI : MonoBehaviour
{
    public TMP_Text cameraRollUi;
    public GameObject crossHair;

    private void Awake()
    {
        SetCameraRollAmount(16);
    }

    public void Active(bool active)
    {
        crossHair.SetActive(active);
        cameraRollUi.gameObject.SetActive(active);
    }

    public void SetCameraRollAmount(int currentPhoto)
    {
        int photoIndex = currentPhoto++;
        cameraRollUi.text = photoIndex.ToString() + "/16";
    }
}
