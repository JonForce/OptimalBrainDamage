using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Material playSelected;
    public Material settingsSelected;
    public Material creditsSelected;
    public Material quitSelected;

    private int selected = 0;
    private DateTime lastSwitched = System.DateTime.Now;
    private Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        materials = new Material[] { playSelected, settingsSelected, creditsSelected, quitSelected };
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.aButton.IsPressed() && selected == 0)
            {
                SceneManager.LoadScene("LevelScene");
            }
            if (Gamepad.current.enabled && (System.DateTime.Now - lastSwitched).Duration().Milliseconds > 250)
            {
                if (Gamepad.current.leftStick.y.ReadValue() < 0)
                {
                    selected = (selected + 1) % materials.Length;
                    lastSwitched = System.DateTime.Now;
                }
                else if (Gamepad.current.leftStick.y.ReadValue() > 0)
                {
                    selected = selected - 1;
                    if (selected < 0) selected = materials.Length - 1;
                    lastSwitched = System.DateTime.Now;
                }
                Material newMaterial = materials[selected];
                GetComponent<MeshRenderer>().material = newMaterial;
            }
        }
    }
}
