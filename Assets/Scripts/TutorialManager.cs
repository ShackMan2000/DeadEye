using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;
    public GameObject prompt;
    public float timerDuration = 10.0f;

    private PlayableDirector timeline;

    private bool gun1Grabbed = false;
    private bool gun2Grabbed = false;

    private bool timerRunning = false;

    private void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        prompt.SetActive(false); // Initially, hide the prompt.

        // Subscribe to events when the timeline reaches certain points.
        timeline.stopped += TimelineStopped;
        timeline.played += TimelinePlayed;

        // Detect when guns are grabbed and update flags accordingly.
        gun1.GetComponent<GrabMe>().onGrabbed += () => gun1Grabbed = true;
        gun2.GetComponent<GrabMe>().onGrabbed += () => gun2Grabbed = true;
    }

    private void Update()
    {
        if (timerRunning)
        {
            timerDuration -= Time.deltaTime;

            if (timerDuration <= 0)
            {
                // Time's up, show the prompt.
                prompt.SetActive(true);
                timerRunning = false;
            }
        }
    }

    private void TimelineStopped(PlayableDirector director)
    {
        // The Timeline has stopped. Check if it's the point where you want to prompt the user.
        if (director.time == director.duration)
        {
            if (gun1Grabbed && gun2Grabbed)
            {
                // The user grabbed both guns, continue the tutorial.
                // Add code to move on in the tutorial.
            }
            else
            {
                // The user didn't grab both guns, start the timer.
                timerRunning = true;
            }
        }
    }

    private void TimelinePlayed(PlayableDirector director)
    {
        // The Timeline has started playing. Check if it's the point where you want to reset the timer and hide the prompt.
        if (director.time == 0)
        {
            timerDuration = 10.0f; // Reset the timer duration.
            prompt.SetActive(false); // Hide the prompt.
        }
    }
}
