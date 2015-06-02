using UnityEngine;
using System.Collections;

public class CameraDeAlpha : MonoBehaviour {
    // After camera is rendered, this clears alpha channel of it's
    // render texture to pure white.
    private Material mat;
 
    void OnPostRender() {
        if (!mat) {
            mat = new Material ("Shader \"Hidden/Alpha\" {" +
                "SubShader {" +
                "    Pass {" +
                "        ZTest Always Cull Off ZWrite Off" +
                "        ColorMask A" +
                "        Color (1,1,1,1)" +
                "    }" +
                "}" +
                "}"
            );
            mat.shader.hideFlags = HideFlags.HideAndDontSave;
            mat.hideFlags = HideFlags.HideAndDontSave;
        }
        GL.PushMatrix ();
        GL.LoadOrtho ();
        for (var i = 0; i < mat.passCount; ++i) {
            mat.SetPass (i);
            GL.Begin (GL.QUADS);
            GL.Vertex3 (0, 0, 0.1f);
            GL.Vertex3 (1, 0, 0.1f);
            GL.Vertex3 (1, 1, 0.1f);
            GL.Vertex3 (0, 1, 0.1f);
            GL.End ();
        }
        GL.PopMatrix ();
    }
}
