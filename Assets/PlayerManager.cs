using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Object playerPrefab;
    public GameObject camera;
    private ArrayList players = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        foreach (InputDevice device in InputSystem.devices)
        {
            if (device is Gamepad)
            {
                Debug.Log("Device : " + device.displayName);
                AddNewPlayer(device);
            }
        }

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
                            PlayerController player = ((GameObject)o).GetComponent<PlayerController>();
                            if (player.GetGamepad() == device)
                            {
                                deviceAlreadyInUse = true;
                            }
                        }
                        
                        if (!deviceAlreadyInUse)
                        {
                            AddNewPlayer(device);
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

        GameObject target = new GameObject();
        camera.GetComponent<LookAt>().target = target.transform;
        camera.GetComponent<Follow>().target = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        camera.GetComponent<LookAt>().target.position = GetMeanPlayerPosition();
        
    }

    private void AddNewPlayer(InputDevice device)
    {
        Debug.Log("Adding new Player " + device.displayName);
        GameObject newObject = Instantiate(playerPrefab) as GameObject;
        PlayerController newController = newObject.GetComponent<PlayerController>();
        players.Add(newObject);
        newController.SetGamepad((Gamepad)device);
    }

    private Vector3 GetMeanPlayerPosition()
    {
        if (players.Count == 0)
            return new Vector3(0, 0, 0);

        float
            xMean = 0,
            yMean = 0,
            zMean = 0;
        foreach (Object o in players)
        {
            GameObject player = ((GameObject)o);
            xMean += player.transform.position.x;
            yMean += player.transform.position.y;
            zMean += player.transform.position.z;
        }
        xMean /= players.Count;
        yMean /= players.Count;
        zMean /= players.Count;
        return new Vector3(xMean, yMean, zMean);
    }
}
