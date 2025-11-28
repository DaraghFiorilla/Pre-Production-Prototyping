using UnityEngine;

public class CamTransitionScript : MonoBehaviour
{

    public GameObject mainCam;
    public GameObject gameCam;
    public GameObject transCam;

    bool minigameTrans;
    bool playerTrans;

    public bool gameTest;
    public bool playerTest;

    float lerpVar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mainCam.SetActive(true);
        transCam.SetActive(false);
        gameCam.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (minigameTrans && !playerTrans)
        {

            transCam.transform.position = Vector3.Lerp(transCam.transform.position, gameCam.transform.position, lerpVar);
            transCam.transform.rotation = Quaternion.Slerp(transCam.transform.rotation, gameCam.transform.rotation, lerpVar);

            lerpVar += 0.002f;

            if (lerpVar >= 0.15)
            {

                GameArrive();

            }

        }

        if (playerTrans && !minigameTrans)
        {

            transCam.transform.position = Vector3.Lerp(transCam.transform.position, mainCam.transform.position, lerpVar);
            transCam.transform.rotation = Quaternion.Slerp(transCam.transform.rotation, mainCam.transform.rotation, lerpVar);

            lerpVar += 0.002f;

            if (lerpVar >= 0.15)
            {

                PlayerArrive();

            }

        }

        if (gameTest)
        {

            MoveCamToGame();

        }

        if (playerTest)
        {

            ReturnCamToPlayer();

        }

    }

    public void MoveCamToGame()
    {

        if (playerTrans)
        {

            playerTrans = false;

        }

        transCam.transform.position = mainCam.transform.position;

        transCam.SetActive(true);
        mainCam.SetActive(false);
        gameCam.SetActive(false);

        minigameTrans = true;

        lerpVar = 0.0f;

        gameTest = false;

    }

    public void ReturnCamToPlayer()
    {

        if (minigameTrans)
        {

            minigameTrans = false;

        }

        transCam.transform.position = gameCam.transform.position;

        transCam.SetActive(true);
        gameCam.SetActive(false);
        mainCam.SetActive(false);

        playerTrans = true;

        lerpVar = 0.0f;

        playerTest = false;

    }

    void GameArrive()
    {

        gameCam.SetActive(true);
        transCam.SetActive(false);

        minigameTrans = false;

    }

    void PlayerArrive()
    {

        mainCam.SetActive(true);
        transCam.SetActive(false);

        playerTrans = false;

    }

}
