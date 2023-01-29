using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SolverController : MonoBehaviour
{
    public static SolverController Instance;
    Vector3 firstPos,defaultScale;
    Obi.ObiSolver solver;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        solver = GetComponent<Obi.ObiSolver>();
        firstPos = transform.position;
        defaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void paintIsOverOP()
    {
        Vector3 targetPos = transform.position;
        targetPos.y += 3.25f;
        targetPos.x += 1.5f;
        transform.DOMove(targetPos,1f).OnComplete( () =>{
            transform.DOMove(firstPos,1f).SetDelay(1.75f).OnComplete( () => {
                Painter.Instance.visibility(true);
                GameManager.Instance.nextFigure();
            });
        });
    }

    public void solverChangeDamping(float _value)
    {
        solver.parameters.damping = _value;
    }

}
