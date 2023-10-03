using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private readonly Stack<UIPopup> _popupStack = new();

    private UIScene _sceneUI;

    private int _order = 0;

    public static GameObject Root
    {
        get
        {
            if (GameObject.Find("@UI_Root") is not { } root)
            {
                root = new() { name = "@UI_Root" };
            }

            return root;
        }
    }

    public void Init()
    {
        ClearAllPopup();
        _popupStack.Clear();
    }

    public void SetCanvas(GameObject gameObject, bool sort = true)
    {
        var canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        canvas.sortingOrder = sort ? _order++ : 0;
    }

    public T ShowSceneUI<T>(string name = null)
        where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"UI/Scene/{name}");
        var sceneUI = gameObject.GetOrAddComponent<T>();
        _sceneUI = sceneUI;

        gameObject.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null, bool usePool = true)
        where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"UI/Popup/{name}", usePool: usePool);
        var popup = gameObject.GetOrAddComponent<T>();
        _popupStack.Push(popup);

        gameObject.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popupStack.Count == 0)
        {
            return;
        }

        while (_popupStack.TryPeek(out var last) && last != popup)
        {
            last = _popupStack.Pop();
            Managers.Resource.Release(last.gameObject);
        }

        if (_popupStack.TryPop(out popup))
        {
            //popup = _popupStack.Pop();
            if (popup != null && popup.gameObject != null)
            {
                Managers.Resource.Release(popup.gameObject);
            }
        }

        _order--;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.TryPeek(out var popup))
        {
            ClosePopupUI(popup);
        }
    }

    public void ClearAllPopup()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
}