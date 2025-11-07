using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InteractableHandler : MonoBehaviour{
		public float radiusInteract = 2;
		private IInteractable currentInteractable;
		private List<IInteractable> tempInteractable = new();
		public Button interactButton;

		private void Start()
		{
			
			if (interactButton != null)
				interactButton.onClick.AddListener(InteractButton);
		}

		private void InteractButton()
		{
			if (currentInteractable != null)
				currentInteractable.OnInteract();
		}

		private void Update()
		{
			interactButton.gameObject.SetActive(false);
			DetectInteractable();
			currentInteractable = null;
			if (tempInteractable.Count > 0)
			{
				currentInteractable = tempInteractable[0];
			}
			if (currentInteractable == null) return;
			interactButton.gameObject.SetActive(true);
		}
		

		private void DetectInteractable()
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, radiusInteract);
			var newListInteractable = new List<IInteractable>(); 

			foreach (var newCollider in colliders )
			{
				var interactable  = newCollider.GetComponent<IInteractable>();
				if (interactable != null )
				{
					newListInteractable.Add(interactable);
				}
			}

			for (int i = tempInteractable.Count-1; i >= 0; i--)
			{
				var temp = tempInteractable[i];
			
				if (!newListInteractable.Contains(temp))
				{
					temp.OnExitInteract();
					tempInteractable.RemoveAt(i);
					
				}
			}

			foreach (var newCol in newListInteractable)
			{
				if (!tempInteractable.Contains(newCol)) 
				{
					tempInteractable.Add(newCol);
					newCol.OnEnterInteract();
				}
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position,radiusInteract);
		}
	}
