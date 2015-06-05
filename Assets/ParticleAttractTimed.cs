using UnityEngine;
using System.Collections;

public class ParticleAttractTimed : MonoBehaviour 
{
	public float PullDistance = 200;
	public Transform MagnetPoint;
	public int TimeUntilPull = 0;
	
	private bool waitCalled = false;
	
	IEnumerator ParticleWait ()
	{
		waitCalled = true;
		yield return new WaitForSeconds(TimeUntilPull);
		TimeUntilPull = 0;
		Debug.Log("TimeUntilPull set to 0");
	}
	
	void ParticlePull ()
	{
		float sqrPullDistance = PullDistance * PullDistance;
		
		ParticleSystem.Particle[] x = new ParticleSystem.Particle[gameObject.GetComponent<ParticleSystem>().particleCount+1]; 
		int y = GetComponent<ParticleSystem>().GetParticles(x);
		
		for (int i = 0; i < y; i++)
		{
			Vector3 offset = MagnetPoint.localPosition - x[i].position;
			//creates Vector3 variable based on the position of the target MagnetPoint (set by user) and the current particle position
			float sqrLen = offset.sqrMagnitude;
			//creats float type integer based on the square magnitude of the offset variable set above (faster than .Magnitude)
			
			if (sqrLen <= sqrPullDistance)
			{
				x[i].position = Vector3.Lerp(x[i].position, MagnetPoint.localPosition, Mathf.SmoothStep(0, 2, (Time.deltaTime / 0.1F)));
				/*Lerping moves an object between two vectors (syntax is FromVector, ToVector, Fraction) by a given fraction. In our example 
                 we take the position of particle i, of particle system x, and the local position of the MagnetPoint transform, and move the 
                 particles in from x[i] towards MagnetPoint over time. Lower the Time.deltaTime / # value to increase how fast the particle attracts*/
				if ((x[i].position-MagnetPoint.localPosition).magnitude <= 30)
				{
					x[i].lifetime = 0;
				}
			}
		}
		
		gameObject.GetComponent<ParticleSystem>().SetParticles(x, y);
		return;
	}
	
	//Use this for initialization
	void Start () 
	{
		Debug.Log("Time until pull equals " + TimeUntilPull);
	}
	
	/*Update is called once per frame and is good for simple timers, basing changes off frame rate and recieving input. 
     Note that if one frame takes longer to process than the next, Update will not be called consistently*/
	void Update ()  
	{
		if ((TimeUntilPull > 0) & (waitCalled == false))
		{
			Debug.Log("Coroutine called");
			StartCoroutine(ParticleWait());
		}
		else if (TimeUntilPull == 0)
			ParticlePull();
	}
}
