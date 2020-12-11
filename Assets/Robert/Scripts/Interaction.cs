using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public enum InteractiveType { PICKABLE, INTERACT_ONCE, INTERACT_MULTI, INDIRECT };

    public InteractiveType type;
    public string interactionText;
    public string requirementText;
    public Texture icon;
    public Interaction[] requirements;
    public Interaction[] interactionChain;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_animator != null)
            _animator.SetTrigger("Interact");

        ProcessInteractionChain();

        if (type == InteractiveType.INTERACT_ONCE)
            GetComponent<Collider>().enabled = false;
    }

    private void ProcessInteractionChain()
    {
        if (interactionChain != null)
        {
            for (int i = 0; i < interactionChain.Length; ++i)
                interactionChain[i].Interact();
        }
    }

}
