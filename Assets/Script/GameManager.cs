using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolygonMan
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        string stageName = "Title";
        SpriteRenderer spriteRenderer;
        GameObject complete;
        bool isLoaded = false;
        private string sceneName;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0, 0, 0, 1.0f);
            complete = GameObject.Find("Complete");
            FadeOut();
        }

        public void LoadScene()
        {
            if (!isLoaded)
            {
                isLoaded = true;
                sceneName = stageName;
                iTween.MoveTo(complete, transform.position, 1.0f);
                StartCoroutine(WaitForLoad());
            }
        }

        public void LoadScene(string _sceneName)
        {
            if (!isLoaded)
            {
                isLoaded = true;
                sceneName = _sceneName;
                FadeIn();
            }
        }

        void FadeIn()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 0.5f, "onupdate", "SetValue", "oncomplete", "LoadStage", "oncompletetarget", gameObject));
        }

        void FadeOut()
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 0.5f, "onupdate", "SetValue"));
        }

        void SetValue(float alpha)
        {
            spriteRenderer.color = new Color(0, 0, 0, alpha);
        }

        void LoadStage()
        {
            SceneManager.LoadScene(sceneName);
        }

        IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(2.0f);

            FadeIn();
        }
    }
}
