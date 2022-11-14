using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MosterNav : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;
    public NavMeshAgent agent;
    public float navDistance;
    public float wanderSpeed;
    public float wanderingDistanceToChangeDirection;
    public float rayMaxDistance;
    private Vector3 moveDirection;
    public bool isChasing;
    private float coolTime;
    private float coolTimeForChasing;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();//取得物件的Agent元件
        navDistance = 17f;

        wanderSpeed = 750f;
        wanderingDistanceToChangeDirection = 2.1f;
        rayMaxDistance = 8f;
        moveDirection = transform.forward;
        isChasing = false;
        coolTime = 0f;
    }
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit,wanderingDistanceToChangeDirection);
        if(coolTimeForChasing >= 0.5f){
            if(Vector3.Distance(transform.position,target.position) <= navDistance){
                agent.SetDestination(target.position);
                rb.velocity = Vector3.zero;
                agent.isStopped = false;
                isChasing = true;
                coolTimeForChasing = 0;
            }else{
                if(isChasing){
                    agent.isStopped = true;
                    moveDirection = updateDirection();
                    coolTime = 0f;
                    coolTimeForChasing = 0;
                }else if(coolTime > 0.3f){
                    moveDirection = checkDirection();
                    coolTime = 0f;
                }
                transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                rb.velocity = moveDirection * wanderSpeed * Time.deltaTime;

                coolTime += Time.deltaTime;
                isChasing = false;
            }
        }
        coolTimeForChasing += Time.deltaTime;
    }

    Vector3 checkDirection(){
        List<int> dir = new List<int>();
        bool hasBranch = false;
        for(int i = 0;i < 8;++i){
            Vector3 rayDir = Quaternion.AngleAxis(i * 45f,Vector3.up) * transform.forward;
            int layer = 1 << 8;
            layer = ~layer;
            layer = layer ^ (1 << 5);
            if(!Physics.Raycast(transform.position, rayDir,rayMaxDistance * (i % 2 == 1 ? 2.5f : (i + 2) % 4 == 0 ? 1.2f : 1),layer) && i != 4){
                dir.Add(i);
                if(i != 0)
                    hasBranch = true;
            }
        }
        if(dir.Count == 7)
            return transform.forward;
        if(hasBranch){
            float dirIndex = Mathf.Floor(Random.Range(0f,(float)dir.Count - 0.01f));

            return Quaternion.AngleAxis(45f * dir[(int)dirIndex],Vector3.up) * transform.forward;
        }else
            return transform.forward;
    }

    Vector3 updateDirection(){
        List<int> dir = new List<int>();
        for(int i = 0;i < 8;++i){
            Vector3 rayDir = Quaternion.AngleAxis(i * 45f,Vector3.up) * Vector3.forward;
            if(!Physics.Raycast(transform.position + new Vector3(0,0.2f,0), rayDir,rayMaxDistance)){
                dir.Add(i);
            }
        }

        if(dir.Count == 0)
            return Vector3.forward;
        float dirIndex = Mathf.Floor(Random.Range(0f,(float)dir.Count - 0.01f));

        return Quaternion.AngleAxis(45f * dir[(int)dirIndex],Vector3.up) * Vector3.forward;
    }
}
