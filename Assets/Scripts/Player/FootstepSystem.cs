//Footstep system intended for cooperation with PlayerController.cs
//by Bartek

using UnityEngine;
using System.Collections;

public class FootstepSystem : MonoBehaviour
{
    [Header("Main settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume1 = 0.8f;
    [SerializeField] private float volume2 = 0.5f;
    [Space]
    [SerializeField] private float walkInterval = 0.2f;
    [SerializeField] private float sprintInterval = 0.125f;

    private PlayerController player;
    private bool isPlaying = false;
    private float interval;
    private IEnumerator footstepCor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource.volume = volume1;
    }

    private void Update()
    {
        CheckIsSprint();

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
