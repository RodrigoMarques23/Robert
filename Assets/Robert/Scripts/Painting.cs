using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    public bool lockedByPassword;

    public Animator anim;

    public void OpenClose()
    {
        anim.SetTrigger("Painting");
    }
}
