using UnityEngine;

	public class NPC : MonoBehaviour,IInteractable
	{
		public void OnEnterInteract()
		{
			Debug.Log("Talk To Someone!");
		}
		public void OnInteract()
		{
			Debug.Log("Sedang mengobrol");
		}
		
		public void OnExitInteract()
		{
			Debug.Log("Pesan talk hilang");
		}
	}
