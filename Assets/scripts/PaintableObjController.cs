using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaintableObjController : MonoBehaviour
{
    Vector3 paintIsOverPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void paintIsOverFunc()
    {
        paintIsOverPos = transform.position;
    }
}
