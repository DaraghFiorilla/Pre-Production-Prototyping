using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("General SFX")]

    [field: SerializeField] public EventReference playerSteps { get; private set; }

    [field: SerializeField] public EventReference mopSFX { get; private set; }

    [field: SerializeField] public EventReference alarmSFX { get; private set; }

    [field: Header("Minigame SFX")]

    [field: SerializeField] public EventReference canDropped { get; private set; }

    [field: SerializeField] public EventReference meatChop { get; private set; }

    [field: SerializeField] public EventReference stockDrop { get; private set; }

    [field: Header("UI SFX")]

    [field: SerializeField] public EventReference menuButton { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference dayMuzak { get; private set; }

    [field: SerializeField] public EventReference nightMusic { get; private set; }

    [field: SerializeField] public EventReference titleMusic { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one FMOD Events instance in scene");
        }
        instance = this;
    }

}
