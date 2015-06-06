// Class for audio management

using UnityEngine;
using System.Collections;



public class AttachedAudio
{
	public AudioSource attachedAudioSource;
	
	public float frameIndex = 0;
	public float fps = 0;
	
	public bool togglePlay = true;
	
	public void Play()
	{
		if (attachedAudioSource)
			if (!attachedAudioSource.isPlaying)
				attachedAudioSource.Play();
	}
	
	public void Mute()
	{
		attachedAudioSource.volume = 0f;
	}
	
	public void UnMute()
	{
		attachedAudioSource.volume = 1;
	}	
	
	public void Stop()
	{
		if (attachedAudioSource)
			attachedAudioSource.Stop();
	}
	
	public void Toggle()
	{
		if (togglePlay)
			{
				togglePlay = false;
				
				if (attachedAudioSource)
				attachedAudioSource.Pause();
			}
			else
			{
				togglePlay = true;
				
				if (attachedAudioSource)
				{
					attachedAudioSource.time = frameIndex / fps;
					attachedAudioSource.Play();
				}
			}
	}	
	
	public void Sync()
	{
		if (attachedAudioSource)
			attachedAudioSource.time = frameIndex / fps;
	}
}