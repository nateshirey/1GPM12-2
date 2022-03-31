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

        //I start at 16/16 pictures because I want to be able to read the current photo index directly
        //for ui and counting down feels better than counting up
        currentPhoto = cameraRoll.photos.Length - 1;
    }

    //zoom input called from player input
    public void OnZoom(InputValue value)
    {
        zoomInput = value.Get<float>();
    }

    //This function is called by the Player Input component.
    // 1) It manually renders the frame as the user sees it to a render texture. 
    // 2) Then it moves a second camera to the same position and renders a second render texture that captures meshes that billboard and display their uvs for scoring purposes.
    // 3) Then it tells the data texture drawer to start a command buffer. The data texture drawer just renders the image
    //as an ID map of characters on screen which is also used for scoring.
    // 4) Then a capture event is raised for scoring, audio, etc.
    public void OnCaptureImage()
    {
        if(currentPhoto < 0)
        {
            //this happens when we fill the camera roll
        }
        else
        {
            ActivateUI(false);
            // 1)
            RenderTexture photo = cameraRoll.photos[currentPhoto];
            mainCamera.targetTexture = photo;
            mainCamera.Render();
            // 2)
            dataCam.transform.position = mainCamera.transform.position;
            dataCam.transform.rotation = mainCamera.transform.rotation;
            dataCam.Render();

            cameraRoll.photos[currentPhoto] = photo;
            mainCamera.targetTexture = null;
            // 3)
            dataTextureDrawer.ScheduleDraw(mainCamera, currentPhoto);
            //DetectTarget(currentPhoto, dataTexture);
            // 4)
            currentPhoto--;
            ActivateUI(true);
            captureEvent.Raise();
        }
    }

    private void ActivateUI(bool state)
    {
        if(cameraUI != null)
        {
            cameraUI.Active(state);
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

    ////this function is actually here to debug. It allows you to hard code a character color id and then
    ////sample it directly to see how many pixels in a texture are taken by that id
    //private void DetectTarget(int photoIndex, RenderTexture dataTex)
    //{
    //    Texture2D tex = new Texture2D(dataTex.width, dataTex.height, TextureFormat.RGB24, false);
    //    Rect imageRect = new Rect(0, 0, tex.width, tex.height);
    //    RenderTexture.active = dataTex;
    //    tex.ReadPixels(imageRect, 0, 0, false);

    //    int activePixels = 0;
    //    float alpha = 0f;
    //    for (int x = 0; x < tex.width; x++)
    //    {
    //        for (int y = 0; y < tex.height; y++)
    //        {
    //            Color col = tex.GetPixel(x, y);
    //            alpha += Mathf.RoundToInt(col.b);
    //            activePixels++;
    //        }
    //    }

    //    Debug.Log(alpha / activePixels);
    //}
}
