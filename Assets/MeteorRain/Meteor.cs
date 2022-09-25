using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Meteor : MonoBehaviour
{
    
    public GameObject[] meteors;

    [HideInInspector]
    public int meteorNumber;
    private GameObject playingMeteor;
    // Update is called once per frame

    void Start(){
        meteorNumber = meteors.Length;
        playingMeteor = null;
    }
    void Update()
    {

    }

    public bool Trigger(int index,Vector3 playerPosition,float direction){
        if(playingMeteor){
                playingMeteor.GetComponent<VisualEffect>().Stop();
                Destroy(playingMeteor,1f);
                return true;
        }else{
            playingMeteor = Instantiate<GameObject>(meteors[index % meteorNumber],new Vector3(0,0,0),new Quaternion(0,0,0,0));
            VisualEffect VE = playingMeteor.GetComponent<VisualEffect>();
            VE.SetVector3("initialPosition", playerPosition + new Vector3(-5 * direction,20,0));
            VE.SetVector3("Direction", new Vector3(10 * direction,-25,0));
            return false;
        }
    }
}
