using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    public void ScheduleDraw(Camera newCam, int photoIndex)
    {
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
        if(CharacterManager.Instance != null && dataMat != null)
        {

            if(commandBuffer == null)
            {
                commandBuffer = new CommandBuffer {name = bufferName };
            }

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

            int idBufferID = Shader.PropertyToID("IDBuffer");
            commandBuffer.GetTemporaryRT(idBufferID, textureDescriptor);
            commandBuffer.SetRenderTarget(idBufferID);

            commandBuffer.ClearRenderTarget(false, true, Color.clear);

            foreach (MeshRenderer rend in stuffToDraw)
            {
                Material objectMat = rend.gameObject.layer == dataLayerInt ? dataMat : blitMat;
                commandBuffer.DrawRenderer(rend, objectMat);
            }

            commandBuffer.SetRenderTarget(dataTexture);
            commandBuffer.ClearRenderTarget(false, true, Color.clear);
            commandBuffer.Blit(idBufferID, dataTexture, blitFlipMat);
            commandBuffer.ReleaseTemporaryRT(idBufferID);

            commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
            commandBuffer.ClearRenderTarget(false, true, Color.white);
        }
    }
}
