/*
    @app        : Screenpresso
    @repo       : https://github.com/Aetherinox/ScreenpressoKeygen
    @author     : Aetherinox
*/

using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;
using Lng = ScreenpressoKG.Properties.Resources;
using Cfg = ScreenpressoKG.Properties.Settings;

namespace ScreenpressoKG
{

    /*
        OReceiver > Status Bar
    */

    public interface IReceiver
    {
        void Status( string message );
    }

    /*
        Receiver > Status Bar
    */

    public static class StatusBar
    {
        private static IReceiver recv = null;

        /*
            Receiver > Initialize
        */

        public static void InitializeReceiver( IReceiver f )
        {
            recv = f;
        }

        /*
            Receiver > Send Message
        */

        public static void Update( string message )
        {
            if ( recv != null ) recv.Status( message );
        }
    }
}
