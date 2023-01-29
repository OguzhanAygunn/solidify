using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool Paintable;
    public GameObject targetSolverObj;
    SolverController solverController;
    [SerializeField] GameObject[] figures;
    int figureIndex = 0;
    private void Awake()
    {
        Instance = this;
        targetSolverObj = GameObject.FindObjectOfType<Obi.ObiSolver>().gameObject;
        solverController = GameObject.FindObjectOfType<SolverController>();
    }

    public void PaintIsOverOP()
    {
        SolverController sc = targetSolverObj.GetComponent<SolverController>();
        sc.paintIsOverOP();
        SolverController.Instance.solverChangeDamping(1f);
        Painter.Instance.visibility(false);
        Paintable = false;
    }

    public void nextFigure()
    {
        FigureController fc = figures[figureIndex].GetComponent<FigureController>();
        fc.removeOP();

        figureIndex++;
        if(figureIndex < figures.Length)
        {
            fc = figures[figureIndex].gameObject.GetComponent<FigureController>();
            fc.toTablePos();
        }
    }
}
