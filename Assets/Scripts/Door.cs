using UnityEngine;


public class Door : MonoBehaviour, IInteractable
	{
		public void OnEnterInteract()
		{
			Debug.Log("Open Door!");
		}
		public void OnInteract()
		{
			Debug.Log("Pintu terbuka");
		}
		
		public void OnExitInteract()
		{
			Debug.Log("Pesan Open Door Hilang");
		}
	}
