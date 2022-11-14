using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI(){
        float w = Screen.width;
        float h = Screen.height;
        GUI.BeginGroup(new Rect(300,h / 5,1300,700));

            GUIStyle labelStyle = GUI.skin.label;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            labelStyle.fontSize = 50;
            labelStyle.normal.textColor = Color.black;
            labelStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(100,0,1200,50),"Tutorial",labelStyle);
            GUI.Label(new Rect(100,50,1200,50),"WASD: Move",labelStyle);
            GUI.Label(new Rect(100,100,1200,50),"Shift: Run",labelStyle);
            GUI.Label(new Rect(100,150,1200,50),"Esc: Pause",labelStyle);
            GUI.Label(new Rect(100,220,1200,50),"Collect diamonds and escape from the maze",labelStyle);
            GUIStyle buttonStyle = GUI.skin.button;
            buttonStyle.fontSize = 80;
            if(GUI.Button(new Rect(350,300,300,100),"START"))
                SceneManager.LoadScene("Maze");
            if(GUI.Button(new Rect(750,300,300,100),"QUIT"))
                Application.Quit();
        GUI.EndGroup();

    }
}
