using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class winpanelController : MonoBehaviour
{
    private int emCount = 0;
    private int emKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        emCount = GameObject.FindGameObjectsWithTag("Enemy").ToArray<GameObject>().Length;
        emKilled = 0;
        gameObject.GetComponentsInChildren<Text>()[1].GetComponent<Text>().text = $"So luong quai vat da tieu diet: {emKilled}";
    }

    // Update is called once per frame
    public void ChangeEmkilledText()
    {
        emKilled = emCount - GameObject.FindGameObjectsWithTag("Enemy").ToArray<GameObject>().Length;
        gameObject.GetComponentsInChildren<Text>()[1].GetComponent<Text>().text = $"So luong quai vat da tieu diet: {emKilled}";
    }
    
}
