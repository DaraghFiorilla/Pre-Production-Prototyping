using UnityEngine;
using FMODUnity;

public class CleanupObj : MonoBehaviour
{
    public float cleanSpeed;
    public float objHealth = 100;
    private Material mat;
    public int flagID;
    [SerializeField] private EventProgress eventManager;

    private bool isPlaying;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void Clean()
    {
        objHealth -= cleanSpeed * Time.deltaTime;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, objHealth / 100);
        if (objHealth <= 0)
        {
            eventManager.UpdateFlag(flagID);
            Destroy(gameObject);
        }

    }
}
