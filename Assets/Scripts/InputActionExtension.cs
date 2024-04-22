using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;


public static class InputActionExtension
{
    public static string GetBindingString(this InputAction action)
    {
        return action.GetBindingDisplayString(InputBinding.MaskByGroup(PlayerInputManager.instance.GetInput().currentControlScheme));
    }
}

