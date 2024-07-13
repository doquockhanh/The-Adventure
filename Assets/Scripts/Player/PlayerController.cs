using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private LayerMask groundPlayer;
    public Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public float jump;
    private bool facingRight = true;
    private Canvas baseCanvas;
    private GameObject blueTextPrefap;
    private GameObject redTextPrefap;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<Stats>().OnGetExp += OnGetExp;
        GetComponent<Stats>().OnTakeDamage += OnTakeDamage;
        GetComponent<Stats>().OnDeath += OnDie;
        baseCanvas = transform.Find("BaseCanvas").GetComponent<Canvas>();
        blueTextPrefap = Resources.Load<GameObject>("textBlue");
        redTextPrefap = Resources.Load<GameObject>("textRed");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    void Move(float horizontalInput)
    {

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.collider.CompareTag("Finish"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("level Up");
    }

    public void OnDie(Stats stats)
    {
        Debug.Log("die");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnGetExp(float exp)
    {
        Transform textSpawnPoint = baseCanvas.transform.Find("textSpawnPoint");
        Vector2 pos = textSpawnPoint == null ? baseCanvas.transform.position : textSpawnPoint.transform.position;
        pos.x = Random.Range(pos.x - 1f, pos.x + 1f);
        GameObject expText = Instantiate(blueTextPrefap, pos, Quaternion.identity);
        expText.transform.SetParent(baseCanvas.transform);
        TextController textController = expText.GetComponent<TextController>();
        textController.sizeScale = 2f;
        textController.text = $"+{exp} Exp";
    }

    public void OnTakeDamage(float damage) {
        Transform textSpawnPoint = baseCanvas.transform.Find("textSpawnPoint");
        Vector2 pos = textSpawnPoint == null ? baseCanvas.transform.position : textSpawnPoint.transform.position;
        pos.x = Random.Range(pos.x - 1f, pos.x + 1f);
        GameObject damageText = Instantiate(redTextPrefap, pos, Quaternion.identity);
        damageText.transform.SetParent(baseCanvas.transform);
        TextController textController = damageText.GetComponent<TextController>();
        textController.sizeScale = 2f;
        textController.text = $"-{damage} Hp";
    }
}
