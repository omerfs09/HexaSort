using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Hexagon : MonoBehaviour,IPoolable
{
    public MeshRenderer mesh;
    public Colors color;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool rotate = false;
    private Vector3 axis;
    public void Rotate180(Vector3 rotateAxis, float duration,float delay)
    {
        Quaternion rotation = Quaternion.AngleAxis(180, rotateAxis);
        transform.DORotate((rotation*transform.rotation).eulerAngles, duration, RotateMode.Fast).SetDelay(delay).SetEase(Ease.OutQuad);
        //transform.DORotateQuaternion(transform.rotation * rotation, duration).SetDelay(delay);
    }

    public void SetGetShadows(bool val)
    {
        if (!val)
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        else mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
    public void SetColor(Colors colors)
    {
        mesh.material = Resources.Load<Material>("Materials/" + colors.ToString());
        color = colors;
    }
    public void MoveTo(Vector3 point,bool animate = false)
    {
        if (animate) transform.DOMove(point, 1f).SetEase(Ease.InOutBack);
        else
            transform.position = point;
        
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        gameObject.transform.SetParent(PoolManager.Instance.transform);
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
    }
}
public enum Colors
{
    Red,
    Blue,
    Green,
    Yellow,
    Black,
    White,
    Pink,
    Purple,
    Cyan,
    Null
}
