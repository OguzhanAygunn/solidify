using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Vector3 offset;
    [SerializeField] float toTargetTime;
    Painter painter;
    // Start is called before the first frame update
    void Start()
    {
        painter = GameObject.FindObjectOfType<Painter>();
        transform.DOMove(targetPos.position + offset,toTargetTime).SetDelay(0.5f).OnComplete( () => {
            painter.ToFirstPosAndRot();
        });
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        LookAtOP();
    }

    private void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPos.position + offset, 10*Time.deltaTime);
    }

    void LookAtOP()
    {
        transform.LookAt(targetPos);
    }
}
