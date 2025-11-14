using UnityEngine;

public class DissolveShader : MonoBehaviour
{
    private MeshRenderer myRenderer;
    private Material myShader;
    public bool show; // show nightmare so if transitioning to nightmare this is true
    public bool active;
    [SerializeField] private float spawnSpeedMultiplier = 1.0f; // make this dependant on max cutoff var so different shapes dissolve at the same rate
    [SerializeField] private float myCutoff;
    [SerializeField] private float minCutoff; // same as max cutoff this needs to be assigned manually based on object shape, for cubes -0.6
    [SerializeField] private float maxCutoff; // this will have to be tested on each object and assigned manualy, im not sure what dictates what the max cutoff should be. for cubes its 0.85 and cylinders its 1.2

    // Start is called before the first frame update
    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        //myShader = Instantiate(myRenderer.sharedMaterial);
        //myRenderer.materials[0] = myShader;
        myShader = myRenderer.materials[1];
        myCutoff = minCutoff;
        myShader.SetFloat("_Cutoff_Height", myCutoff);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (active)
        {
            if (show)
            {
                //myCutoff += (0.015f * spawnSpeedMultiplier);
                if (myCutoff >= maxCutoff) { active = false; show = false; }
            }

            if (!show)
            {
                //myCutoff -= (0.015f * spawnSpeedMultiplier);
                if (myCutoff <= -0.6f) { active = false; show = true; }
            }

            myCutoff = Mathf.Clamp(myCutoff, -0.6f, maxCutoff);
            myShader.SetFloat("_Cutoff_Height", myCutoff);
        }
    }

    /*
    public void Spawn()
    {

        show = true;

    }

    public void Hide()
    {

        show = false;

    }

    public void Despawn()
    {

        show = false;

        destroy = true;

    }
    */
}
