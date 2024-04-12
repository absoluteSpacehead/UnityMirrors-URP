using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class MainCamMirrorHelper : MonoBehaviour
{
    // Add component to Main Camera to render Mirrors
    Mirror[] mirrors;
    Camera mCamera;

    void Awake()
    {
        mirrors = FindObjectsOfType<Mirror>();

        // Replacement for OnPreCull() which is BIRP only
        mCamera = GetComponent<Camera>();
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        if (cam != mCamera)
            return;

        for (int i = 0; i < mirrors.Length; i++)
        {
            mirrors[i].Render(ctx);
        }
    }
}
