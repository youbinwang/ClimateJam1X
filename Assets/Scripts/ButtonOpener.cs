using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonOpener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SourceOne()
    {
        Application.OpenURL("https://www.froglife.org/2019/11/28/croaking-science-artificial-light-at-night-a-problem-for-amphibians/");
    }

    public void SourceTwo()
    {
        Application.OpenURL("https://news.fiu.edu/2021/its-lights-out-for-moths,-if-you-like-your-nightly-pollinators");
    }

    public void SourceThree()
    {
        Application.OpenURL("https://butterfly-conservation.org/news-and-blog/streetlights-reduce-moth-populations");
    }

    public void SourceFour()
    {
        Application.OpenURL("https://butterfly-conservation.org/news-and-blog/streetlights-reduce-moth-populations"); // whoops this is the same as three but whatever
    }

    public void SourceFive()
    {
        Application.OpenURL("https://butterfly-conservation.org/news-and-blog/how-to-reduce-light-pollution-at-home#:~:text=Light%20pollution%20threatens%20many%20nocturnal%20insects%2C%20including,steps%20to%20reduce%20light%20pollution%20at%20home");
    }

    public void SourceSix()
    {
        Application.OpenURL("https://natlands.org/how-you-can-combat-light-pollution/");

    }

    public void SourceSeven()
    {
        Application.OpenURL("https://www.dcceew.gov.au/campaign/light-pollution/turtles");
    }
    public void SourceEight()
    {
        Application.OpenURL("https://www.jackcentral.org/news/flagstaff-nights-the-costs-of-maintaining-dark-skies/article_88ed27d2-e08b-11e5-8100-1bc997654b8b.html");
    }
}
