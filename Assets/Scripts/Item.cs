using UnityEngine;

	public class Item : MonoBehaviour, IInteractable
	{
		public void OnEnterInteract()
		{
			Debug.Log("Ambil Item!");
		}
		public void OnInteract()
		{
			Debug.Log("Item Diambil");
		}
		
		public void OnExitInteract()
		{
			Debug.Log("Pesan Item Hilang");
		}
	}
