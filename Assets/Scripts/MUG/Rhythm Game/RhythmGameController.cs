using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SonicBloom.Koreo.Demos
{
    public class RhythmGameController : MonoBehaviour
    {
        public Rigidbody player;
        public string eventID;
        public float hitWindowRangeInMS = 80;
        public float noteSpeed = 1f;
        public NoteObject noteObjectArchetype;
        public List<LaneController> noteLanes = new List<LaneController>();
        public float leadInTime;
        public AudioSource audioCom;

        float leadInTimeLeft;
        float timeLeftToPlay;
        Koreography playingKoreo;
        int hitWindowRangeInSamples;
        Stack<NoteObject> noteObjectPool = new Stack<NoteObject>();
        bool running = true;

		bool GameIsPaused = false;

        public int HitWindowSampleWidth
        {
            get{
                return hitWindowRangeInSamples;
            }
        }

        public float WindowSizeInUnits
        {
            get{
                return noteSpeed * (hitWindowRangeInMS * 0.001f);
            }
        }

        public int SampleRate
        {
            get{
                return playingKoreo.SampleRate;
            }
        }

        // The current sample time, including any necessary delays.
        public int DelayedSampleTime
        {
            get{
                return playingKoreo.GetLatestSampleTime() - (int)(audioCom.pitch * leadInTimeLeft * SampleRate);
            }
        }

        void Start()
        {
            InitializeLeadIn();
            
            // Initialize all the Lanes.
            for(int i = 0; i < noteLanes.Count; ++i)
            {
                noteLanes[i].Initialize(this);
            }

            // Initialize events.
            playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);

            // Grab all the events out of the Koreography.
            KoreographyTrack rhythmTrack = playingKoreo.GetTrackByID(eventID);
            List<KoreographyEvent> rawEvents = rhythmTrack.GetAllEvents();
            for (int i = 0; i < rawEvents.Count; ++i)
			{
				KoreographyEvent evt = rawEvents[i];
				string payload = evt.GetTextValue();
				
				// Find the right lane.
				for (int j = 0; j < noteLanes.Count; ++j)
				{
					LaneController lane = noteLanes[j];
					if (lane.DoesMatchPayload(payload))
					{
						lane.AddEventToLane(evt);
						break;
					}
				}
			}
        }

        // Sets up the lead-in-time.  Begins audio playback immediately if the specified lead-in-time is zero.
		void InitializeLeadIn()
		{
			// Initialize the lead-in-time only if one is specified.
			if (leadInTime > 0f)
			{
				// Set us up to delay the beginning of playback.
				leadInTimeLeft = leadInTime;
				timeLeftToPlay = leadInTime - Koreographer.Instance.EventDelayInSeconds;
			}
			else
			{
				// Play immediately and handle offsetting into the song.  Negative zero is the same as
				//  zero so this is not an issue.
				audioCom.time = -leadInTime;
				audioCom.Play();
			}
		}

        private void FixedUpdate() {
            if(!running) return;

            Vector3 forwardMove = new Vector3(1,0,0) * noteSpeed * Time.fixedDeltaTime;
            player.MovePosition(player.position + forwardMove);
        }
        
        void Update()
		{
			UpdateInternalValues();

			// Count down some of our lead-in-time.
			if (leadInTimeLeft > 0f && GameIsPaused == false)
			{
				leadInTimeLeft = Mathf.Max(leadInTimeLeft - Time.unscaledDeltaTime, 0f);
			}

			// Count down the time left to play, if necessary.
			if (timeLeftToPlay > 0f && GameIsPaused == false)
			{
				timeLeftToPlay -= Time.unscaledDeltaTime;

				// Check if it is time to begin playback.
				if (timeLeftToPlay <= 0f)
				{
					audioCom.time = -timeLeftToPlay;
					audioCom.Play();

					timeLeftToPlay = 0f;
				}
			}
		}
        void UpdateInternalValues()
		{
			hitWindowRangeInSamples = (int)(0.001f * hitWindowRangeInMS * SampleRate);
		}

        // Retrieves a frehsly activated Note Object from the pool.
		public NoteObject GetFreshNoteObject()
		{
			NoteObject retObj;

			if (noteObjectPool.Count > 0)
			{
				retObj = noteObjectPool.Pop();
			}
			else
			{
				retObj = GameObject.Instantiate<NoteObject>(noteObjectArchetype);
			}
			
			retObj.gameObject.SetActive(true);
			retObj.enabled = true;

			return retObj;
		}
        // Deactivates and returns a Note Object to the pool.
		public void ReturnNoteObjectToPool(NoteObject obj)
		{
			if (obj != null)
			{
				obj.enabled = false;
				obj.gameObject.SetActive(false);

				noteObjectPool.Push(obj);
			}
		}

        public void EndGame()
        {
            running = false;
            Invoke("Restart", 3);
        }

        // Restarts the game.
		public void Restart()
		{
			// Reset the audio.
			audioCom.Stop();
			audioCom.time = 0f;

			// Flush the queue of delayed event updates. 
			Koreographer.Instance.FlushDelayQueue(playingKoreo);

			// Reset the Koreography time. 
			playingKoreo.ResetTimings();

			// Reset all the lanes so that tracking starts over.
			for (int i = 0; i < noteLanes.Count; ++i)
			{
				noteLanes[i].Restart();
			}

			// Reinitialize the lead-in-timing.
			InitializeLeadIn();
			Time.timeScale = 1f;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		// Pause the game
		public void Pause()
		{
			GameIsPaused = true;
			audioCom.Pause();
		}
		// Resume the game
		public void Resume()
		{
			GameIsPaused = false;
			if (timeLeftToPlay <= 0f)
			{
				audioCom.Play();
			}
		}
    }

}
