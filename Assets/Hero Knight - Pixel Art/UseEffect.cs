using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEffect : MonoBehaviour
{
    public GameObject Trail;

    private Meteor MS;
    void Start()
    {
        Trail.SetActive(false);

        MS = GetComponent<Meteor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")){
            if(Trail.activeInHierarchy)
                Trail.SetActive(false);
            else
                Trail.SetActive(true);
        }else if(Input.GetKeyDown("2")){
            MS.Trigger(0,transform.position,1);
        }else if(Input.GetKeyDown("3")){
            MS.Trigger(1,transform.position,1);
        }else if(Input.GetKeyDown("4")){
            MS.Trigger(2,transform.position,1);
        }
    }
}
