using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance{
        get{
            return _instance;
        }
    }
    public int diamondNumber;
    private bool isGamePaused = true;
    public TextMeshProUGUI text;
    public GameObject ghostText;
    public GameObject[] monsters;
    public AudioSource BGM1;
    public AudioSource BGM2;
    public AudioSource monsterSFX;
    public AudioSource getDiamondSFX;
    public AudioSource ghostAppear;
    private bool playerIsBeingChasing;
    private InputManager inputManager;
    private Coroutine coroutine;
    public GameObject passDoor;
    bool winGame;
    void Awake(){
        if(_instance != null && _instance != this)
            Destroy(this);
        else{
            _instance = this;
        }
        inputManager = InputManager.Instance;
    }
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(diamondNumber > 0)
            text.text = "X " + diamondNumber;
        else
            text.text = "Find the green door";

    }

    void LateUpdate(){
        if((playerIsBeingChasing && !getMonsterChasing(0) && !getMonsterChasing(1) && !getMonsterChasing(2) && !getMonsterChasing(3)) ||
           (!playerIsBeingChasing && (getMonsterChasing(0) || getMonsterChasing(1) || getMonsterChasing(2) || getMonsterChasing(3)))){
            if(!playerIsBeingChasing)
                monsterSFX.Play();
            playerIsBeingChasing = !playerIsBeingChasing;

            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(changeBGM(playerIsBeingChasing ? 2 : 1));
        }
    }

    void startGame(){
        diamondNumber = 173;
        playerIsBeingChasing = false;
        winGame = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void win(){
        for(int i = 0; i < 4;++i)
            monsters[i].SetActive(false);
        winGame = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnGUI(){
        if(winGame){
            float w = Screen.width;
            float h = Screen.height;

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 70;
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontStyle = FontStyle.Bold;

            GUI.Box(new Rect(10,10,w - 20,h - 20),"");
            GUI.Box(new Rect(10,10,w - 20,h - 20),"");

            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            labelStyle.fontSize = 50;
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(400,300,1200,50),"YOU WIN!!",labelStyle);

            if(GUI.Button(new Rect(600,h * 2 / 3,700,150),"Back to start scene",buttonStyle))
                SceneManager.LoadScene("MainScene");
        }
    }

    public void getDiamond(){
        diamondNumber -= 1;
        getDiamondSFX.Play();
        if(diamondNumber == 140)// 140
            StartCoroutine(activeMoster(0));
        else if(diamondNumber == 100) // 100
            StartCoroutine(activeMoster(1));
        else if(diamondNumber == 50) // 50
            StartCoroutine(activeMoster(2));
        else if(diamondNumber == 25)// 25
            StartCoroutine(activeMoster(3));
        else if(diamondNumber == 0)
            activePassDoor();
    }

    IEnumerator activeMoster(int index){
        Vector3 pos;
        if(index == 0)
            pos = new Vector3(45,0,35);
        else if(index == 1)
            pos = new Vector3(-30,0,122);
        else if(index == 2)
            pos = new Vector3(-15,0,-23);
        else
            pos = new Vector3(-63,0,29);
        monsters[index] = Instantiate(monsters[index], pos, Quaternion.identity);
        monsters[index].GetComponent<MosterNav>().target = GameObject.Find("Player").transform;
        monsters[index].SetActive(true);
        ghostAppear.Play();
        ghostText.SetActive(true);

        yield return new WaitForSeconds(3f);

        ghostText.SetActive(false);
    }

    void activePassDoor(){
        passDoor.SetActive(true);
    }


    bool getMonsterChasing(int index){
        return monsters[index].GetComponent<MosterNav>().isChasing;
    }

    IEnumerator changeBGM(int index){

        float time = 0f;

        while(time < 1){
            if(index == 1){
                BGM1.volume = Mathf.Min(BGM1.volume + Time.deltaTime,1);
                BGM2.volume = Mathf.Max(BGM2.volume - Time.deltaTime,0);
            }else{
                BGM2.volume = Mathf.Min(BGM2.volume + Time.deltaTime,1);
                BGM1.volume = Mathf.Max(BGM1.volume - Time.deltaTime,0);
            }
            time += Time.deltaTime;
            yield return null;
        }
    }
}
