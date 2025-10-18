using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public bool topOfList;
    public bool inTrigger;
    public int arrowType;
    public RhythmMinigame minigameManager;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        //Debug.Log(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RhythmInput") { inTrigger = true; Debug.Log(gameObject.name + " Entered trigger"); }
        else if (collision.gameObject.tag == "DeleteArrow") { minigameManager.DeleteArrow(gameObject, arrowType); Debug.Log("Deleting " + gameObject.name); }
    }
}
