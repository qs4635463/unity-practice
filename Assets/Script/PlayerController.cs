using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private InputManager inputManager;
    private Transform cameraTransform;
    [SerializeField]
    private Transform torch;
    public AudioSource footStepSFX;
    private Vector3 playerMove;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
        Cursor.visible = false;

        StartCoroutine(playFootStepSFX());
    }

    void Update()
    {

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //Debug.Log(inputManager);
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        Vector3 angle = cameraTransform.eulerAngles;
        torch.transform.rotation = Quaternion.Euler(angle.x,angle.y,angle.z);
        controller.Move(move * Time.deltaTime * playerSpeed * (inputManager.PlayerIsRunning() ? 1.8f : 1f));

        playerMove = move;
    }

    IEnumerator playFootStepSFX(){
        while(true){
            if(playerMove != Vector3.zero){
                footStepSFX.Play();
                if(inputManager.PlayerIsRunning()){
                    yield return new WaitForSeconds(0.27f);
                }else
                    yield return new WaitForSeconds(0.4f);
            }else{
                yield return null;
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.name == "Pass"){
            gameManager.win();
        }else if(other.tag == "ghost"){
            SceneManager.LoadScene("MainScene");
        }
    }
}
