using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraPathPreviewSupport 
{
    public static bool previewSupported
    {
        get
        {
#if UNITY_EDITOR
            if (!SystemInfo.supportsRenderTextures) return false;
//            if (SystemInfo.graphicsShaderLevel >= 50 && PlayerSettings.useDirect3D11) return false;
//            if (!Application.HasProLicense()) return false;
#endif
            return true;
        }
    }
    public static string previewSupportedText
    {
        get
        {
#if UNITY_EDITOR
            if (!SystemInfo.supportsRenderTextures) return "Render Textures is not support now";
//            if (SystemInfo.graphicsShaderLevel >= 50 && PlayerSettings.useDirect3D11) return "DirectX 11 is not supported at this shader level";
//            if (!Application.HasProLicense()) return "Preview is a Unity Pro feature";
#endif
            return "";
        }
    }
}
