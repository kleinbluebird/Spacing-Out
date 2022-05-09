using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SonicBloom.Koreo.Demos
{
    public class LaneController : MonoBehaviour
    {
        public event EventHandler OnNoteHit;
        public event EventHandler OnNoteMiss;

        public Color color = Color.blue;
        public MeshRenderer visual;
        public KeyCode keyboardButton;
        public List<string> matchedPayloads = new List<string>();
        public float expand = 0f;
        [SerializeField] GameObject parsHit;
        
        List<KoreographyEvent> laneEvents = new List<KoreographyEvent>();
        Queue<NoteObject> trackedNotes = new Queue<NoteObject>();
        RhythmGameController gameController;
        float spawnX = 0f;
        float despawnX = 0f;
        int pendingEventIdx = 0;

        // Feedback Scales used for resizing the buttons on press.
        Vector3 defaultScale;
        float scaleNormal = 1f;
        float scalePress = 1.4f;
        float scaleHold = 1.2f;

        public Vector3 SpawnPosition
        {
            get{
                return new Vector3(transform.position.x + spawnX, transform.position.y, transform.position.z);
            }
        }
        public Vector3 TargetPosition
        {
            get{
                return new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
        public float DespawnX
        {
            get{
                return despawnX;
            }
        }

        public void Initialize(RhythmGameController controller)
        {
            gameController = controller;
        }

        public void Restart()
        {
            pendingEventIdx = 0;
            int numToClear = trackedNotes.Count;
			for (int i = 0; i < numToClear; ++i)
			{
				trackedNotes.Dequeue().OnClear();
			}
        }

        void Start()
        {
            // visiblility control
            gameObject.GetComponent<Renderer>().enabled = true;

            // Get the position to allow for spawning/removal.
            spawnX = 10f;
            despawnX = transform.position.x - 0.5f;

            // Update our visual color.
            visual.material.color = color;

            // Capture the default scale set in the Inspector
            defaultScale = gameObject.transform.localScale;
        }

        void Update()
        {
            despawnX = transform.position.x - 0.5f;
            // Clear out invalid entries.
            while (trackedNotes.Count > 0 && trackedNotes.Peek().IsNoteMissed())
			{
                OnNoteMiss?.Invoke(this, EventArgs.Empty);
				trackedNotes.Dequeue();
			}

            // Check for new spawns.
            CheckSpawnNext();

            // Check for input.
            if (Input.GetKeyDown(keyboardButton))
			{
				CheckNoteHit();
				SetScalePress();
			}
			else if (Input.GetKey(keyboardButton))
			{
				SetScaleHold();
			}
			else if (Input.GetKeyUp(keyboardButton))
			{
				SetScaleDefault();
			}


        }

        int GetSpawnSampleOffset()
		{
			// Given the current speed, what is the sample offset of our current.
			float spawnDistToTarget = spawnX;
			
			// At the current speed, what is the time to the location?
			double spawnSecsToTarget = (double)spawnDistToTarget / (double)gameController.noteSpeed;
			
			// Figure out the samples to the target.
			return (int)(spawnSecsToTarget * gameController.SampleRate);
		}

        // Check if a Note Object is hit.
        public void CheckNoteHit()
		{
			if (trackedNotes.Count > 0 && trackedNotes.Peek().IsNoteHittable())
			{
				NoteObject hitNote = trackedNotes.Dequeue();
                // Particle Play
                PoolManager.Release(parsHit, transform.position, Quaternion.AngleAxis(90, Vector3.forward));
				OnNoteHit?.Invoke(this, EventArgs.Empty);
                hitNote.OnHit();
			}
            else{
                OnNoteMiss?.Invoke(this, EventArgs.Empty);
            }
		}

        public void AddEventToLane(KoreographyEvent evt)
		{
			laneEvents.Add(evt);
		}


        // Check if the next Note Object should be spawned.
        void CheckSpawnNext()
		{
			int samplesToTarget = GetSpawnSampleOffset();
			
			int currentTime = gameController.DelayedSampleTime;
            //print(currentTime);
			
			// Spawn for all events within range.
			while (pendingEventIdx < laneEvents.Count &&
			       laneEvents[pendingEventIdx].StartSample < currentTime + samplesToTarget)
			{
				KoreographyEvent evt = laneEvents[pendingEventIdx];
				
				NoteObject newObj = gameController.GetFreshNoteObject();
				newObj.Initialize(evt, color, this, gameController, expand);

				trackedNotes.Enqueue(newObj);
				
				pendingEventIdx++;
			}
		}

        // Check to see if the string value passed in matches matchedPayloads List.
        public bool DoesMatchPayload(string payload)
		{
			bool bMatched = false;

			for (int i = 0; i < matchedPayloads.Count; ++i)
			{
				if (payload == matchedPayloads[i])
				{
					bMatched = true;
					break;
				}
			}

			return bMatched;
		}

        void AdjustScale(float multiplier)
        {
            gameObject.transform.localScale = defaultScale * multiplier;
        }
        public void SetScaleDefault()
		{
			AdjustScale(scaleNormal);
		}
        public void SetScalePress()
		{
			AdjustScale(scalePress);
		}
        public void SetScaleHold()
		{
			AdjustScale(scaleHold);
		}

    }
}

