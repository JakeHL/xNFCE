
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
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace xNFCE
{
	[Activity (Label = "DecryptFileActivity")]			
	public class DecryptFileActivity : Activity
	{
		Button _btnGo;
		CheckBox _chk;
		EditText _decLoc, _password;
		string decLoc
		{
			get { return _decLoc.Text; }
			set
			{
				_decLoc.Text = value;
				_btnGo.Enabled = true;
			}
		}
		
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView(Resource.Layout.DecryptFile);

			_decLoc = FindViewById<EditText>(Resource.Id.decryptFilePath);
			_decLoc.Click += SelectFile;

			_chk = FindViewById<CheckBox>(Resource.Id.decryptUsePassword);
			_chk.Click += enablePassword;

			_password = FindViewById<EditText>(Resource.Id.decryptPassword);

			_btnGo = FindViewById<Button>(Resource.Id.decryptBtnGo);
			_btnGo.Click += decryptfile;

		}

		void enablePassword (object sender, EventArgs e)
		{
			_password.Enabled = _chk.Checked ? true : false;
		}


		void decryptfile (object sender, EventArgs e)
		{
			Crypto crypt = new Crypto(decLoc, _password.Text, Crypto.cryptype.AES);
			string msg = crypt.Decrypt() ? "File Decrypted" : "Decryption failed";
			using (var toast = Toast.MakeText(this, "File Decrypted", ToastLength.Short))
			{
				toast.Show();
			}
		}


		void SelectFile (object sender, EventArgs e)
		{
			Intent intent = new Intent(this, typeof(FilePickerActivity));
			StartActivityForResult(intent, 0);
		}


		// Get the result from the file picker activity
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
			if((data != null) && (data.HasExtra("file")))
			{
				decLoc = data.GetStringExtra("file");
			}
		}

	}
}

