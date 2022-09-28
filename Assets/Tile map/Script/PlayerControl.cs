using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 700f;
    private Rigidbody rb;
    public GameObject mainCamera;
    public GameObject rotateCamera;
    public GameObject fixCamera;

    public Camera main;
    public GameObject parent;

    void Start()
    {
        rb = parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(inputX * Time.deltaTime * speed, 0,inputY * Time.deltaTime * speed));

        // if(mainCamera)
        //       transform.rotation = Quaternion.LookRotation(mainCamera.transform.position, Vector3.up);
        //Debug.Log(CinemachineCore.Instance.IsLive(mainCamera));

        if(Input.GetKeyDown("space"))
             StartCoroutine(OnCameraChanged());
        if(Input.GetKeyDown("z"))
            StartCoroutine(test());
    }

    IEnumerator OnCameraChanged(){
        mainCamera.SetActive(false);
        rotateCamera.SetActive(true);

        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(3);

        mainCamera.SetActive(true);
        rotateCamera.SetActive(false);

    }

    IEnumerator test(){
        mainCamera.SetActive(false);
        fixCamera.SetActive(true);

        float count = 0;
        while(count < 2f){
            count += Time.deltaTime;

            //Quaternion r = new Quaternion()
            Vector3 see = new Vector3(main.transform.position.x,transform.position.y,main.transform.position.z);
            see -= transform.position;
            transform.LookAt(see);
            transform.rotation = Quaternion.LookRotation(Vector3.Reflect(see,see));
            
            Debug.Log(main.transform.forward);
            Debug.Log(see);
            Debug.Log(Vector3.Reflect(see,see));
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(2f);

        mainCamera.SetActive(true);
        fixCamera.SetActive(false);

        count = 0;
        while(count < 2f){
            count += Time.deltaTime;

            //Quaternion r = new Quaternion()
            Vector3 see = new Vector3(main.transform.position.x,transform.position.y,main.transform.position.z);
            see -= transform.position;
            transform.LookAt(see);
            transform.rotation = Quaternion.LookRotation(Vector3.Reflect(see,see));
            
            // Debug.Log(main.transform.forward);
            // Debug.Log(see);
            yield return new WaitForFixedUpdate();
        }
    }
}
