using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;


public class Controller : MonoBehaviour
{
    
    public float speed;
    public Text countText;
    public Text winText;
    public Text timerText;
    public GameObject completeLevel;
    public SerialPort sp;
    public static string line;
    public static string[] data;
    public static string x;
    public static string z;
    
    private Rigidbody rb;
    private int count;
    private static float timer;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        sp = new SerialPort("COM8", 115200);
        sp.Open();
        sp.ReadTimeout = 11;


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            line = sp.ReadLine();
            data = line.Split(' ');
            x = data[0];
            float num_x = float.Parse(x, System.Globalization.CultureInfo.InvariantCulture);
            z = data[1];
            float num_z = float.Parse(z, System.Globalization.CultureInfo.InvariantCulture);
            Vector3 movement = new Vector3(1 * num_x, 0, 1 * num_z);
            Debug.Log("<size=25>" + sp.ReadLine() + "</size>");
            rb.AddForce(movement * speed);
        }
        catch (System.Exception)
        {
            
        }
        Timer();
        /*float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);*/
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prize"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 11)
        {
            completeLevel.SetActive(true);
            winText.text = "You Win!";            
        }
    }
    void Timer()
    {
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("0");
    }
}

