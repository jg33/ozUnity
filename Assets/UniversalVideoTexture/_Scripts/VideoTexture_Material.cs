using UnityEngine;
using System.Collections;

// Universal Video Texture v1.75

public class VideoTexture_Material : MonoBehaviour 
{
	// UVT playback parameters
	
	public float FPS = 30;
	public int firstFrame;
	public int lastFrame;
	public string FileName = "VidTex";
	public string digitsFormat = "0000";
	public DigitsLocation digitsLocation = DigitsLocation.Postfix;
	public UVT_PlayMode  playMode;
	public bool pingpongStartsWithReverse = false;
	public int numberOfLoops = 1;
	public TextureType textureType;
	public LowMemoryMode lowMemoryMode = LowMemoryMode.Normal;
	
	
	// UVT custom control objects (Scrollbar, Current Time Indicator (CTI) and Timecode (3D Text)
	
	public GameObject scrollBar;
	public GameObject CTI;
	public TextMesh timeCode;
	public LayerMask controlLayer = -1; // The Layer for assigned for this UVT
	
	// UVT state variables
	
	private UVT_PlayState playState;
	private UVT_PlayDirection playDirection;
	
	// UVT additional parameters (On/Off switches)

	public bool sharedMaterial = false;
	public bool enableAudio = false;
	public bool forceAudioSync = false;
	public bool enableControls = true;
	public bool autoPlay = true;
	public bool autoDestruct = false;
	public bool autoLoadLevelWhenDone = false;
	
	public string LevelToLoad; // Name of the scene to load when playback is done
	
	// UVT private variables
	
	float currentPosition = 0; 	// Current playback position (ranges from 0 to 1)
	float ctiPosition = 0;	    // Current CTI position (ranges from 0 to 1)
	private int currentLoop = 0; // Loop counter
	float scrollBarLength = 0; 
	bool scrubbing = false;
	bool audioAttached = false;
	AttachedAudio myAudio = new AttachedAudio(); // Creates an audio class for audio management //
	string texType = "";
	string indexStr = "";
	Texture newTex; 
	Texture lastTex;
	float playFactor = 1; // Advances playback (Forwards / Backwards)
	float index = 0;      // Frame index to be used with playFactor for calculating current frame. 
	int intIndex = 0;     // // Frame index to be used for loading the actual texture.
	int lastIndex = -1;
	
	void Awake()
	{
		audioAttached = GetComponent("AudioSource");
	}
	
	void Start ()
	{
		// Enables / Disables controls
		
		if (enableControls)
		{
			scrollBar.gameObject.active = true;
			CTI.gameObject.active = true;
			timeCode.gameObject.active = true;
			
			HandleControls();
			
			scrollBarLength = scrollBar.GetComponent<MeshFilter>().mesh.bounds.extents.x;
			timeCode.GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("GUI/3D Text Depth Aware Shader");
		}	
		else
		{
			if (scrollBar != null)
				scrollBar.gameObject.active = false;
			if (CTI != null)
				CTI.gameObject.active = false;
			if (timeCode != null)
				timeCode.gameObject.active = false;
		}
		
		if (playMode == UVT_PlayMode .PingPong && pingpongStartsWithReverse)
		{
			currentLoop--;
			index = lastFrame;
			playDirection = UVT_PlayDirection.backward;
		}
		else
			index = firstFrame;
		
		// Sets up audio for playback
		
		if (audioAttached)
		{
			myAudio.attachedAudioSource = GetComponent<AudioSource>();
			myAudio.fps = FPS;
			myAudio.frameIndex = firstFrame;
		}
		
		// Assigns the layer to the chosen channel
		
		switch (textureType)
		{
		case TextureType.Diffuse:
			texType = "_MainTex";
			break;
		case TextureType.NormalMap:
			texType = "_BumpMap";
			break;
		case TextureType.DetailMap:
			texType = "_Detail";
			break;
		case TextureType.Illumination:
			texType = "_Illum";
			break;
		case TextureType.HeightMap:
			texType = "_ParallaxMap";
			break;
		}
		
		playState = UVT_PlayState.Paused;
		
		if (autoPlay)
			Play();
	}
 	
	void Update () 
	{
		// Checks if played all loops
		
		if (currentLoop == numberOfLoops)
		{
			if (autoLoadLevelWhenDone)
				Application.LoadLevel(LevelToLoad);
			else
				if (autoDestruct)
				{
					Destroy(scrollBar.transform.parent.gameObject);
					Destroy(gameObject);
				}
				else
					Stop();
		}
		
		intIndex = (int)index;
		
		currentPosition = (float)(intIndex - firstFrame) / (lastFrame - firstFrame); // Calculates current position of playback
		
		if (enableControls)
			HandleControls();
		
		// Defaults to forward playback on first frame
		
		if (index <= firstFrame)
		{
			index = firstFrame;
				
			if (playState == UVT_PlayState.Playing)  
			{
				playDirection = UVT_PlayDirection.forward;
				UpdatePlayFactor();
				UpdateAudio();
			}
		}
		
		// Handles play modes on last frame
		
		if (index >= lastFrame) 
		{
			if (playState == UVT_PlayState.Playing)
			{	
				if (playMode != UVT_PlayMode .Random)
					currentLoop++;
				
				if (playMode == UVT_PlayMode .Loop)
				{
					index = firstFrame;
					Play();
				}
				else
					if (playMode == UVT_PlayMode .Once)
					{
						index = lastFrame;
					}
					else
						if (playMode == UVT_PlayMode .PingPong)
							{
 								playDirection = UVT_PlayDirection.backward;
								UpdatePlayFactor();
							}
			}
		}
		if ((playMode == UVT_PlayMode .Random) && (intIndex != lastIndex))
			{
				intIndex = Random.Range(firstFrame,lastFrame);
				index = intIndex;
			}
		
		// Memory management (Low memory modes)
		
		if (intIndex != lastIndex)	
		{
			if (lowMemoryMode == LowMemoryMode.Normal)
			{
				Resources.UnloadAsset(lastTex);
				lastTex = newTex;
			}
			else
			if (lowMemoryMode == LowMemoryMode.BruteForce)
				Resources.UnloadUnusedAssets();
				
			indexStr = string.Format("{0:" + digitsFormat + "}", intIndex); 
			
			if (digitsLocation == DigitsLocation.Postfix)
				newTex = Resources.Load(FileName + indexStr) as Texture;
			else
				newTex = Resources.Load(indexStr + FileName) as Texture;
			
			if (sharedMaterial)					
				GetComponent<Renderer>().sharedMaterial.SetTexture(texType, newTex);
			else
				GetComponent<Renderer>().material.SetTexture(texType, newTex);
				
			lastIndex = intIndex;
		}
		
		if (playState == UVT_PlayState.Playing)
			index += playFactor * (FPS * Time.deltaTime);
		
		// When enabled (via public forceAudioSync) forces Video/Audio sync with each update (Disabled by default)
		
		if (enableAudio && forceAudioSync && playState == UVT_PlayState.Playing)
			ForceAudioSync();
				
	}
	
	// Handles the display and mechanics of the assigned controls
	
	void HandleControls()
	{
			timeCode.text = ((string.Format("{0:" + "00" + "}", ((int)(index / FPS / 60)))) + ":" // Minutes
				+ (string.Format("{0:" + "00" + "}", ((int)(index / FPS)) % 60) + ":" 	// Seconds
			    + (string.Format("{0:" + "00" + "}", intIndex % FPS ))) /*+ " FPS " + 1 / Time.deltaTime */);
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
	    	if (Physics.Raycast(ray, out hit, 100, controlLayer.value))
			{
				if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.name == "ScrollBar")
				{
					CTI.transform.position = new Vector3(hit.point.x ,CTI.transform.position.y, CTI.transform.position.z);
					scrubbing = true;
					UpdateAudio();
				}
				
				if (Input.GetMouseButtonDown(0) && hit.transform.gameObject == this.gameObject)
				{
					TogglePlay();
				}
			}
			else
				if (Input.GetMouseButton(0) && (scrubbing))
				{
					scrubbing = false;
					UpdateAudio();
				}
				
				if (scrubbing)
				{
					index = (((firstFrame + scrollBar.transform.InverseTransformPoint(hit.point).x + scrollBarLength) * scrollBar.transform.localScale.x) 
						                           / (scrollBarLength * scrollBar.transform.localScale.x * 2) * (lastFrame - firstFrame));
				}
				
				if (Input.GetMouseButtonUp(0) && scrubbing)
				{
					scrubbing = false;
					Sync();
					UpdatePlayFactor();
					UpdateAudio();
				}
					
					ctiPosition = currentPosition;
					CTI.transform.localPosition = (new Vector3(-scrollBarLength * scrollBar.transform.localScale.x + ctiPosition * scrollBarLength * scrollBar.transform.localScale.x * 2 , 0, 0));
	}
	
	public void EnableControls(bool EnableControls)
	{
		if (EnableControls)
		{
			scrollBar.gameObject.active = true;
			CTI.gameObject.active = true;
			timeCode.gameObject.active = true;

			enableControls = true;
		}	
		else
		{
			if (scrollBar != null)
				scrollBar.gameObject.active = false;
			if (CTI != null)
				CTI.gameObject.active = false;
			if (timeCode != null)
				timeCode.gameObject.active = false;
			
			enableControls = false;
		}
	}
	
	// Checks if out of sync and updates audio accordingly
	
	void ForceAudioSync()
	{
		if (!myAudio.attachedAudioSource.isPlaying || 
			(myAudio.attachedAudioSource.time / (index / FPS) < 0.8) || 
			(myAudio.attachedAudioSource.time / (index / FPS) > 1.2))
		{	
			// If true then audio is out of sync
			UpdateAudio();
		}
	}
	
	// Updates playFactor according to playState
	
	void UpdatePlayFactor()
	{
		if (playState == UVT_PlayState.Playing)
		{	
			switch (playDirection)
			{
			case UVT_PlayDirection.forward:
				playFactor = 1;
				break;
			case UVT_PlayDirection.backward:
				playFactor = -1;
				break;
			}
		}	
		else
			playFactor = 0;
	}
	
	// Updates audio according to playState & playDirection
	
	void UpdateAudio()
	{
		if (enableAudio && audioAttached)
		{
			if (scrubbing)
			{
				myAudio.Stop();
			}
			else
			{
				if(playState == UVT_PlayState.Playing)
				{	
					switch (playDirection)
					{
					case UVT_PlayDirection.forward:
						myAudio.frameIndex = index;
						myAudio.Sync();
						myAudio.Play();
						break;
						
					case UVT_PlayDirection.backward:
						myAudio.Stop();
						break;
					}
				}	
				else
					myAudio.Stop();
			}
		}
	}
	
	// Syncs audio to video
	
	public void Sync()
	{
		if (enableAudio)
		{
			myAudio.frameIndex = index;
			myAudio.Sync();
		}
	}
	
	// Plays the video texture
	
	public void Play()
	{
		if (enableAudio)
			{
				myAudio.frameIndex = index;
				myAudio.Sync();
				myAudio.UnMute();
				myAudio.Play();
			}
			
			playState = UVT_PlayState.Playing;
			UpdatePlayFactor();
	}	
	
	// Pauses the video texture
	
	public void Stop()
	{
		if (playState != UVT_PlayState.Paused)
		{
			if (enableAudio)
				myAudio.Stop();
		
			playState = UVT_PlayState.Paused;
			UpdatePlayFactor();
		}
	}
	
	// Toggles playback on/off
	
	public void TogglePlay()
	{
		
		if (playState == UVT_PlayState.Playing)
			{
				Stop();
			}
			else
			{
				Play();
			}
	}
	
	// Changes direction of playback
	
	public void ChangeDirection(UVT_PlayDirection newUVT_PlayDirection)
	{
		playDirection = newUVT_PlayDirection;
		UpdatePlayFactor();
		UpdateAudio();
	}
	
	// Returns current playback state (Playing / Paused)
	
	public UVT_PlayState CurrentUVT_PlayState()
	{
		return playState;
	}	
	
	// Returns current playback direction (Forwards / Backwards)
	
	public UVT_PlayDirection CurrentUVT_PlayDirection()
	{
		return playDirection;
	}
	
	// Returns current playback position (Ranges from 0 to 1)
	
	public float CurrentPosition()
	{
		return currentPosition;
	}



	public void gotoPosition(float pos){
		index = Mathf.Floor (lastFrame * pos) ;

	}

	
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      