using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimations : MonoBehaviour
{
    private Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!myAnim) { return; }

        AnimatorStateInfo state = myAnim.GetCurrentAnimatorStateInfo(0);
        myAnim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
