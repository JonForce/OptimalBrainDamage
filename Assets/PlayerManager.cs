using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public Object playerPrefab;

    private ArrayList players = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    Debug.Log("DEVICE New : " + device.displayName);
                    if (device is Gamepad) {
                        bool deviceAlreadyInUse = false;
                        foreach (Object o in players)
                        {
                            PlayerController player = (PlayerController) o;
                            if (player.GetGamepad() == device)
                            {
                                deviceAlreadyInUse = true;
                            }
                        }
                        
                        if (!deviceAlreadyInUse)
                        {
                            Debug.Log("Adding new Player");
                            GameObject newObject = Instantiate(playerPrefab) as GameObject;
                            PlayerController newController = newObject.GetComponent<PlayerController>();
                            players.Add(newController);
                            newController.SetGamepad((Gamepad)device);
                        }
                    }
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    Debug.Log("DEVICE Disconnected : " + device.displayName);
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log("DEVICE Reconnected : " + device.displayName);
                    // Plugged back in.
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("DEVICE Removed : " + device.displayName);
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
