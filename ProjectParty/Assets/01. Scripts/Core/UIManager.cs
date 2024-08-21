using OMG.Inputs;
using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [SerializeField] private UIInputSO input;

    //[Space]
    //[SerializeField] private RenderTexture[] playerRenderTextures;

    private UICanvas mainCanvas;
    public UICanvas MainCanvas
    {
        get
        {
            if (mainCanvas == null)
            {
                mainCanvas = GameObject.Find("MainCanvas").GetComponent<UICanvas>();
            }

            return mainCanvas;
        }
    }

    private Stack<UIPanel> panelStack;
    public int StackCount => panelStack.Count;

    public void Awake()
    {
        Instance = this;
        panelStack = new Stack<UIPanel>();
    }

    public void ShowPanel(UIPanel panel)
    {
        if (panel == null)
            return;

        if (StackCount > 0)
        {
            panelStack.Peek().Hide();
        }
        panelStack.Push(panel);

        if (panelStack.Count == 1)
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            input.OnBackEvent += HidePanelByInput;
        }
    }

    public void HidePanel()
    {
        if (panelStack.Count == 0)
            return;

        UIPanel panel = panelStack.Pop();
        panel.Hide();

        if (panelStack.Count == 0)
        {
            input.OnBackEvent -= HidePanelByInput;
            InputManager.ChangeInputMap(InputManager.PrevInputMapType);
        }
        else
        {
            panelStack.Peek().OnlyShow();
        }
    }

    private void HidePanelByInput()
    {
        HidePanel();
    }
}