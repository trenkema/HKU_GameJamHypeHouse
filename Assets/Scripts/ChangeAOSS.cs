using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;

public class ChangeAOSS : MonoBehaviour
{
    //public RenderPipelineAsset assetToUse;
    public UniversalRenderPipelineAsset assetToUse;
    public float gravityValue = -9.81f;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, gravityValue, 0);
        var rpAsset = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset;
        var urpAsset = (UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)rpAsset;
        GraphicsSettings.renderPipelineAsset = assetToUse;
        
    }
}
