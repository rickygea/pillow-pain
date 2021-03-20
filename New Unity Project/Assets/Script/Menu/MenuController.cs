using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool _isStoryPanelShowed = false;

    [SerializeField] private CanvasGroup _storyPanel = null;
    [SerializeField] private float _delayShowStory = 1f;
    [SerializeField] private float _holdShowStory = 2f;
    [SerializeField] private float _fadeInOutDuration = 0.5f;

    [Space]
    [SerializeField] private CanvasGroup _lobbyPanel = null;

    [Space]
    [SerializeField] private GameObject _tutorialPanel = null;

    private void Start()
    {
        if(!_isStoryPanelShowed)
        {
            SetCanvasGroupAlphaAndBool(_storyPanel, 0, true);
            SetCanvasGroupAlphaAndBool(_lobbyPanel, 0, false);

            LeanTween.delayedCall(_delayShowStory, () => {
                LeanTween.alphaCanvas(_storyPanel, 1, _fadeInOutDuration).setOnComplete(() => {
                    LeanTween.delayedCall(_holdShowStory, () => {
                        LeanTween.alphaCanvas(_storyPanel, 0, _fadeInOutDuration).setOnComplete(() => {

                            SetCanvasGroupAlphaAndBool(_storyPanel, 0, false);
                            LeanTween.alphaCanvas(_lobbyPanel, 1, _fadeInOutDuration).setOnComplete(() => {
                                SetCanvasGroupAlphaAndBool(_lobbyPanel, 1, true);
                            });
                        });
                    });
                });
            });

            _isStoryPanelShowed = true;
        } else
        {
            SetCanvasGroupAlphaAndBool(_storyPanel, 0, false);
            SetCanvasGroupAlphaAndBool(_lobbyPanel, 1, true);
        }
    }

    private void SetCanvasGroupAlphaAndBool(CanvasGroup canvasG, float alpha, bool boolean)
    {
        canvasG.alpha = alpha;
        canvasG.interactable = boolean;
        canvasG.blocksRaycasts = boolean;
    }

    #region Buttons
    public void GotoGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void TutorialButton()
    {
        _tutorialPanel.SetActive(true);
        _lobbyPanel.gameObject.SetActive(false);
    }
    #endregion
}
