using System;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject plane;
    public GameObject[] trails;
    public float radius = 1f;

    private GameObject[] l;
    private Quaternion rotation;

    private Vector3 v;
    void Start()
    {
        rotation = plane.GetComponent<Transform>().rotation;
        l = new GameObject[trails.Length];
        for(int i = 0;i < 4; ++i){
            l[i] = Instantiate<GameObject>(trails[i], new Vector3(0,0,0),new Quaternion(0,0,0,0));
            l[i].transform.SetParent(plane.transform);
            l[i].transform.position = new Vector3(radius,0,0);
            l[i].transform.RotateAround(plane.transform.position,new Vector3(0,1,0),-45f + 90f * i);
            l[i].transform.RotateAround(plane.transform.position,new Vector3(1,0,0),rotation.eulerAngles.x);
        }
        v = l[0].transform.position;
        v.x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < 4; ++i){
            l[i].transform.RotateAround(plane.transform.position,Vector3.Cross(new Vector3(1,0,0),v),180 * Time.deltaTime);
        }
    }
}
