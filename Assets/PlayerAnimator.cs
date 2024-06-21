using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator _plyaerAnim;
    [SerializeField] int _animationIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _plyaerAnim.SetInteger("PlayerAnimation", _animationIndex);
    }
}
