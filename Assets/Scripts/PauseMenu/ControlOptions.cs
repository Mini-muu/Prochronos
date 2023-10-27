using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlOptions: MonoBehaviour
{
    private bool checkingButton = false;
    [SerializeField] PlayerInput playerInput;
    //InputBindingComposite composite;
    InputActionMap inputActions;
    InputBinding currentBinding;
    InputAction actionToChange;
    string inputDevice;
    
    private void Start()
    {
        /*inputActions = playerInput.actions.actionMaps[0];
        currentBinding = inputAction.action.bindings[0];
        Debug.Log(currentBinding.effectivePath);*/
    }

    public void BackGame()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    private void Update()
    {
        /*if (checkingButton) {
            checkButton();
        }*/
    }

    private void setCheckingButton(bool check)
    {
        checkingButton = check;
    }

    private void checkButton()
    {
        string newButton;
        Debug.Log("Checking button...");
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                Debug.Log("KeyCode down: " + kcode);
                checkingButton = false;
                newButton = kcode.ToString().ToLower();
                InputBinding inputBinding = new InputBinding { path = currentBinding.path, overridePath = inputDevice+"/"+newButton };
                //Debug.Log("Input binding: "+inputBinding.path);
                actionToChange.ApplyBindingOverride(inputBinding);
                //Debug.Log("New: "+currentBinding.overridePath + ", " + currentBinding.path);
            }
        }
    }

    private void actionPath(int action, int binding)
    {
        try
        {
            setCheckingButton(true);
            actionToChange = inputActions.actions[action];
            currentBinding = actionToChange.bindings[binding];
            int indexToRemove = currentBinding.path.IndexOf("/");
            inputDevice = currentBinding.path.Remove(indexToRemove);
        } catch(Exception e) {
            Debug.Log(e + "\nError: probably the action doesn't exist. Check if the name in the method correspond to the name of the action in the Player Input Actions.");
        }
    }

    public void movementLeft() {
        //actionPath(0, 1);
        //setCheckingButton(true);
        /*actionToChange = inputActions.actions[0];
        currentBinding = inputActions.actions[0].bindings[1];
        Debug.Log(currentBinding.name+" "+currentBinding.path+" "+currentBinding.overridePath);*/
        playerInput.actions["Move"].ChangeCompositeBinding("Keyboard")
            .InsertPartBinding("Left", "&lt;Keyboard&gt;/leftArrow")
            .InsertPartBinding("Right", "&lt;Keyboard&gt;/rightArrow");
        Debug.Log(playerInput.actions["Move"].ChangeCompositeBinding("Keyboard").binding + " " + "Work in progress...");
    }
    public void movementRight() {
        //actionPath(0, 1);
        Debug.Log("Work in progress...");
    }
    public void roll() {
        Debug.Log(playerInput.actions["Roll"]);
    }
    public void jump() {
    }
    public void run() {
    }
    public void mainAttack()
    {
    }
    public void parry()
    {
    }
    public void superAttack()
    {
    }
}
