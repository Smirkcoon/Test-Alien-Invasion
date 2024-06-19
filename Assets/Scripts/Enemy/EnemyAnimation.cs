using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetAnimActive(AnimName animName)
    {
        animator.Play(animName.ToString());
    }
}

public enum AnimName
{
    Idle,
    Walk,
    Floating
}