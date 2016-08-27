using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    #region Variables (private)

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform followXForm;

    private Vector3 lookDir;
    private Vector3 targetPosition;

    //Smooth and damping the camera Moviment
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;

    #endregion

    #region Variables (public)

    #endregion
    #region Metodos (Unity)
    // Use this for initialization
    void Start () {
        followXForm = GameObject.FindGameObjectWithTag("PlayerFollow").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {

        Vector3 characterOffSet = followXForm.position + new Vector3(0f, distanceUp, 0f);

        //Calcula a distancia entre o jogador e a camera, Seta o Y pra 0 e normaliza a a direçao
        lookDir = characterOffSet - transform.position;
        lookDir.y = 0;
        lookDir.Normalize();




        targetPosition = characterOffSet + followXForm.up * distanceUp - lookDir * distanceAway;
        Debug.DrawRay(followXForm.position, Vector3.up * distanceUp, Color.red);
        Debug.DrawRay(followXForm.position, -1f * followXForm.forward * distanceAway, Color.blue);
        Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);

        CompensateForWalls(characterOffSet, ref targetPosition);
        SmoothPosition(transform.position, targetPosition);

        transform.LookAt(followXForm);

    }

    #endregion

    #region Metodos
    private void SmoothPosition(Vector3 fromPos,Vector3 toPos)
    {
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
    private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        //Checar se tem objectos entre a camera e o jogador
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
        }
    }
    #endregion

}
