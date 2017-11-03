using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolygonMan
{
    public class TitleManager : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        SpriteRenderer loadSprite;
        GameObject stages;
        bool isSelected = false;
        bool isLoaded = false;
        string sceneName;
        float depth = 10.0f;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            stages = GameObject.Find("Stages");
            loadSprite = GameObject.Find("Load").GetComponent<SpriteRenderer>();
            loadSprite.color = new Color(0, 0, 0, 1.0f);
            FadeOut();
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
            loadSprite.color = new Color(0, 0, 0, alpha);
        }

        void LoadStage()
        {
            SceneManager.LoadScene(sceneName);
        }

        RaycastHit2D IsSelected()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, depth, 1 << LayerMask.NameToLayer("Title"));
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = IsSelected();

                if (hit && !isSelected)
                {
                    isSelected = true;
                    spriteRenderer.enabled = true;
                    iTween.MoveTo(stages, transform.position, 1.0f);
                }
            }
        }
    }
}