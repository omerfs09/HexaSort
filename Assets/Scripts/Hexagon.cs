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
        
    }
}
public enum Colors
{
    Red,
    Blue,
    Green,
    Null
}
