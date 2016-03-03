
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace xNFCE
{
	public class FilePickerFragmentClass : Fragment
	{
		public ListView lv;
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			LinearLayout ll = (LinearLayout)inflater.Inflate(Resource.Layout.FilePickerFragment, container, false);
			ListView lv = ll.FindViewById<ListView>(Resource.Id.FilePickerList);
			return ll;
		}

	}
}

