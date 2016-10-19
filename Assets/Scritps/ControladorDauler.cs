using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ControladorDauler : MonoBehaviour {

    #region Private Vars
    private int score;
    private int faseAtual;
    #endregion

    #region Public Vars
    public int _score { get { return score; } }
    public int _faseAtual { get { return faseAtual; } }
    #endregion

    public void SetScore(int x)
    {
        score += x;
    }



}
