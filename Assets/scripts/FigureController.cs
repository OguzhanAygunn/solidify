using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FigureController : MonoBehaviour
{
    Vector3 tablePos;
    // Start is called before the first frame update
    void Start()
    {
        tablePos = GameObject.FindGameObjectWithTag("PaintableObj").gameObject.transform.position;
    }

    public void toTablePos()
    {
        Vector3 targetPos = transform.position;
        targetPos.y += 2f;
        transform.position = targetPos;
        transform.DOMove(tablePos,1f);
    }

    public void removeOP()
    {
        SolverController.Instance.solverChangeDamping(0);
        transform.DOScale(Vector3.zero,0.65f).OnComplete( () => {
            Painter.Instance.resetParticle();
            Destroy(this.gameObject);
        });
    }
}
