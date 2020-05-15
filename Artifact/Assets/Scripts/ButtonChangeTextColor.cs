using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChangeTextColor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Button button;
    private Text text;

    void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        if (button.name == "StartGameButton")
        {
            button.Select();
        }
        else if (EventSystem.current.currentSelectedGameObject == null)
        {
            button.Select();
        }
    }


    public void OnSelect(BaseEventData eventData)
    {
        text.color = Color.white;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = Color.black;
    }
}
