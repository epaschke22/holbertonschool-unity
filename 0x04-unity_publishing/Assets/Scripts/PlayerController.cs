using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController character;
    Vector3 moveVector;
    public float speed = 10f;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseUI;
    int score = 0;
    int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        winLoseUI.gameObject.SetActive(false);
        score = 0;
        health = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        character.Move(moveVector * speed * Time.fixedDeltaTime);
    }

    public void OnMovementChanged(InputAction.CallbackContext context)
	{
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
	}

    public void PauseMenu()
	{
        SceneManager.LoadScene("menu");
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Pickup"))
		{
            other.gameObject.SetActive(false);
            SetScoreText();
		}

        if (other.gameObject.CompareTag("Trap"))
		{
            SetHealthText();
            if (health == 0)
			{
                winLoseUI.gameObject.SetActive(true);
                winLoseUI.color = Color.red;
                winLoseText.color = Color.white;
                winLoseText.text = "Game Over!";
                StartCoroutine(LoadScene(3));
            }
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            winLoseUI.gameObject.SetActive(true);
            winLoseUI.color = Color.green;
            winLoseText.color = Color.black;
            winLoseText.text = "You win!";
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
	{
        score++;
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
	{
        health--;
        healthText.text = "Health: " + health;
    }

    IEnumerator LoadScene(float seconds)
	{
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
