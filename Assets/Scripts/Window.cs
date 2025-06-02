using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public abstract class Window : MonoBehaviour
{
    public Dictionary<string, Button> Buttonset = new Dictionary<string, Button>();
    public Dictionary<string, Image> Imageset = new Dictionary<string, Image>();
    public Dictionary<string, RawImage> RawImageset = new Dictionary<string, RawImage>();
    public Dictionary<string, TextMeshProUGUI> Textset = new Dictionary<string, TextMeshProUGUI>();
    [SerializeField] public bool isInitialwindow;
    protected virtual void Awake()
    {
        AddComponents(transform);
    }

    protected virtual void Initialize()
    {

    }
    void AddComponents(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Button 컴포넌트가 있다면 Buttonset에 추가
            Button btn = child.GetComponent<Button>();
            if (btn != null && !Buttonset.ContainsKey(child.name))
            {
                Buttonset.Add(child.name, btn);
            }

            // Image 컴포넌트가 있다면 Imageset에 추가
            Image img = child.GetComponent<Image>();
            if (img != null && !Imageset.ContainsKey(child.name))
            {
                Imageset.Add(child.name, img);
            }

            // Text 컴포넌트가 있다면 Textset에 추가
            TextMeshProUGUI txt = child.GetComponent<TextMeshProUGUI>();
            if (txt != null && !Textset.ContainsKey(child.name))
            {
                Textset.Add(child.name, txt);
            }

            RawImage rawimg = child.GetComponent<RawImage>();
            if (rawimg != null && !RawImageset.ContainsKey(child.name))
            {
                RawImageset.Add(child.name, rawimg);
            }
        }
    }

    public void ShowAllUI()
    {
        foreach (var button in Buttonset.Values)
            button.gameObject.SetActive(true);

        foreach (var image in Imageset.Values)
            image.gameObject.SetActive(true);

        foreach (var text in Textset.Values)
            text.gameObject.SetActive(true);

        foreach (var rawimg in RawImageset.Values)
            rawimg.gameObject.SetActive(true);

        Initialize();
    }

    // ✅ 모든 버튼과 이미지를 숨기기
    public void HideAllUI()
    {
        Debug.Log($"HideAllUI: {Buttonset.Values.Count} + {Imageset.Values.Count} + {Textset.Values.Count}");

        foreach (var button in Buttonset.Values)
            button.gameObject.SetActive(false);

        foreach (var image in Imageset.Values)
            image.gameObject.SetActive(false);

        foreach (var text in Textset.Values)
        {
            text.gameObject.SetActive(false);
        }

        foreach (var rawimg in RawImageset.Values)
            rawimg.gameObject.SetActive(false);
    }

    // ✅ 특정 버튼과 이미지만 보이게 하기
    public void ShowUIElement(string key)
    {
        if (Buttonset.ContainsKey(key))
            Buttonset[key].gameObject.SetActive(true);

        if (Imageset.ContainsKey(key))
            Imageset[key].gameObject.SetActive(true);

        if (Textset.ContainsKey(key))
            Textset[key].gameObject.SetActive(true);

        if (RawImageset.ContainsKey(key))
            RawImageset[key].gameObject.SetActive(true);
    }

    // ✅ 특정 버튼과 이미지를 숨기기
    public void HideUIElement(string key)
    {
        if (Buttonset.ContainsKey(key))
            Buttonset[key].gameObject.SetActive(false);

        if (Imageset.ContainsKey(key))
            Imageset[key].gameObject.SetActive(false);

        if (Textset.ContainsKey(key))
            Textset[key].gameObject.SetActive(false);

        if (RawImageset.ContainsKey(key))
            RawImageset[key].gameObject.SetActive(false);
    }

    // ✅ 모든 버튼의 클릭 기능을 비활성화 (회색 처리됨)
    public void DisableAllButtons()
    {
        foreach (var button in Buttonset.Values)
            button.interactable = false;
    }

    // ✅ 모든 버튼의 클릭 기능을 활성화
    public void EnableAllButtons()
    {
        foreach (var button in Buttonset.Values)
            button.interactable = true;
    }

    // ✅ 특정 버튼의 클릭 기능을 비활성화
    public void DisableButton(string key)
    {
        if (Buttonset.ContainsKey(key))
            Buttonset[key].interactable = false;
    }

    // ✅ 특정 버튼의 클릭 기능을 활성화
    public void EnableButton(string key)
    {
        if (Buttonset.ContainsKey(key))
            Buttonset[key].interactable = true;
    }


}
