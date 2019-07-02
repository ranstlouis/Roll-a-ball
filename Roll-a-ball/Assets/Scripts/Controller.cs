using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    
    public float speed;
    public Text countText;
    public Text winText;
    public GameObject completeLevel;
    

    private Rigidbody rb;
    private int count;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
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
}
