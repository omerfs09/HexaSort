using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour,IPoolable
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveTo(Vector3 point)
    {
        transform.position = point;
    }

    public void OnSpawn()
    {
        
    }

    public void OnDespawn()
    {
        
    }
}
