import UnityEngine as ue

objects = ue.GameObject.FindObjectsOfType(ue.GameObject)
for obj in objects:
    ue.Debug.Log(obj.name)
