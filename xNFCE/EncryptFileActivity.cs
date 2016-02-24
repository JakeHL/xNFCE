
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace xNFCE
{
	[Activity (Label = "EncryptFileActivity")]			
	public class EncryptFileActivity : Activity
	{

		EditText _fileLoc, _password;
		Button _btnGo;
		CheckBox _usePassword;

		string fileLoc
		{
			get { return _fileLoc.Text; }
			set
			{
				_fileLoc.Text = value;
				_btnGo.Enabled = true;
			}
		}


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.EncryptFile);

			// Grab the file textbox box in an object and subscribe it to the file picker function
			_fileLoc = FindViewById<EditText>(Resource.Id.filePath);
			_fileLoc.Click += SelectFile;

			// Grab the button and subscribe it to the encrypt function
			_btnGo = FindViewById<Button>(Resource.Id.btnGo);
			_btnGo.Click += Encrypt;

			// Grab the password box to use with the encryption
			_password = FindViewById<EditText>(Resource.Id.password);
			// Grab the password checkbox
			_usePassword = FindViewById<CheckBox>(Resource.Id.usePassword);
			// subscribe the checkbox to the function
			_usePassword.Click += enablePassword;
		}


		#region "enable or disable the password field"
		void enablePassword (object sender, EventArgs e)
		{
			if(_usePassword.Checked == true)
			{
				_password.Enabled = true;
			}
			else
			{
				_password.Enabled = false;
			}
		}
		#endregion

		#region "encrypt the file"
		void Encrypt (object sender, EventArgs e)
		{
			// Create a crypto object, pass it the input, output and password
			// the output appends the .nenc extension to the file
			// TODO get the encryption type from a radio button
			Crypto enc = new Crypto(_fileLoc.Text, _password.Text, Crypto.cryptype.AES);
			// Encrypt the file
			string message = enc.Encrypt() ? "File Encrypted" : "Encryption Failed";
			using (var toast = Toast.MakeText(this, message, ToastLength.Short))
			{
				toast.Show();
			}
		}
		#endregion

		#region "open the file picker"
		void SelectFile (object sender, EventArgs e)
		{
			// Create an intent and start the file picker
			Intent intent = new Intent(this, typeof(FilePickerActivity));
			StartActivityForResult(intent, 0);
		}
		#endregion

		#region "get the file back from the file picker"
		// Get the file back from the file picker activity
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if((data != null) && (data.HasExtra("file")))
			{
				fileLoc = data.GetStringExtra("file");
			}
		}
		#endregion
	}
}

