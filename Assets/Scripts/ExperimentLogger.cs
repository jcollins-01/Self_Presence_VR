using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExperimentLogger : MonoBehaviour
{
    private string filePath;
    private bool startWriting;
    private int counter = 0;

    public InputActionProperty moveAction;
	public InputActionProperty jumpAction;
	public InputActionProperty sprintAction;
    public InputActionProperty moveTwoAction;
	public InputActionProperty jumpTwoAction;
	public InputActionProperty sprintTwoAction;
    public InputActionProperty grabLeftAction;
    public InputActionProperty grabRightAction;
    public InputActionProperty snapTurnAction;

    // Start is called before the first frame update
    void Start()
    {
        startWriting = false;
        filePath = GetFilePath();
        Debug.Log(filePath);
        
        moveAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Move body one", ctx.ReadValue<Vector2>().ToString());
        // jumpAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Jump body one");
        sprintAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Sprint body one");
        moveTwoAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Move body two", ctx.ReadValue<Vector2>().ToString());
        // jumpTwoAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Jump body two");
        sprintTwoAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Sprint body two");
        grabLeftAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Grab left");
        grabRightAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Grab right");
        snapTurnAction.action.performed += (ctx) => RecordKey(ctx.control.name, "Snap turn", ctx.ReadValue<Vector2>().ToString());
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    string GetFilePath()
    {
        string path = Application.persistentDataPath;
        string fileName = System.DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + "_Log.csv";
        string fullPath = System.IO.Path.Combine(path, fileName);
        return fullPath;
    }

    public void RecordKey(string key, string keyPurpose)
    {
        System.DateTime timeReal = System.DateTime.Now;
        float timeApp = Time.time;
        RecordKey(timeReal, timeApp, key, keyPurpose, "Pressed");
    }

    public void RecordKey(string key, string keyPurpose, string keyValue)
    {
        System.DateTime timeReal = System.DateTime.Now;
        float timeApp = Time.time;
        RecordKey(timeReal, timeApp, key, keyPurpose, keyValue);
    }
    
    void RecordKey(System.DateTime timeReal, float timeApp, string key, string keyPurpose, string keyValue)
    {
        string line = counter + "," + timeReal.ToString() + "," + timeApp.ToString() + "," + key + "," + keyPurpose + "," + keyValue + System.Environment.NewLine;
        if(!startWriting)
        {
            // Record header line
            string header = "Index,Real-time,App-time,Key,Key-Purpose,Key-Value" + System.Environment.NewLine;
            System.IO.File.WriteAllText(filePath, header);
            startWriting = true;
        }
        System.IO.File.AppendAllText(filePath, line);

        counter++;
    }

    // void OnMove(InputAction.CallbackContext context) {
    //     Debug.Log("Move");
    // }
    // public void OnMoveTwo(InputAction.CallbackContext context) {
    //     Debug.Log("MoveTwo");
    // }
    // public void OnLook(InputAction.CallbackContext context) {
    //     Debug.Log("Look");
    // }
    // public void OnJump(InputAction.CallbackContext context) {
    //     Debug.Log("Jump");
    // }
    // public void OnJumpTwo(InputAction.CallbackContext context) {
    //     Debug.Log("JumpTwo");
    // }
    // public void OnSprint(InputAction.CallbackContext context) {
    //     Debug.Log("Sprint");
    // }
    // public void OnSprintTwo(InputAction.CallbackContext context) {
    //     Debug.Log("SprintTwo");
    // }
}
