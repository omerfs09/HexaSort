using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer3D : MonoBehaviour
{
    public static Hammer3D Instance;

    [SerializeField] private Animator hammerAnimator;

    [SerializeField] private GameObject meshHolder;

    [SerializeField] private ParticleSystem hitParticle;

    private Colors cubeColor;

    private void Awake()
    {
        Instance= this;
    }

    public void HammerAnimation(Vector3 hammerTargetPos)
    {
        transform.position = hammerTargetPos;
        meshHolder.SetActive(true);
        hammerAnimator.SetTrigger("Hit");
      //  cubeColor = color;
        SFXManager.Instance.SetHaptic(HapticTypes.LightImpact);
    }

    public void HammerHit()
    {

        
    }
}
