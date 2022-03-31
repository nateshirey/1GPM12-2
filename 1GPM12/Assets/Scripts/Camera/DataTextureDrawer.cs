using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//This class uses a command buffer to draw a special scoring id map of the scene
public class DataTextureDrawer : MonoBehaviour
{
    public RenderTexture dataTexture;
    public Material dataMat;
    public Material blitMat;
    public Material blitFlipMat;

    CommandBuffer commandBuffer;
    public CameraEvent camEvent = CameraEvent.AfterImageEffects;

    const string bufferName = "DataTexBuffer";

    public int freezeFrames = 2;
    public int frame;
    public bool addBuffer = false;
    public bool drawing = false;

    Camera cam;
    int currentPhotoIndex;

    private List<MeshRenderer> stuffToDraw = new List<MeshRenderer>();

    public List<MeshRenderer> environmentMeshes;

    public string dataLayerName = "Characters";
    int dataLayerInt;

    public GameEventInt dataReadyEvent;

    private void Awake()
    {
        dataLayerInt = LayerMask.NameToLayer(dataLayerName);
    }

    //This method is called by the camera controller to add a command buffer on the current frame
    public void ScheduleDraw(Camera newCam, int photoIndex)
    {
        //set the number of frames we want to run the buffer for
        frame = freezeFrames;
        addBuffer = true;
        cam = newCam;
        currentPhotoIndex = photoIndex;
    }

    public void OnPreRender()
    {
        if (addBuffer)
        {
            addBuffer = false;
            drawing = true;
            //create the command buffer, then add it to the render list at camEvent time
            DrawDataTexture();
            cam.AddCommandBuffer(camEvent, commandBuffer);
        }
    }

    public void OnPostRender()
    {
        if (drawing)
        {
            frame--;

            if (commandBuffer != null && frame < 0)
            {
                //reset everything and then put out an event saying the data is ready
                cam.RemoveCommandBuffer(camEvent, commandBuffer);
                commandBuffer.Clear();
                cam = null;
                drawing = false;
                dataReadyEvent.Raise(currentPhotoIndex);
            }
        }
    }

    public void DrawDataTexture()
    {
        //make sure we have characters to draw and something to draw them with
        if(CharacterManager.Instance != null && dataMat != null)
        {

            if(commandBuffer == null)
            {
                commandBuffer = new CommandBuffer {name = bufferName };
            }

            //load meshes into our draw list
            stuffToDraw.Clear();
            foreach (MeshRenderer mesh in environmentMeshes)
            {
                stuffToDraw.Add(mesh);
            }
            foreach (MeshRenderer mesh in CharacterManager.Instance.CharacterDataMeshes)
            {
                stuffToDraw.Add(mesh);
            }

            RenderTextureDescriptor textureDescriptor = new RenderTextureDescriptor
            {
                width = dataTexture.width,
                height = dataTexture.height,

                depthBufferBits = 24,
                useMipMap = false,
                autoGenerateMips = false,
                dimension = TextureDimension.Tex2D,
                sRGB = false,
                graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R16G16B16A16_UNorm,
                msaaSamples = 1
            };

            //make a temp render texture
            int idBufferID = Shader.PropertyToID("IDBuffer");
            commandBuffer.GetTemporaryRT(idBufferID, textureDescriptor);
            commandBuffer.SetRenderTarget(idBufferID);

            //clear the camera view
            commandBuffer.ClearRenderTarget(false, true, Color.clear);

            //draw each mesh, if its a character use a material that draws vertex colors, otherwise draw black
            foreach (MeshRenderer rend in stuffToDraw)
            {
                Material objectMat = rend.gameObject.layer == dataLayerInt ? dataMat : blitMat;
                commandBuffer.DrawRenderer(rend, objectMat);
            }

            //empty the dataTexture and draw the newly created idTexture into it
            commandBuffer.SetRenderTarget(dataTexture);
            commandBuffer.ClearRenderTarget(false, true, Color.clear);
            commandBuffer.Blit(idBufferID, dataTexture, blitFlipMat);
            commandBuffer.ReleaseTemporaryRT(idBufferID);

            //draw white to simulate a camera flash
            commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
            commandBuffer.ClearRenderTarget(false, true, Color.white);
        }
    }
}
