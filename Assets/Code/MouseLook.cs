using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] private int slowedSensitivity = 1000;
    [SerializeField] private int normalSensitivity = 100;

    private float currentSensitivity = 0;

    public SerialField serial;
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        slowedSensitivity = PlayerPrefs.GetInt("S_SENSETIVITY",slowedSensitivity);
        normalSensitivity = PlayerPrefs.GetInt("N_SENSETIVITY", normalSensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        currentSensitivity = Mathf.Lerp(slowedSensitivity, normalSensitivity, Time.timeScale);

        float mouseX = serial.TouchDist.x * currentSensitivity * Time.deltaTime;
        float mouseY = serial.TouchDist.y * currentSensitivity * Time.deltaTime;
            
        MoveCamera(mouseX, mouseY);

    }

    public void MoveCamera(float mouseX, float mouseY)
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
