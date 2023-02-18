using UnityEngine;

public class AgEnemyAttack : MonoBehaviour
{
    public int damage = 1;

    public TreeManager treeManager;

    private AudioSource _audioSource;
    public AudioClip crunchSound;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        treeManager = GameObject.FindGameObjectWithTag("tree").GetComponent<TreeManager>();
    }

    // Called via animation event in the TermiteAttackAnimation
    public void Attack()
    {
        treeManager.DecreaseHealth(damage);
        //Debug.Log("PLAY CRUNCH");
        _audioSource.clip = crunchSound;
        _audioSource.pitch = Random.Range(.5f, 1f);
        _audioSource.Play();
    }
}