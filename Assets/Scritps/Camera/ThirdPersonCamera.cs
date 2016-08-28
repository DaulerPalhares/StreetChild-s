using UnityEngine;
using System.Collections;

/// <summary>
/// Struct to hold data for aligning camera
/// </summary>

struct CameraPosition
{
    private Vector3 _position;
    private Transform _xForm;

    public Vector3 position { get { return _position; } set { _position = value; } }
    public Transform xForm { get { return _xForm; } set { _xForm = value; } }

    public void Init(string camName, Vector3 pos, Transform transform,Transform parent)
    {
        _position = pos;
        _xForm = transform;
        _xForm.name = camName;
        _xForm.parent = parent;
        _xForm.localPosition = Vector3.zero;
        _xForm.localPosition = _position;

    }
}


[RequireComponent(typeof(BarEffect))]
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
    [SerializeField]
    private float wideScreen = 0.2f;
    [SerializeField]
    private float targetingTime = 0.5f;
    [SerializeField]
    private float firstPersonThreshold = 0.5f;
    [SerializeField]
    private float firstPersonLookSpeed = 1.5f;
    [SerializeField]
    private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 90.0f);

    //Smooth and damping the camera Moviment
    private Vector3 velocityCamSmooth = Vector3.zero;
    [SerializeField]
    private float camSmoothDampTime = 0.1f;

    //Private Global
    private Vector3 lookDir;
    private Vector3 targetPosition;
    private BarEffect barEffect;
    private CamStates camState = CamStates.Behind;
    private float xAxisRot = 0.0f;
    private CameraPosition firstPersonCamPos;
    private float lookWeight;

    #endregion

    #region Variables (public)

    public CamStates CamState { get { return camState; } }

    public enum CamStates
    {
        Behind, //Camera behind the player moving free
        FirstPerson,
        Target, //Camera lock the position to behind the player
        Free
    }

    #endregion
    
    #region Metodos (Unity)
    // Use this for initialization
    void Start () {
        followXForm = GameObject.FindGameObjectWithTag("PlayerFollow").transform;
        lookDir = followXForm.forward;
        barEffect = GetComponent<BarEffect>();
        if (!barEffect)
        {
            Debug.LogError("Attach the BarEffect to the camera", this);
        }

        firstPersonCamPos = new CameraPosition();
        firstPersonCamPos.Init("First Person Camera", new Vector3(0f, 1.6f, 0.2f), new GameObject().transform, followXForm.transform);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        //Inputs To move the camera in first person camera
        float rightX = Input.GetAxis("RightStickX");
        float rightY = Input.GetAxis("RightStickY");
        float leftX = Input.GetAxis("Horizontal");
        float leftY = Input.GetAxis("Vertical");

        Vector3 characterOffSet = followXForm.position + new Vector3(0f, distanceUp, 0f);
        Vector3 lookAt = characterOffSet;

        //Determine the camera State
        if (Input.GetAxis("Target") > 0.01f)
        {
            barEffect.coverage = Mathf.SmoothStep(barEffect.coverage, wideScreen, targetingTime);
            camState = CamStates.Target;
        }
        else
        {
            barEffect.coverage = Mathf.SmoothStep(barEffect.coverage, 0, targetingTime);
            if(rightY > firstPersonThreshold || Input.GetMouseButtonDown(1))
            {
                xAxisRot = 0;
                lookWeight = 0;
                camState = CamStates.FirstPerson;
            }
            if ((camState == CamStates.FirstPerson && Input.GetButton("ExitFPV")) || Input.GetMouseButtonUp(1))
            {
                camState = CamStates.Behind;
            }
        }

        switch (camState)
        {
            case CamStates.Behind:
                //Calcula a distancia entre o jogador e a camera, Seta o Y pra 0 e normaliza a a direçao
                lookDir = characterOffSet - transform.position;
                lookDir.y = 0;
                lookDir.Normalize();
                targetPosition = characterOffSet + followXForm.up * distanceUp - lookDir * distanceAway;
            break;
            case CamStates.Target:
                lookDir = followXForm.forward;
                targetPosition = characterOffSet + followXForm.up * distanceUp - lookDir * distanceAway;
            break;
            case CamStates.FirstPerson:
                //Looking up and down
                xAxisRot += (leftY * firstPersonLookSpeed);
                xAxisRot = Mathf.Clamp(xAxisRot, firstPersonXAxisClamp.x, firstPersonXAxisClamp.y);
                firstPersonCamPos.xForm.localRotation = Quaternion.Euler(xAxisRot, 0, 0);

                Quaternion rotationShift = Quaternion.FromToRotation(transform.forward, firstPersonCamPos.xForm.forward);
                transform.rotation = rotationShift * transform.rotation;

                //Move the camera to FPV
                targetPosition = firstPersonCamPos.xForm.position;
                
                lookAt = Vector3.Lerp(transform.position + transform.forward, lookAt, Vector3.Distance(transform.position, firstPersonCamPos.xForm.position));

            break;
        }



        Debug.DrawRay(followXForm.position, Vector3.up * distanceUp, Color.red);
        Debug.DrawRay(followXForm.position, -1f * followXForm.forward * distanceAway, Color.blue);
        Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);

        CompensateForWalls(characterOffSet, ref targetPosition);
        SmoothPosition(transform.position, targetPosition);

        transform.LookAt(lookAt);

    }

    #endregion

    #region Metodos

    private void SmoothPosition(Vector3 fromPos,Vector3 toPos)
    {
        transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }

    //If the camera collide with the wall stop where hit
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
