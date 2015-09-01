/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
	/// <summary>
	/// A custom handler that implements the ITrackableEventHandler interface.
	/// </summary>
	public class CustomAREventHandler : MonoBehaviour,
	ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES
		
		private TrackableBehaviour mTrackableBehaviour;

		private GameObject camCtl;
		private GameObject storm;

		#endregion // PRIVATE_MEMBER_VARIABLES
		
		
		
		#region UNTIY_MONOBEHAVIOUR_METHODS
		
		void Start()
		{
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}

			 if (mTrackableBehaviour.TrackableName.StartsWith( "Passive" ) ){

				gameObject.transform.GetChild(0).gameObject.SetActive(false);
				gameObject.transform.GetChild(1).gameObject.SetActive(false);
				Debug.Log ("Turn that shit off " + gameObject.GetComponent<ImageTargetBehaviour>().ImageTarget.Name+ " "  );
					

				
			}



		}
		
		#endregion // UNTIY_MONOBEHAVIOUR_METHODS
		
		
		
		#region PUBLIC_METHODS
		
		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
			    newStatus == TrackableBehaviour.Status.TRACKED ||
			    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
			}
			else
			{
				OnTrackingLost();
			}
		}
		
		#endregion // PUBLIC_METHODS
		
		
		
		#region PRIVATE_METHODS
		
		
		private void OnTrackingFound()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}
			
			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}

			if (mTrackableBehaviour.TrackableName == "Oz_TopTarget_inverted"){
				camCtl = GameObject.Find ("Camera Container");
				camCtl.SendMessage("updateTarget");
				camCtl.SendMessage("setFoundTarget",true);
			} else if (mTrackableBehaviour.TrackableName.StartsWith("Passive")){
				camCtl = GameObject.Find ("Camera Container");
				camCtl.SendMessage("setTightTracking", true);
				storm = GameObject.Find("storm");
				storm.SetActive(false);

				if(gameObject.GetComponent<ImageTargetBehaviour>().ImageTarget.Name == mTrackableBehaviour.TrackableName){
					gameObject.transform.GetChild(0).gameObject.SetActive(true);
	//				gameObject.transform.GetChild(1).gameObject.SetActive(true);

				}

			}



			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
		}
		
		
		private void OnTrackingLost() {

			if (mTrackableBehaviour.TrackableName == "Oz_TopTarget_inverted"){
				GameObject camCtl = GameObject.Find ("Camera Container");
				camCtl.SendMessage("lostTarget");
			} else if (mTrackableBehaviour.TrackableName.StartsWith( "Passive" ) ){
				GameObject camCtl = GameObject.Find ("Camera Container");
				camCtl.SendMessage("setTightTracking", false);
				if(!storm) storm = GameObject.Find("storm");
				storm.SetActive(true);

				if(gameObject.GetComponent<ImageTargetBehaviour>().ImageTarget.Name == mTrackableBehaviour.TrackableName){
					gameObject.transform.GetChild(0).gameObject.SetActive(false);
//					gameObject.transform.GetChild(1).gameObject.SetActive(false);
					Debug.Log ("BORT " + gameObject.GetComponent<ImageTargetBehaviour>().ImageTarget.Name+ " "  );

				}

				GameObject.Find("GyroResetter").SendMessage("resetResetter");

			}



			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			
			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}
			
			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		}
		
		#endregion // PRIVATE_METHODS
	}
}
