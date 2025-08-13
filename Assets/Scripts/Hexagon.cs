using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Hexagon : MonoBehaviour,IPoolable
{
    public MeshRenderer mesh;

    [OnValueChanged("SetColor")]
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
    public void Rotate180(Vector3 rotateAxis, float duration, float delay, bool isLocalAxis = false)
    {
        
        Quaternion rotation = Quaternion.AngleAxis(180, rotateAxis);
        transform
            .DORotateQuaternion(rotation * transform.rotation, duration)
            .SetDelay(delay)
            .SetEase(Ease.OutQuad);
    }
    public void SetGetShadows(bool val)
    {
        if (!val)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mesh.staticShadowCaster = true;
        }
        else mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
    public void SetColor(Colors colors=Colors.Null)
    {

        color = colors;
        mesh.material = Resources.Load<Material>("Materials/" + color.ToString());

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
