using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEffect : MonoBehaviour
{
    public GameObject Trail;
    private int MeteorIndex;
    private Meteor MS;
    void Start()
    {
        Trail.SetActive(false);

        MeteorIndex = 0;
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
            if(MS.Trigger(MeteorIndex,transform.position,(float)GetComponent<HeroKnight>().m_facingDirection))
                MeteorIndex++;
        }
    }
}
