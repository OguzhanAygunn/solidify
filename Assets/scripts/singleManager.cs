using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class singleManager : MonoBehaviour
{
    [SerializeField] Obi.ObiSolver obisolver;
    Obi.ObiEmitter emitter;
    GameObject obj;
    [SerializeField] Vector3 targetPos;
    Touch touch;
    // Start is called before the first frame update

    private void Awake()
    {
        emitter = GameObject.FindObjectOfType<Obi.ObiEmitter>();
        emitter.speed = 0;
    }
    void Start()
    {
        obj = obisolver.gameObject;
        Invoke(nameof(emitterActive), 1.3f);
    }

    void emitterActive()
    {
        emitter.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        dampingOP();
        rotateOP();
    }

    void dampingOP()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (obisolver.parameters.damping == 1)
            {
                obisolver.parameters.damping = 0;
            }
            else
            {
                obisolver.parameters.damping = 1;
                obj.transform.DOMove(targetPos, 1).SetDelay(1f);
            }
        }
    }

    void rotateOP()
    {
        if (Input.GetKey(KeyCode.A))
        {
            obj.transform.Rotate(Vector3.down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            obj.transform.Rotate(Vector3.up);
        }
        if (Input.GetKey(KeyCode.W))
        {
            obj.transform.Rotate(Vector3.right);
        }
        if (Input.GetKey(KeyCode.S))
        {
            obj.transform.Rotate(Vector3.left);
        }
    }
}
