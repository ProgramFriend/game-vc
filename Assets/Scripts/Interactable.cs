using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool hasInteracted = true;

    public virtual void CallInteraction()
    {
        hasInteracted = false;
    }

    public virtual void CallOffInteraction()
    {
        hasInteracted = true;
    }

    public void Update()
    {
        if (hasInteracted == false && Input.GetButtonDown("Submit"))
        {
            Interact();
            hasInteracted = true;
        }
    }
    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }
}
