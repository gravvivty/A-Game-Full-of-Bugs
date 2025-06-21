using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;

public class SimpleDialogueBox : MonoBehaviour
{

    [SerializeField]
    [TextArea]
    private List<string> _dialogueLines;
    private int _lineIndex;

    private TMP_Text _text;
    private CanvasGroup _group;
    private bool _started;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _group = GetComponent<CanvasGroup>();
        _group.alpha = 0;
    }

    private void OnValidate()
    {
        if (_dialogueLines.Count > 0)
        {
            if (_text == null) _text = GetComponent<TMP_Text>();
            _text.SetText(_dialogueLines[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!_started)
            {
                _lineIndex = 0;
                // _text.SetText(_dialogueLines[_lineIndex]);
                _group.alpha = 1;
                _started = true;
            }
            else if (_lineIndex < _dialogueLines.Count)
            {
                // This logic is broken and doesnt work - it increases it every frame, every click no matter what
                // _text.SetText(_dialogueLines[_lineIndex++]);
            }
            else
            {
                _group.alpha = 0;
            }
        }
    }
}
