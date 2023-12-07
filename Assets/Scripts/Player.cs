using UnityEngine;

public class Player : MonoBehaviour
{

    public TMPro.TextMeshPro scoreText;
    private int score = 0;

    public TMPro.TextMeshPro livesText;
    public int hp = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyLaser"))
        {
            hp--;
        }
    }

    // Start is called before the first frame update
    public void Score()
    {
        score += 50;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Life: " + hp.ToString();

    }
}
