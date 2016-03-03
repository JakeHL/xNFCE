using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using Android.Util;

namespace xNFCE
{
	public class FilePickerAdapter : BaseAdapter<FileListItem>
	{
		Activity context;
		List<FileListItem> fli;

		// Assign the list and the context
		public FilePickerAdapter (Activity _context, List<FileListItem> _fli)
		{
			context = _context;
			fli = _fli;
		}

		// Get the amount of items in the list
		public override int Count
		{
			get { return fli.Count;	}
		}

		// Get the row number
		public override long GetItemId(int position)
		{
			return position;
		}

		// Return the data associated with the row
		public override FileListItem this[int position]
		{
			get {return fli[position]; }
		}

		// Return the view for each row
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			// Reuse a view if one is available
			View view = convertView;
			// Otherwise create a new one
			if(view == null)
			{
				view = context.LayoutInflater.Inflate(Resource.Layout.FileListItem, null);
			}

			// set the image depending on the type of object
			if(fli[position].type == "file")
			{
				view.FindViewById<ImageView>(Resource.Id.fileImg).SetImageResource(Resource.Drawable.file);
			}
			else
			{
				view.FindViewById<ImageView>(Resource.Id.fileImg).SetImageResource(Resource.Drawable.folder);
			}

			// set the name and size of the object
			view.FindViewById<TextView>(Resource.Id.fileName).Text = fli[position].name;
			view.FindViewById<TextView>(Resource.Id.fileSize).Text = fli[position].size;

			return view;
		}

	}
}

