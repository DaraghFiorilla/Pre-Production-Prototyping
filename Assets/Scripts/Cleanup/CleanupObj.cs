using UnityEngine;

public class CleanupObj : MonoBehaviour
{
    public float cleanSpeed;
    public float objHealth = 100;
    private Material mat;

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
            // tell another script this object is done 
            Destroy(gameObject);
        }
    }
}
