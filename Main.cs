using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Main : MonoBehaviour
{
    public int framesPerSecond;
    private float interval => 1.0f / this.framesPerSecond;

    private float timer;

    /// <summary>
    /// Render target (destination) for the TV camera.
    /// </summary>
    protected RenderTexture lidarTexture;

    /// <summary>
    /// Material holding the shader which will apply to the camera output.
    /// The output of the shader is copied to the texture of `screenMaterial`.
    /// </summary>
    protected Material lidarMaterial;

    /// <summary>
    /// Camera capturing the image.
    /// </summary>
    protected Camera lidarCamera;

    /// <summary>
    /// Material holding the texture which will be pasted unto the screen.
    /// </summary>
    protected Material screenMaterial;

    /// <summary>
    /// Final output texture that will be pasted unto the screen.
    /// </summary>
    protected RenderTexture screenTexture;

    void Start()
    {
        this.timer = 0.0f;

        // var oldTvTexture = GameObject.Find("TV Camera (old)").GetComponent<Camera>().targetTexture;

        this.lidarTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.Depth, 0)
        {
            name = "Lidar Texture (dynamic)",
            depthStencilFormat = GraphicsFormat.D32_SFloat
        };

        // Create Lidar camera and set correct settings:
        var lidarCameraObj = new GameObject("Lidar Camera");
        lidarCameraObj.transform.SetPositionAndRotation(new Vector3(5.0f, 1.0f, 0.0f), Quaternion.Euler(0.0f, -90.0f, 0.0f));
        this.lidarCamera = lidarCameraObj.AddComponent<Camera>();
        this.lidarCamera.depthTextureMode = DepthTextureMode.Depth;
        this.lidarCamera.clearFlags = CameraClearFlags.SolidColor;
        this.lidarCamera.nearClipPlane = 1.0f;
        this.lidarCamera.farClipPlane = 10.0f;
        this.lidarCamera.renderingPath = RenderingPath.Forward;
        this.lidarCamera.useOcclusionCulling = false;
        this.lidarCamera.allowHDR = false;
        this.lidarCamera.allowMSAA = false;

        this.lidarCamera.targetTexture = this.lidarTexture;

        this.lidarMaterial = Resources.Load<Material>("LidarMaterial");

        // Create display texture and apply it to an existing object in the scene (for debugging)
        this.screenTexture = new RenderTexture(this.lidarTexture.width, this.lidarTexture.height, 0, RenderTextureFormat.ARGBFloat, 0)
        {
            name = "Screen Texture (dynamic)"
        };
        this.screenMaterial = GameObject.Find("TV Screen").GetComponent<Renderer>().material;
        this.screenMaterial.mainTexture = this.screenTexture;
    }

    void Update()
    {
        if ((this.timer += Time.deltaTime) >= interval)
        {
            this.timer = 0.0f;
            this.UpdateScreen();
        }
        //this.UpdateScreen();
    }

    void UpdateScreen()
    {
        // Camera is disabled, manually trigger render instead:
        this.lidarCamera.Render();

        // Parse tvTexture with the shader from tvMaterial and output to screenTexture
        Graphics.Blit(this.lidarTexture, this.screenTexture, this.lidarMaterial);
    }
}
