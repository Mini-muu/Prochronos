using UnityEngine;

public class Fireplace : BaseInteraction
{
    public override void ExecuteInteraction()
    {
        base.ExecuteInteraction();

        SetCheckPoint();

        SaveData();
    }

    private void SetCheckPoint()
    {
        //SavePosition
    }

    private void SaveData()
    {
        Debug.LogWarning("Not Implemented");
    }
}
