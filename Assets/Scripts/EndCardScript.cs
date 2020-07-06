using UnityEngine;
using TMPro;

public class EndCardScript : MonoBehaviour
{
    public TextMeshProUGUI hits;
    public TextMeshProUGUI deaths;
    public TextMeshProUGUI comment;

    public string[] zeroCmt;
    public string[] oneCmt;
    public string[] twoCmt;
    public string[] threeCmt;

    public void EndInit()
    {
        int hit = Settings.hits;
        int death = Settings.deaths;

        hits.text = hit.ToString();
        deaths.text = death.ToString();

        string comment = "";
        if (hit == 0)
        {
            comment = zeroCmt[Random.Range(0, zeroCmt.Length)];
        }
        else if (hit == 1)
        {
            comment = oneCmt[Random.Range(0, oneCmt.Length)];
        }
        else if (hit == 2)
        {
            comment = twoCmt[Random.Range(0, twoCmt.Length)];
        }
        else if (hit >= 3)
        {
            comment = threeCmt[Random.Range(0, threeCmt.Length)];
        }
        this.comment.text = comment;
    }
}
