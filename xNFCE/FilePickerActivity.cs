
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
using Android.Views.Animations;
using System.Diagnostics;

namespace xNFCE
{
	[Activity (Label = "File Picker")]			
	public class FilePickerActivity : Activity
	{

		// TODO Implement a standard default directory [SDCARD?]
		string dir = "/sdcard/xnfce/";
		string workingdir;
		List<FileListItem> dirfiles;

		ListView filePickerlist;
		Button parent;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set the view to the file picker layout
			SetContentView(Resource.Layout.FilePicker);

			// Capture the list in an object
			filePickerlist = FindViewById<ListView>(Resource.Id.filePickerList);
			// Subcribe the item click to a method
			filePickerlist.ItemClick += Fpl_Navigate;

			// Capture the navigate up button in an object
			parent = FindViewById<Button>(Resource.Id.upDirectory);
			// Subscribe it to a method
			parent.Click += Parent_Click;
						
			dirfiles = new List<FileListItem>();

			// populate the list
			popList(dir);
		}

		#region "Navigate up a directory"
		// Navigate up a directory
		// TODO Implement this as the back button [Maybe fragments??]
		void Parent_Click (object sender, EventArgs e)
		{
			popList(Directory.GetParent(workingdir).ToString());
		}
		#endregion

		#region "User clicks an item in the list"
		void Fpl_Navigate (object sender, AdapterView.ItemClickEventArgs e)
		{	
			
			String wp = Path.Combine(dirfiles[e.Position].directory, dirfiles[e.Position].name);
			if(Directory.Exists(wp))
			{
				// Check the directory is readable
				try
				{
					Directory.GetFiles(wp);
					// If it makes it this far, the directory is readable
					popList(wp);
				}
				catch
				{
					// If not readable, alert the user
					using (var toast = Toast.MakeText(this, "Inaccessible", ToastLength.Short))
					{
						toast.Show();
					}
				}
			}
			// if not a directory, select the file and return it
			else
			{
				// Check the directory for the file is writable
				FileInfo fi = new FileInfo(wp);
				try
				{
					FileStream fs = new FileStream(fi.DirectoryName + "test.txt", FileMode.Create, FileAccess.Write);
					fs.Dispose();
					File.Delete(fi.DirectoryName + "test.txt");
					Intent fIntent = new Intent(this, typeof(EncryptFileActivity));
					fIntent.PutExtra("file", wp);
					SetResult(Result.Ok, fIntent);
					Finish();
				}
				catch
				{
					// If not readable, alert the user
					using (var toast = Toast.MakeText(this, "Inaccessible", ToastLength.Short))
					{
						toast.Show();
					}
				}

			}
		}
		#endregion

		#region "Populate the list view"
		/// <summary>
		/// Populates the list view with directories and files
		/// </summary>
		protected void popList(string d)
		{

			parent.Enabled = (dir == d) ? false : true;

			// Set the working directory
			workingdir = d;

			// Set the title to the current directory
			this.Title = "Directory: " + d;
			// Clear the list ready for the next directory
			dirfiles.Clear();

			// Populate the list with directories
			foreach(string f in Directory.GetDirectories(d))
			{
				FileListItem fli = new FileListItem(f);
				dirfiles.Add(fli);
			}
			// populate the list with files
			foreach(string f in Directory.GetFiles(d))
			{
				FileListItem fli = new FileListItem(f);
				dirfiles.Add(fli);
			}

			// apply the list to the listview with a custom adapter
			filePickerlist.Adapter = new FilePickerAdapter(this, dirfiles);
		}
		#endregion

	}
}

