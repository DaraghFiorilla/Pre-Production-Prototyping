using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FlagUpdateFast : MonoBehaviour
{
    [SerializeField] private InputActionReference debugFlag;

    private void OnEnable()
    {
        debugFlag.action.performed += OnDebugFlag;
        debugFlag.action.Enable();
    }

    private void OnDisable()
    {
        debugFlag.action.performed -= OnDebugFlag;
        debugFlag.action.Disable();
    }

    private void OnDebugFlag(InputAction.CallbackContext context)
    {
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}