//Footstep system intended for cooperation with PlayerController.cs
//by Bartek
//
//Footstep sound is depended by actual Sprite under player
//Raycast checks Sprite under player and set right properties which are in Ground class

using UnityEngine;
using System.Collections;
using System;

public class FootstepSystem : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] private AudioSource audioSource;                       //AudioSource which contains actual footstep sound
    [SerializeField] private float volume1;                                 //Actual volume 1
    [SerializeField] private float volume2;                                 //Actual volume 2
    [SerializeField] private float walkInterval = 0.2f;                     //Interval beetwen one step in walk
    [SerializeField] private float sprintInterval = 0.125f;                 //Interval beetwen one step in sprint
    [Header("Grounds")]
    [SerializeField] private LayerMask groundLayer;                         //Ground layer, all grounds need to have that layer
    [Space]
    [SerializeField] private AudioClip basicFootstep;                       //If raycast doesn't detect any ground, that sound will be playing
    [SerializeField] private float basicVol1;                               //Basic volume 1, If raycast doesn't detect any ground
    [SerializeField] private float basicVol2;                               //Basic volume 2, If raycast doesn't detect any ground
    [Space]
    [SerializeField] private Ground[] grounds;                              //Array with a lot of Ground classes

    private PlayerController player;                                        //Reference to player
    private bool isPlaying = false;                                         //Is footstep sound playing
    private float interval;                                                 //Interval beetwen actual one step
    private IEnumerator footstepCor;                                        //Variable with coroutine                                   

    [Serializable]
    public class Ground                                                     //Serializabled class with informations about Ground
    {
        public Sprite sprite;                                               
        public AudioClip clip;
        public float volume1;
        public float volume2;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource.volume = basicVol1;
    }

    private void Update()
    {
        CheckIsSprint();
        DetectTypeOfGround();

        if(player.isMoving)
        {
            if (!isPlaying)
            {
                isPlaying = true;

                if (footstepCor != null) StopCoroutine(footstepCor);
                footstepCor = null;
                footstepCor = Footstep();
                StartCoroutine(footstepCor);
            }
        }
        else
        {
            isPlaying = false;
        }
    }

    private void DetectTypeOfGround ()
    {
        RaycastHit2D ray2D = Physics2D.Raycast(transform.position, Vector2.down, 10, groundLayer);

        Debug.Log(ray2D ? ray2D.transform.gameObject.name : "n");

        if(!ray2D)                                                                                  //Did raycast detect any ground
        {
            volume1 = basicVol1;
            volume2 = basicVol2;
            audioSource.clip = basicFootstep;
        }
        else
        {
            foreach(Ground ground in grounds)                                                       
            {
                if(ray2D.transform.gameObject.GetComponent<SpriteRenderer>().sprite == ground.sprite) //Check which type of ground has been detected and ascribe right properties
                {
                    volume1 = ground.volume1;
                    volume2 = ground.volume2;
                    audioSource.clip = ground.clip;
                }
            }
        }
    }

    private void CheckIsSprint ()
    {
        if (player.isSprinting)
            interval = sprintInterval;
        else
            interval = walkInterval;
    }

    private IEnumerator Footstep ()
    {
        yield return new WaitForSeconds(0.4f);

        while (isPlaying)
        {
            if (audioSource.volume == volume1)
                audioSource.volume = volume2;
            else
                audioSource.volume = volume1;

            audioSource.Play();
            yield return new WaitForSeconds(interval);
        }
    }
}
