using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SonicBloom.Koreo.Demos
{
    public class GameManager : MonoBehaviour
    {
        private int score = 0;
        public static GameManager inst;
        public Text scoreText;
        public TMP_Text textMeshPro;
        public Animation combo;
        public List<LaneController> noteLanes = new List<LaneController>();
        private int hitCount;

        public void IncrementScore()
        {
            score++;
            scoreText.text = "SCORE   " + score;
        }
        private void Awake() {
            inst = this;    
            HideHitCounter();
        }

        private void Start() {
            for(int i = 0; i < noteLanes.Count; ++i)
            {
                noteLanes[i].OnNoteHit += Hit_OnNoteHit;
                noteLanes[i].OnNoteMiss += Hit_OnNoteMiss;
            }
        }

        private void Hit_OnNoteHit(object sender, System.EventArgs e) {
            hitCount++;
            SetHitCounter(hitCount);
        }
        private void Hit_OnNoteMiss(object sender, System.EventArgs e){
            FadeHitCounter();
            hitCount = 0;
        }
        private void SetHitCounter(int hitCount) {
            if(hitCount >= 5)
            {
                textMeshPro.text = hitCount.ToString()+" Combo";
                if(hitCount == 5)
                {
                    combo.Play("Combo");
                }
            }
            else
            {
                textMeshPro.text = " ";
            }
        }

        private void HideHitCounter() {
            textMeshPro.text = " ";
        }

        private void FadeHitCounter() {
            if(hitCount >= 5)
            {
                textMeshPro.text = hitCount.ToString()+" Combo";
                combo.Play("FadeCombo");
            }
        }
    }
}

