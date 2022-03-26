using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("User Parameters")]
    public Vector2 zoomLimits;

    [Header("Components")]
    public RenderTexture dataTexture;
    public DataTextureDrawer dataTextureDrawer;
    public CameraRoll cameraRoll;
    public CameraUI cameraUI;
    public Camera mainCamera;
    public Camera dataCam;
    public GameEvent captureEvent;
    private CinemachineVirtualCamera virtualCamera;

    private int currentPhoto = 0;
    private float zoomInput = 0f;
    private float zoomValue;

    private void Awake()
    {
        virtualCamera = this.GetComponent<CinemachineVirtualCamera>();
        zoomValue = zoomLimits.y;

        currentPhoto = cameraRoll.photos.Length - 1;
    }

    public void OnZoom(InputValue value)
    {
        zoomInput = value.Get<float>();
    }

    public void OnCaptureImage()
    {
        if(currentPhoto < 0)
        {
            //this happens when we fill the camera roll
        }
        else
        {
            cameraUI.Active(false);

            RenderTexture photo = cameraRoll.photos[currentPhoto];
            mainCamera.targetTexture = photo;
            mainCamera.Render();

            dataCam.transform.position = mainCamera.transform.position;
            dataCam.transform.rotation = mainCamera.transform.rotation;
            dataCam.Render();

            cameraRoll.photos[currentPhoto] = photo;
            mainCamera.targetTexture = null;

            dataTextureDrawer.ScheduleDraw(mainCamera, currentPhoto);
            //DetectTarget(currentPhoto, dataTexture);

            currentPhoto--;
            cameraUI.Active(true);
            captureEvent.Raise();
        }
    }

    void Update()
    {
        zoomValue -= zoomInput;
        zoomValue = Mathf.Clamp(zoomValue, zoomLimits.x, zoomLimits.y);
        if(virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = zoomValue;
        }
    }

    private void DetectTarget(int photoIndex, RenderTexture dataTex)
    {
        Texture2D tex = new Texture2D(dataTex.width, dataTex.height, TextureFormat.RGB24, false);
        Rect imageRect = new Rect(0, 0, tex.width, tex.height);
        RenderTexture.active = dataTex;
        tex.ReadPixels(imageRect, 0, 0, false);

        int activePixels = 0;
        float alpha = 0f;
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                Color col = tex.GetPixel(x, y);
                alpha += Mathf.RoundToInt(col.b);
                activePixels++;
            }
        }

        Debug.Log(alpha / activePixels);
    }
}
