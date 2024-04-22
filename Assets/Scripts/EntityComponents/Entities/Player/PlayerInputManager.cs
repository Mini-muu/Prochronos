using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    //Santa sia questa donna
    //https://www.youtube.com/watch?v=OT537RfNzCU


    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private PlayerInput playerInput;

    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction jump;
    [HideInInspector] public InputAction normalAttack;
    [HideInInspector] public InputAction strongAttack;
    [HideInInspector] public InputAction roll;
    [HideInInspector] public InputAction run;
    [HideInInspector] public InputAction interact;
    [HideInInspector] public InputAction parry;

    public static PlayerInputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        inputs.FindActionMap("Player").Enable();
        SetInputs();
    }

    private void OnDisable()
    {
        inputs.FindActionMap("Player").Disable();
    }

    private void SetInputs()
    {
        move = inputs.FindAction("Move");
        jump = inputs.FindAction("Jump");
        normalAttack = inputs.FindAction("NormalAttack");
        strongAttack = inputs.FindAction("StrongAttack");
        roll = inputs.FindAction("Roll");
        run = inputs.FindAction("Run");
        interact = inputs.FindAction("Interact");
        parry = inputs.FindAction("Parry");
    }

    public PlayerInput GetInput()
    {
        return playerInput;
    }

}
