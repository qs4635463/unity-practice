using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    private InputManager inputManager;

    public static bool isGamePaused = false;
    private float brightness = 1;
    public Light overallLight;
    void Start()
    {
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.PlayerPause()){
            if(isGamePaused)
                Resume();
            else
                Pause();
        }

    }

    void OnGUI(){
        if(isGamePaused){
            float w = Screen.width;
            float h = Screen.height;

            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 70;

            Texture2D consoleBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
            consoleBackground.SetPixel(0, 0, new Color(1, 1, 1, 0.25f));
            buttonStyle.normal.background = consoleBackground;

            GUI.Box(new Rect(10,10,w - 20,h - 20),"");
            GUI.Box(new Rect(10,10,w - 20,h - 20),"");

            if(GUI.Button(new Rect(w * 2 / 5,h * 2 / 3,400,150),"RESUME",buttonStyle))
                Resume();
            
            GUI.BeginGroup(new Rect(w * 1.4f / 5,h * 1 / 3,800,300));

                GUIStyle labelStyle = GUI.skin.label;
                labelStyle.alignment = TextAnchor.MiddleLeft;
                labelStyle.normal.textColor = Color.white;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.fontSize = 70;
                GUI.Label(new Rect(100,100,400,150),"Brightness",labelStyle);

                GUI.BeginGroup(new Rect(500,100,250,200));

                    GUIStyle sliderBackgroundStyle = new GUIStyle(GUI.skin.box);
                    sliderBackgroundStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 1f, 0.1f ) );
                    GUI.Box(new Rect(0,0,350,100),"",sliderBackgroundStyle);
                    overallLight.intensity = GUI.HorizontalSlider(new Rect(15,50,200,200),overallLight.intensity,0.0f,1f);
                
                GUI.EndGroup();

            GUI.EndGroup();
        }
    }

    private Texture2D MakeTex( int width, int height, Color col )
    {
        Color[] pix = new Color[width * height];
        for( int i = 0; i < pix.Length; ++i )
        {
            pix[ i ] = col;
        }
        Texture2D result = new Texture2D( width, height );
        result.SetPixels( pix );
        result.Apply();
        return result;
    }

    public void Resume(){
        isGamePaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause(){
        isGamePaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
