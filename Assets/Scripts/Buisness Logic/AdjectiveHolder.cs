using System.Collections;
using System.Collections.Generic;


public static class AdjectiveHolder
{

    public static string GetGoodAdjective()
    {
        List<string> words = new List<string>();
        words.Add("An astonishing");
        words.Add("An awe-inspiring");
        words.Add("A beautiful");
        words.Add("A breathtaking");
        words.Add("A formidable");
        words.Add("A frightening");
        words.Add("An imposing");
        words.Add("An impressive");
        words.Add("An intimidating");
        words.Add("A magnificent");
        words.Add("A overwhelming");
        words.Add("A shocking");
        words.Add("A stunning");
        words.Add("A wondrous");
        words.Add("A wonderful");
        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetOkAdjective()
    {
        List<string> words = new List<string>();
        words.Add("A Ho-hum");
        words.Add("An Ok");
        words.Add("An acceptable");
        words.Add("A seen it before kind of");
        words.Add("A typical sort of");
        words.Add("A type-A");
        words.Add("A passable");
        words.Add("A 'C-'");
        words.Add("A fair to middling");
        words.Add("An ordinary");
        words.Add("A garden type");
        words.Add("A dime a dozen kind of");
        words.Add("A regular");
        words.Add("A moderate");
        words.Add("A humdrum");
        words.Add("A mainstream");
        words.Add("A more of the same kind of");
        words.Add("A middle of the road");
        words.Add("An usual");
        words.Add("An unexceptional");



        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetBadAdjective()
    {
        List<string> words = new List<string>();
        words.Add("An alarmingly poor");
        words.Add("An astonishingly bad");
        words.Add("An awful");
        words.Add("A dreadful");
        words.Add("A horrible");
        words.Add("A gorrifyingly sad");
        words.Add("A terrible");
        words.Add("A terrifyingly poor");
        words.Add("An abhorent");
        words.Add("A terrifyingly poor");
        words.Add("A horrid");
        words.Add("An appalling");
        words.Add("A vile");
        words.Add("A repulsive");
        words.Add("An odious");
        words.Add("An obnoxious");

        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetVictoryWord()
    {
        List<string> words = new List<string>();
        words.Add("triumph");
        words.Add("conquest");
        words.Add("killing");
        words.Add("destruction");
        words.Add("subjugation");
        words.Add("superiority");
        words.Add("clean sweep");
        words.Add("hole in one");
        words.Add("achievement");
        words.Add("grand slam");
        words.Add("bull's-eye");
        words.Add("upset");
        words.Add("overthrow");
        words.Add("victory");
        words.Add("domination");
        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetHorizontalWord()
    {
        List<string> words = new List<string>();
        words.Add("flush");
        words.Add("parallel");
        words.Add("recumbent");
        words.Add("level");
        words.Add("horizontal");
        words.Add("supine");
        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetVerticalWord()
    {
        List<string> words = new List<string>();
        words.Add("perpendicular");
        words.Add("upright");
        words.Add("on end");
        words.Add("cliff like");
        words.Add("up-and-down");
        words.Add("straight-up");
        words.Add("vertical");
        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetDiagonalWord()
    {
        List<string> words = new List<string>();
        words.Add("crosswise");
        words.Add("slanted");
        words.Add("crossways");
        words.Add("oblique");
        words.Add("inclining");
        words.Add("cater-cornered");
        words.Add("diagonal");
        return words[UnityEngine.Random.Range(0, words.Count)];
    }
    public static string GetRandomName()
    {
        List<string> words = new List<string>();
        words.Add("Chuckster");
        words.Add("Pernicious the Baneful");
        words.Add("Billy the Childish");
        words.Add("Frig");
        words.Add("Mr Particular");
        words.Add("Torf");
        words.Add("Lokschnik DanButterhaun");
        words.Add("'M' is for Murder");
        words.Add("Terminatrix");
        words.Add("Mildred");
        words.Add("Connexicon");
        words.Add("Bill Clinton");
        words.Add("Sneaky Sam");
        words.Add("Tortado the Clam");
        words.Add("Aspen CollassPin");
        words.Add("Mastah of Disastah");
        words.Add("Bromethius");

        return words[UnityEngine.Random.Range(0, words.Count)];
    }
}
