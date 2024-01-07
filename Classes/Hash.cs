/*
    @app        : Screenpresso
    @repo       : https://github.com/Aetherinox/ScreenpressoKeygen
    @author     : Aetherinox
*/

using System.Windows.Forms;

namespace ScreenpressoKG
{

    public sealed class Hash
    {

        /*
            Define > Classes
        */

        static AppInfo AppInfo             = new AppInfo( );

        /*
            method : hash
        */

        private Hash( ) {}

        /*
            method : byte

            @ret    : byteArr
        */

        private static byte[] ConvertStringToByteArray( string data )
        {
            return ( new System.Text.UnicodeEncoding( ) ).GetBytes( data );
        }

        /*
            method : file stream

            @arg    : str path
        */

        private static System.IO.FileStream GetFileStream( string path )
        {
            return ( new System.IO.FileStream( path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite ) );
        }

        /*
            method : Get SHA1 Hash
                requires a path to file to obtain the SHA1 hash

            @arg    : str path
            @ret    : str
        */

        public static string GetSHA1Hash( string path )
        {
            string result       = "";
            string res_hash     = "";

            byte[] arrByteHash;
            System.IO.FileStream fs = null;

            System.Security.Cryptography.SHA1CryptoServiceProvider sha1_hash = new System.Security.Cryptography.SHA1CryptoServiceProvider( );

            try
            {
                fs              = GetFileStream( path );
                arrByteHash     = sha1_hash.ComputeHash( fs );

                fs.Close( );

                res_hash        = System.BitConverter.ToString( arrByteHash );
                res_hash        = res_hash.Replace( "-", "" );
                result          = res_hash;
            }
            catch ( System.Exception ex )
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }

            if ( AppInfo.bIsDebug( ) )
            {
                MessageBox.Show( result, string.Format( "Hash.cs ( SHA1 ):\n{0}", result ),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                );
            }


            return ( result );
        }

        /*
            method : Get MD5 Hash
                requires a path to file to obtain the md5 hash

            @arg    : str path
            @ret    : str
        */

        public static string GetMD5Hash( string path )
        {
            string result       = "";
            string res_hash     = "";

            byte[] arrByteHash;
            System.IO.FileStream fs = null;

            System.Security.Cryptography.MD5CryptoServiceProvider sha1_hash = new System.Security.Cryptography.MD5CryptoServiceProvider( );

            try
            {
                fs              = GetFileStream( path );
                arrByteHash     = sha1_hash.ComputeHash( fs );

                fs.Close( );

                res_hash        = System.BitConverter.ToString( arrByteHash );
                res_hash        = res_hash.Replace( "-", "" );
                result          = res_hash;
            }
            catch ( System.Exception ex )
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }

            if ( AppInfo.bIsDebug( ) )
            {
                MessageBox.Show( result, string.Format( "Hash.cs ( MD5 ):\n{0}", result ),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation
                );
            }

            return ( result );
        }
    }

}
