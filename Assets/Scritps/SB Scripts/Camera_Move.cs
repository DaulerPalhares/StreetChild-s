using UnityEngine;
using System.Collections;

public class Camera_Move : MonoBehaviour {
    public float velocidade;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (new Vector3(-velocidade, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (new Vector3(velocidade, 0.0f, 0.0f));
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (new Vector3(0.0f, 0.0f, velocidade));
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (new Vector3(0.0f, 0.0f, -velocidade));
        }


        if (Input.mouseScrollDelta.y > 0 && GetComponent<Camera>().fieldOfView >= 20)
        { // zoom out 
            GetComponent<Camera>().fieldOfView -= 3;
        }

        if (Input.mouseScrollDelta.y < 0 && GetComponent<Camera>().fieldOfView <= 60)
        {
            GetComponent<Camera>().fieldOfView += 3;

        }
    }
}
