using System;
using UnityEditor;
using UnityEngine;
//using Object = UnityEngine.Object;
//using System.Collections.Generic;

public class CameraPathDualControl : EditorWindow 
{
    [MenuItem("Window/Camera Path Multi Path Viewer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CameraPathDualControl));
    }

    private bool _animateInEditor = true;
    private CameraPathAnimator[] animators;
    private float[] pathTimes;
//    private Dictionary<int, float[]> percentages;
    private float editorPercentage = 0;

    //Preview Camera
    private static float aspect = 1.7777f;
    private static int previewResolution = 800;

    private void OnEnable()
    {
        UpdateCalculations();

    }

//    private void OnValidate()
//    {
//        UpdateCalculations();
//    }

    private void OnGUI()
    {
        int numberOfPaths = animators.Length;
        for(int p = 0; p < numberOfPaths; p++)
        {
            CameraPathAnimator animator = animators[p];

            if(animator==null)
                return;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(animator.name, GUILayout.Width(160));
            EditorGUILayout.LabelField("Path Time", GUILayout.Width(60));
            EditorGUILayout.LabelField(pathTimes[p].ToString("F1")+" secs", GUILayout.Width(70));

            if(GUILayout.Button("Goto Animation Target", GUILayout.Width(150)))
                GotoScenePoint(animator.animationObject.position);

            animator.showPreview = EditorGUILayout.Toggle("Show Preview", animator.showPreview);

            animator.editorPercentage = editorPercentage;

            if(animateInEditor)
            {
                SetTransform(animator.animationObject, animator);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        animateInEditor = EditorGUILayout.Toggle("Animate Targets in Editor", animateInEditor);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        float inputPercent = editorPercentage * 100;
        float outputPercent = EditorGUILayout.Slider(inputPercent, 0, 100) * 0.01f;
        editorPercentage = outputPercent;
        EditorGUILayout.LabelField("%", GUILayout.Width(20));
        EditorGUILayout.EndHorizontal();

        RenderPreviews();

        if(GUI.changed)
            UpdateCalculations();
    }

    private bool animateInEditor
    {
        get {return _animateInEditor;}
        set
        {
            if(value != _animateInEditor)
            {
                int numberOfPaths = animators.Length;
                for (int p = 0; p < numberOfPaths; p++)
                    animators[p].animateSceneObjectInEditor = animateInEditor;
                _animateInEditor = value;
            }
        }
    }

    private void UpdateCalculations()
    {
        animators = FindObjectsOfType<CameraPathAnimator>();
        int numberOfPaths = animators.Length;
        pathTimes = new float[numberOfPaths];
//        percentages = new Dictionary<int, float[]>();
        for(int p = 0; p < numberOfPaths; p++)
        {
            CameraPathAnimator animator  = animators[p];
            CameraPath path = animator.cameraPath;
            float pathStoredResoltion = path.storedPointResolution;
            int storedPointsSize = path.storedValueArraySize;
            float pathTime = 0;
            for(int i = 0; i < storedPointsSize; i++)
            {
                float pathSpeed = animator.pathSpeed;
                if(path.speedList.listEnabled)
                {
                    float perc = pathStoredResoltion * i;
                    pathSpeed = path.GetPathSpeed(perc);
                }
                pathTime += pathStoredResoltion / pathSpeed;
            }
            pathTimes[p] = pathTime;


//            percentages.Add();
        }
    }

    private void RenderPreviews()
    {
        int numberOfPaths = animators.Length;
        GameObject previewGO = new GameObject();
        previewGO.hideFlags = HideFlags.HideAndDontSave;
        Camera previewCam = previewGO.AddComponent<Camera>();
        previewCam.orthographic = Camera.main.orthographic;
        GUILayout.BeginHorizontal();
        for(int p = 0; p < numberOfPaths; p++)
        {
            CameraPathAnimator animator = animators[p];
            CameraPath path = animator.cameraPath;

            if (path.realNumberOfPoints < 2 || !animator.showPreview)
                continue;

            if(CameraPathPreviewSupport.previewSupported && !EditorApplication.isPlaying)
            {
                RenderTexture rt = RenderTexture.GetTemporary(previewResolution, Mathf.RoundToInt(previewResolution / aspect), 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1);

                if (previewCam.orthographic)
                    previewCam.orthographicSize = path.GetPathOrthographicSize(animator.editorPercentage);
                else
                    previewCam.fieldOfView = path.GetPathFOV(animator.editorPercentage);

                SetTransform(previewCam.transform, animator);
                previewCam.enabled = true;
                previewCam.targetTexture = rt;
                previewCam.Render();
                previewCam.targetTexture = null;
                previewCam.enabled = false;

                GUILayout.BeginVertical();
                GUILayout.Label(animator.name);
                GUILayout.Label(rt, GUILayout.Width(200), GUILayout.Height(150));
                GUILayout.EndVertical();
                RenderTexture.ReleaseTemporary(rt);
            }
            else
            {
                string errorMsg = CameraPathPreviewSupport.previewSupportedText;
                EditorGUILayout.LabelField(errorMsg);
            }
        }
        GUILayout.EndHorizontal();
        DestroyImmediate(previewGO);
    }
    
    /// <summary>
    /// A little hacking of the Unity Editor to allow us to focus on an arbitrary point in 3D Space
    /// We're replicating pressing the F button in scene view to focus on the selected object
    /// Here we can focus on a 3D point
    /// </summary>
    /// <param name="position">The 3D point we want to focus on</param>
    private void GotoScenePoint(Vector3 position)
    {
        UnityEngine.Object[] intialFocus = Selection.objects;
        GameObject tempFocusView = new GameObject("Temp Focus View");
        tempFocusView.transform.position = position;
        try
        {
            Selection.objects = new UnityEngine.Object[] { tempFocusView };
            SceneView.lastActiveSceneView.FrameSelected();
            Selection.objects = intialFocus;
        }
        catch (NullReferenceException)
        {
            //do nothing
        }
        DestroyImmediate(tempFocusView);
    }

    private void SetTransform(Transform transform, CameraPathAnimator animator)
    {
        CameraPath path = animator.cameraPath;
        transform.position = path.GetPathPosition(editorPercentage);
        transform.rotation = animator.GetAnimatedOrientation(editorPercentage, false);
    }
}
