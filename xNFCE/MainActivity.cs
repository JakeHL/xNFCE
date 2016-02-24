using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace xNFCE
{
	[Activity (Label = "xNFCE", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our buttons from the layout resource
			Button btnToEncrypt = FindViewById<Button> (Resource.Id.btnToEncrypt);
			Button btnToDecrypt = FindViewById<Button>(Resource.Id.btnToDecrypt);

			// Subscribe the buttons click to a method
			btnToEncrypt.Click += goToEncrypt;
			btnToDecrypt.Click += goToDecrypt;
		}

		// Method to open the decrypt file activity
		void goToDecrypt (object sender, System.EventArgs e)
		{
			Intent intent = new Intent(this, typeof(DecryptFileActivity));
			StartActivity(intent);
		}

		// Method for the encrypt button to open the encrypt a file screen
		void goToEncrypt (object sender, System.EventArgs e)
		{
			Intent intent = new Intent(this, typeof(EncryptFileActivity));
			StartActivity(intent);
		}
	}
}


