using DG.Tweening;
using OMG.Extensions;
using OMG.UI;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatUI : UIPanel
{
    [SerializeField] private ChatText textPrefab;
    [SerializeField] private Transform textContainer;
    [SerializeField] private int maxTextCount;

    [Space]
    [SerializeField] private TMP_InputField inputField;

    [Space]
    [SerializeField] private GameObject textSection;
    [SerializeField] private GameObject inputSection;

    [Space]
    [SerializeField] private float fadeDelay;
    [SerializeField] private float fadeTime;

    private CanvasGroup canvasGroup;

    private Sequence fadeSeq;

    private char lastChar;

    public string Message
    {
        get
        {
            if (inputField.text == "")
                return "";

            //string message = inputField.text;
            //if (lastChar.IsKorean())
            //    message += lastChar;

            return inputField.text;
        }
    }

    public override void Init()
    {
        base.Init(); 

        canvasGroup = GetComponent<CanvasGroup>();

        fadeSeq = DOTween.Sequence();
        fadeSeq.AppendInterval(fadeDelay);
        fadeSeq.Append(canvasGroup.DOFade(0f, fadeTime));
        fadeSeq.SetAutoKill(false);

        inputField.onValueChanged.AddListener((str) =>
        {
            fadeSeq.Pause();
            canvasGroup.DOFade(1f, 0f);
        });
    }

    public override void Show()
    {
        base.Show();

        textSection.SetActive(true);
        inputSection.SetActive(true);

        inputField.ActivateInputField();
        inputField.text = "";

        fadeSeq.Pause();
        canvasGroup.DOFade(1f, 0f);   
    }

    public override void OnlyShow()
    {
        base.OnlyShow();

        inputSection.SetActive(false);
        Fade();
    }

    public void CreateChat(string senderName, string message)
    {
        ChatText text = Instantiate(textPrefab, textContainer);
        text.SetText(senderName, message);

        if (textContainer.childCount > maxTextCount)
        {
            Destroy(textContainer.GetChild(0).gameObject);
        }
    }

    private void Fade()
    {
        fadeSeq.Restart();
    }
}