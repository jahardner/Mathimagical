using UnityEngine;
using System.Collections;

public class SineWave : MonoBehaviour
{
    public GameObject player;
    public GameObject plotPointObject;
    public int numberOfPoints = 100;
    public float frequency = 2 * Mathf.PI; // scale number from [0 to 99] to [0 to 2Pi]
    public float amplitude = 10.0f;
    public float multiple = 1.2f;
    public bool stuck = false;
    public float speed = .3f;
    public AudioClip pickup;

    private float timed = 0, pausedTime = 0;
    private float colTimed = 0;
    private float playerX, playerY;

    private float freqMax = 40, freqMin = -40, ampMax = 40, ampMin = -40;

    GameObject[] plotPoints;

    // Use this for initialization
    void Start()
    {
        playerX = player.transform.position.x;

        if (plotPointObject == null) //if user did not fill in a game object to use for the plot points
            plotPointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere); //create a sphere

        plotPoints = new GameObject[numberOfPoints]; //creat an array of 100 points.

        for (int i = 0; i < numberOfPoints; i++)
        {
            plotPoints[i] = (GameObject)GameObject.Instantiate(plotPointObject, new Vector3(i - (numberOfPoints / 2), 0, 0), Quaternion.identity); //this specifies what object to create, where to place it and how to orient it
        }
        plotPointObject.SetActive(false); //hide the original
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float functionXvalue = i * frequency / numberOfPoints; // scale number from [0 to 99] to [0 to 2Pi]
            plotPoints[i].transform.position = new Vector3(i - (numberOfPoints / 2), Mathf.Sin(functionXvalue) * amplitude, 0);
        }

        if (Input.GetKey("a"))
        {
            if (frequency > freqMin)
            {
                frequency-=.1f;
            }
        }
        else if (Input.GetKey("d"))
        {
            if (frequency < freqMax)
            {
                frequency+=.1f;
            }
        }

        if (Input.GetKey("w"))
        {
            if (amplitude < ampMax)
            {
                amplitude+=.3f;
            }
        }
        else if (Input.GetKey("s"))
        {
            if (amplitude > ampMin)
            {
                amplitude-=.3f;
            }
        }

        timed += Time.deltaTime;
        multiple = (frequency - 2 * Mathf.PI) * 0.5f;
        if (timed > .02f && !stuck)
        {
            timed = 0f;
            playerX += speed;
        }
        if (stuck)
        {
            pausedTime += Time.deltaTime;
            if (pausedTime > 1f)
            {
                pausedTime = 0f;
                GradeText.score-=2;
            }
        }
        playerY = -1 * Mathf.Sin(playerX * frequency / numberOfPoints + multiple) * amplitude + 3;
        player.GetComponent<Transform>().position = new Vector3(playerX, playerY, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "obstacles")
        {
            stuck = true;
            pausedTime = 0f;
        }
        else if (col.gameObject.tag == "black-pie"
          || col.gameObject.tag == "red-pie" || col.gameObject.tag == "blue-pie")
        {
            if (col.gameObject.tag == "black-pie")
            {
                GradeText.score += 2;
            }
            else if (col.gameObject.tag == "red-pie")
            {
                GradeText.score += 3;
                speed += .1f;
            }
            else if (col.gameObject.tag == "blue-pie")
            {
                GradeText.score += 1;
                speed -= .1f;
            }
            Destroy(col.gameObject);
            AudioSource.PlayClipAtPoint(pickup, col.transform.position, 1.0f);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "obstacles")
        {
            stuck = false;
        }
    }
}
