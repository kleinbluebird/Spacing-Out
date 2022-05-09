using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class NoteObject : MonoBehaviour
    {
        public MeshRenderer visual;
        KoreographyEvent trackedEvent;
        LaneController laneController;
        RhythmGameController gameController;

        static Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            return new Vector3 (from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
        }

        //Prepare the Note Object for use.
        public void Initialize(KoreographyEvent evt, Color color, LaneController laneCont, RhythmGameController gameCont, float expand)
        {
            trackedEvent = evt;
            visual.material.color = color;
            laneController = laneCont;
            gameController = gameCont;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) + new Vector3(1f, 1f, 1f) * expand;

            GetPosition();
        }
        // Resets the Note Object to its default state.
        private void Reset() {
            trackedEvent = null;
			laneController = null;
			gameController = null;
        }

        void Update()
        {
            // pay attention
            if(transform.position.x <= laneController.DespawnX)
            {
                gameController.ReturnNoteObjectToPool(this);
                Reset();
            }
        }

        // Get the position of the Note Object along the Lane.
        void GetPosition()
        {
            float samplesPerUnit = gameController.SampleRate / gameController.noteSpeed;
            // pay attention
            Vector3 pos = laneController.TargetPosition;
            pos.x -= (gameController.DelayedSampleTime - trackedEvent.StartSample) / samplesPerUnit;
            transform.position = pos;
        }

        // Check to see if the Note Object is currently hittable or not.
        public bool IsNoteHittable()
        {
            int noteTime = trackedEvent.StartSample;
            int curTime = gameController.DelayedSampleTime;
            int hitWindow = gameController.HitWindowSampleWidth + 880*6;

            return (Mathf.Abs(noteTime - curTime) <= hitWindow);
        }

        // Check to see if the note is missed.
        public bool IsNoteMissed()
        {
            bool bMissed = true;
            if(enabled)
            {
                int noteTime = trackedEvent.StartSample;
                int curTime = gameController.DelayedSampleTime;
                int hitWindow = gameController.HitWindowSampleWidth + 880*6;

                bMissed = (curTime - noteTime > hitWindow);
            }
            return bMissed;
        }

        void ReturnToPool()
        {
            gameController.ReturnNoteObjectToPool(this);
            Reset();
        }

        // Perfrom actions when the Note Object is hit.
        public void OnHit()
        {
            // Add to the player's score
            GameManager.inst.IncrementScore();
            ReturnToPool();
        }

        // Perform actions when the Note Object is cleared.
        public void OnClear()
        {
            ReturnToPool();
        }

    }
}

