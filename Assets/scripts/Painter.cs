using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Obi;

public class Painter : MonoBehaviour
{
    public static Painter Instance;
    Touch touch;
    [SerializeField] float moveSens,moveSpeed;
    ObiEmitter emitter;
    bool emitterSpeedActive;
    [SerializeField] LayerMask paintableLayer;

    [SerializeField] Vector3 targetPos,targetRot;
    [SerializeField] MeshRenderer[] renderers;
    ObiSolver solver;
    Vector3 _targetpos,defaultScale;
    List<int> deletableParticles= new List<int>();
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        emitter = GetComponentInChildren<ObiEmitter>();
        solver = GameObject.FindObjectOfType<ObiSolver>();
        Debug.Log(emitter.particleCount);
        Debug.Log(emitter.activeParticleCount);
        defaultScale = transform.localScale;

        solver.OnCollision += Solver_OnCollision;
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        if(GameManager.Instance.Paintable)
        EmitterOP();
    }
    void Update()
    {
        //RotateOP();
        MoveOP();
    }

    void RotateOP()
    {
        if (Input.touchCount > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(150, transform.right), Time.deltaTime * 150);
            if (transform.rotation == Quaternion.AngleAxis(150, transform.right))
            {
                emitterSpeedActive = true;
            }
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(270, transform.right), Time.deltaTime * 150);
            emitterSpeedActive = false;
        }
    }

    private void Solver_OnCollision(ObiSolver s, ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        foreach (Oni.Contact contact in e.contacts)
        {
            var col = world.colliderHandles[contact.bodyB].owner;
            if (col.gameObject.layer == LayerMask.NameToLayer("Paintable"))
            {
                int index = contact.bodyA;
                bool x = deletableParticles.Contains(index);
                if(x == false)
                {
                    deletableParticles.Add(index);
                }
            }
        }
    }
    void MoveOP()
    {
        if(Input.touchCount > 0 && GameManager.Instance.Paintable)
        {
            touch = Input.GetTouch(0);
            Vector3 deltaPos = touch.deltaPosition;
            _targetpos += new Vector3(-deltaPos.y, 0, deltaPos.x) / moveSens * Time.deltaTime;
            //transform.position += new Vector3(-deltaPos.y,0,deltaPos.x) / moveSens * Time.deltaTime;
        }

        if (GameManager.Instance.Paintable)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetpos, moveSpeed * Time.deltaTime);
        }
    }

    public void ToFirstPosAndRot()
    {
        transform.DOLocalMove(targetPos,1f);
        transform.DORotate(targetRot,1f).OnComplete( () => {
            _targetpos = transform.position;
            GameManager.Instance.Paintable = true;
        });
    }

    void EmitterOP()
    {
        if (/*Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, paintableLayer)*/ true)
        {
            if (Input.touchCount > 0)
            {
                emitter.speed = 10;
            }
            else
            {
                emitter.speed = 0 /*Mathf.MoveTowards(emitter.speed, 0, 3 * Time.deltaTime)*/;
            }
        }
        else
        {
            emitter.speed = 0;
        }
    }

    public void visibility(bool visibility)
    {
        if (!visibility)
        {
            foreach(MeshRenderer mr in renderers)
            {
                Color color = mr.material.color;
                color.a = 0;
                mr.material.DOColor(color,0.25f);
            }
        }
        else
        {
            foreach(MeshRenderer mr in renderers)
            {
                Color color = mr.material.color;
                color.a = 1;
                mr.material.DOColor(color, 0.25f);
            }
        }
    }

    public void otherParticleDestroyOP()
    {
        for (int i = 0; i < solver.userData.count; ++i)
        {
            solver.colors[i] = solver.userData[i];
        }
    }

    public void resetParticle()
    {
        Vector3 scale = defaultScale;
        scale.x = 0;
        scale.z = 0;
        emitter.KillAll();

        GameManager.Instance.Paintable = true;
    }

}
