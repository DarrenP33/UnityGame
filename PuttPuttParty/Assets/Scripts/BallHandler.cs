using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallHandler : MonoBehaviour
{
    public AudioClip puttMake, puttHit;
    public float maxPower;
    public float changeAngleSpeed;
    public float aimLength;
    public float minHoleTime;
    public Slider powerBar;
    public TextMeshProUGUI puttCountLabel;
    public Transform startTransform;
    public LevelManager levelManager;

    private LineRenderer aim;
    private Rigidbody ball;
    private float angle;
    private float powerUpTime;
    private float power;
    private int numPutts;
    private float holeTime;
    private Vector3 lastPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ball = GetComponent<Rigidbody>();
        ball.maxAngularVelocity = 1000;
        aim = GetComponent<LineRenderer>();
        startTransform.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (ball.velocity.magnitude < 0.01f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                ChangeAngle(-1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                ChangeAngle(1);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PuttPutt();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                PowerUp();
            }
            UpdateLinePositions();
        }
        else
        {
            aim.enabled = false;
        }
    }

    private void ChangeAngle(int direction)
    {
        angle += changeAngleSpeed * Time.deltaTime * direction;
    }

    private void UpdateLinePositions()
    {
        if (holeTime == 0) 
        { 
            aim.enabled = true; 
        }

        aim.SetPosition(0, transform.position);
        aim.SetPosition(1, transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * aimLength);
    }

    private void PuttPutt()
    {
        audioSource.PlayOneShot(puttHit);
        lastPosition = transform.position;
        ball.AddForce(Quaternion.Euler(0, angle, 0) * Vector3.forward * maxPower * power, ForceMode.Impulse);
        power = 0;
        powerBar.value = 0;
        powerUpTime = 0;
        numPutts++;
        puttCountLabel.text = numPutts.ToString();
    }

    private void PowerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime, 1);
        powerBar.value = power;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hole")
        {
            CountHoleTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole")
        {
            audioSource.PlayOneShot(puttMake);
        }
    }


    private void CountHoleTime()
    {
        holeTime += Time.deltaTime;

        if (holeTime >= minHoleTime)
        {
            levelManager.nextPlayer(numPutts);
            holeTime = 0;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            MissHole();
        }
    }

    private void MissHole()
    {
        holeTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "OOB")
        {
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
    }

    public void SetBall(Color color)
    {
        transform.position = startTransform.position;
        angle = startTransform.rotation.eulerAngles.y;
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        aim.material.SetColor("_Color", color);
        aim.enabled = true;
        numPutts = 0;
        puttCountLabel.text = "0";
    }

}
