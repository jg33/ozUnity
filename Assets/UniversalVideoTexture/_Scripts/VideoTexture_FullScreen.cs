using UnityEngine;
using System.Collections;

// Universal Video Texture v1.75

public class VideoTexture_FullScreen : MonoBehaviour 
{
	// UVT playback parameter
	
	public float FPS = 30;
	public int firstFrame;
	public int lastFrame;
	public string FileName = "VidTex";
	public string digitsFormat = "0000";
	public DigitsLocation digitsLocation = DigitsLocation.Postfix;
	public float aspectRatio = 1.78f;
	public UVT_PlayMode playMode;
	public bool pingpongStartsWithReverse = false;
	public int numberOfLoops = 1;
	public LowMemoryMode lowMemoryMode = LowMemoryMode.Normal;
	
	// UVT GUI control variables
	
	public Texture ctiTexture;
	public Texture backgroundTexture;
	public Texture scrollBarTexture;
	public float scrollBarLength = 250f;
	public float scrollBarHeight = 50f;
	public float scrollBarOffset = 50f;
	public int timecodeSize = 16;
	
	// UVT additional variables (On/Off switches)
	
	public bool hideBackground = true;
	public bool showScrollBar = true;
	public bool showTimecode = true;
	public bool enableTogglePlay = true;
	public bool enableAudio = false;
	public bool forceAudioSync = false;
	public bool autoPlay = true;
	public bool autoHideWhenDone = false;
	public bool autoLoadLevelWhenDone = false;
	
	public string LevelToLoad; // Name of the scene to load when playback is done 
	
	// UVT state variables
	
	private UVT_PlayState playState;
	private UVT_PlayDirection playDirection;
	
	// UVT private variables
	
	bool enableControls = true;
	bool scrubbing = false;
	bool audioAttached = false;
	private int loopNumber = 0;
	string indexStr = "";
	Texture newTex;
	Texture lastTex;
	float playFactor = 0; // Advances playback (Forwards / Backwards)
	float currentPosition = 0;
	float index = 0; // Frame index to be used with playFactor for calculating current frame. 
	int intIndex = 0; // Frame index to be used for loading the actual texture.
	int lastIndex = -1;
	AttachedAudio myAudio = new AttachedAudio(); // Creates an audio class for audio management 
	Rect CTI; 
	GUIStyle style = new GUIStyle();
	bool enableGUI = true;
	
	void Awake()
	{
		//Application.targetFrameRate = 60; (Optional for smoother scrubbing on capable systems)
		
		audioAttached = GetComponent("AudioSource");
	}
	
	void Start ()
	{	
		CTI = new Rect(0,0,0,0);
		
		style.fontSize = timecodeSize;
	 	style.normal.textColor = Color.white;
		
		if (playMode == UVT_PlayMode.PingPong && pingpongStartsWithReverse)
		{
			//loopNumber--;
			index = lastFrame;
			playDirection = UVT_PlayDirection.backward;
		}
		else
			index = firstFrame;
		
		if (audioAttached)
		{
			myAudio.attachedAudioSource = GetComponent<AudioSource>();
			myAudio.fps = FPS;
			myAudio.frameIndex = firstFrame;
		}
		
			playState = UVT_PlayState.Paused;
			
		if (autoPlay)
			Play();
	}
 	
	void Update () 
	{
		// Checks if played all loops
		
		if (loopNumber == numberOfLoops)
		{
			if (autoLoadLevelWhenDone)
				Application.LoadLevel(LevelToLoad);
			else
			{
				Stop();
				if (autoHideWhenDone)
					enableGUI = false;
			}
		}
		
		HandleControls();
		
		intIndex = (int)index;
		
		currentPosition = (float)(intIndex - firstFrame) / (lastFrame - firstFrame);
		
		/// Defaults to normal play on first frame
		
		if (intIndex <= firstFrame)
		{
			if (playState == UVT_PlayState.Playing)  
			{
				playDirection = UVT_PlayDirection.forward;
				UpdatePlayFactor();
			}
		}
		
		// Handles play modes on last frame
		
		if (index >= lastFrame)
		{
			if (playState == UVT_PlayState.Playing)
			{	
				loopNumber++;
				
				if (playMode == UVT_PlayMode.Loop)
				{
					index = firstFrame;
					Play();
				}
				else
					if (playMode == UVT_PlayMode.Once)
						index = lastFrame;
					else
						if (playMode == UVT_PlayMode.PingPong)
						{
							playDirection = UVT_PlayDirection.backward;
							UpdatePlayFactor();
						}
			}
		}
		
		if ((playMode == UVT_PlayMode.Random) && (intIndex != lastIndex))
			{
				intIndex = Random.Range(firstFrame,lastFrame);
				index = intIndex;
			}
		
		//Memory management
				
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
			
			lastIndex = intIndex;
		}
	}
	
	// Utilizes Unity's built in GUI for displaying the video texture and its customized controls
	
	void OnGUI()
		
	{
		if (enableGUI)
		{
			if(hideBackground)
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),backgroundTexture,ScaleMode.StretchToFill,true,aspectRatio); // Background Texture draw	to avoid ghosting
			
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),newTex,ScaleMode.ScaleToFit,true,aspectRatio); // Actual video texture draw
			
			if (enableControls)
			{
				if (showTimecode)
				{
					GUI.Label(new Rect(Screen.width / 2 - timecodeSize * 2, Screen.height - scrollBarOffset + scrollBarHeight, scrollBarLength, scrollBarHeight),(string.Format("{0:" + "00" + "}", ((int)(index / FPS / 60)))) + ":" // Minutes
																																					     + (string.Format("{0:" + "00" + "}", ((int)(index / FPS)) % 60) + ":" 	// Seconds
																																						 + (string.Format("{0:" + "00" + "}", intIndex % FPS ))),style); // Frames
				}	
				
				if (showScrollBar)
				{	
					GUI.DrawTexture(new Rect(Screen.width / 2 - (scrollBarLength / 2), Screen.height - scrollBarOffset, scrollBarLength, scrollBarHeight),scrollBarTexture,ScaleMode.StretchToFill,true);
					GUI.DrawTexture(CTI,ctiTexture,ScaleMode.StretchToFill,true);
				}
			}
		}
	}
	
	// Handles the mechanics of the assigned controls
	
	void HandleControls()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (showScrollBar && CTI.Contains(new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y,0)) && (Input.GetMouseButton(0)))
			{
				myAudio.Stop();
				scrubbing = true;
			}
			
			else
			if (enableTogglePlay)	
				TogglePlay();
		}
			
		if ((scrubbing) && (Input.mousePosition.x > Screen.width / 2 - (scrollBarLength / 2) 
					    && ((Input.mousePosition.x < Screen.width / 2 - (scrollBarLength / 2) + scrollBarLength))))
		{
			CTI = new Rect(Input.mousePosition.x - scrollBarHeight/2, Screen.height - scrollBarOffset, scrollBarHeight, scrollBarHeight);
			index = ((Input.mousePosition.x - (Screen.width / 2 - (scrollBarLength / 2))) / scrollBarLength) * (firstFrame + lastFrame);
			lastIndex = intIndex;
		}
		else
		{
			CTI = new Rect(Screen.width / 2 - (scrollBarLength / 2) + (scrollBarLength) * index / lastFrame - scrollBarHeight/2, Screen.height-scrollBarOffset,scrollBarHeight,scrollBarHeight);
			index += playFactor * (FPS * Time.deltaTime);
				
			if (intIndex <= firstFrame && audioAttached && enableAudio && playFactor > 0)
			{
				UpdateAudio();
			}
		}
		
		if ((scrubbing) && (Input.GetMouseButtonUp(0)))
		{	
				scrubbing = false;
				if (enableAudio && myAudio.togglePlay && playFactor > 0)
			{
				UpdatePlayFactor();
				UpdateAudio();
			}
		}
		
		// When enabled (via public forceAudioSync) forces Video/Audio sync with each update (Disabled by default)
		
		if (enableAudio && forceAudioSync && playState == UVT_PlayState.Playing)
			ForceAudioSync();
	}
	
	// Checks if out of sync and updates audio accordingly
	
	void ForceAudioSync()
	{
		if (!myAudio.attachedAudioSource.isPlaying || 
			(myAudio.attachedAudioSource.time / (index / FPS) < 0.9))
			
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
	
	public void EnableControls(bool EnableControls)
	{
		if (EnableControls)
		{
			enableControls = true;
			enableTogglePlay = true;
		}	
		else
		{
			enableControls = false;
			enableTogglePlay = false;
		}
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
		if (enableAudio && playDirection == UVT_PlayDirection.forward)
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
		

}
