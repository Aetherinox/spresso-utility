using System.Windows.Forms;

/*

    Aetherx > Control > Button

    Button customization

*/

namespace ScreenpressoKG
{

    public class AetherxButton : Button
    {

        /*
            Show keyboard cues no matter what.
            By default, user must press ALT to see them.
        */

        protected override bool ShowKeyboardCues
        {
            get
            {
                return true;
            }
        }
    }
}
