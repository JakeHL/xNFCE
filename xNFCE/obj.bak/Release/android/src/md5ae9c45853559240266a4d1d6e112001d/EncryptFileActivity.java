package md5ae9c45853559240266a4d1d6e112001d;


public class EncryptFileActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("xNFCE.EncryptFileActivity, xNFCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EncryptFileActivity.class, __md_methods);
	}


	public EncryptFileActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == EncryptFileActivity.class)
			mono.android.TypeManager.Activate ("xNFCE.EncryptFileActivity, xNFCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
