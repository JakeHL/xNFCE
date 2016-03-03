using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Java.IO;
using Android.App.Assist;
using System.Linq;
using System.Runtime.InteropServices;
using Android.Views.Animations;
using Android.Widget;
using System.Collections;
using Java.Nio.Channels;
using Android.Telephony.Cdma;
using Android.Media;
using Android.App;
using Android.AccessibilityServices;
using Android.Util;


namespace xNFCE
{
	public class Crypto
	{

		public enum cryptype
		{
			DES,
			THREEDES,
			AES
		};

		// Variables for the file input, output and the password to encrypt the file
		string _file;
		byte[] _password;
		cryptype _type;

		/// <summary>
		/// Initializes a new instance of the <see cref="xNFCE.Crypto"/> class.
		/// </summary>
		/// <param name="input">Input.</param>
		/// <param name="output">Output.</param>
		/// <param name="password">Password.</param>
		public Crypto(string file, string password, cryptype type = cryptype.AES)
		{
			// Set the properties for the encryption
			_file = file;
			_password = hash(password);
			_type = type;
		}


		#region "Select encryption method, append IV etc."
		/// <summary>
		/// Run the appropriate encryption function
		/// </summary>
		public bool Encrypt()
		{
			byte[] iv = new byte[0];
			bool result = false;
			string path = _file + GlobalValues.fextension;
			switch (_type)
			{
				case cryptype.DES:
					// TODO Implement
					break;
				case cryptype.THREEDES:
					// TODO Implement
					break;
				case cryptype.AES:
					using(RijndaelManaged rm = new RijndaelManaged())
					{
						rm.GenerateIV();
						iv = rm.IV;
					}
					result = AESenc(path, iv);
					break;
			}

			// Write the iv to the end of the file
			using(FileStream ivapp = new FileStream(path, FileMode.Append))
			{
				ivapp.Write(iv, 0, iv.Length);
			}

			// Make the file visible to usb connection etc.
			FileInfo fi = new FileInfo(_file);
		    MediaScannerConnection.ScanFile(Application.Context, new string[] {_file + GlobalValues.fextension}, null, null);
			
			// TODO make this optional
			if(System.Diagnostics.Debugger.IsAttached)
			{
				System.IO.File.Delete(_file);
			}

			return result;
		}


		/// <summary>
		/// Run the appropriate decryption function
		/// </summary>
		public bool Decrypt()
		{
			FileInfo fi = new FileInfo(_file);
			string path = Path.Combine(fi.DirectoryName, Path.GetFileNameWithoutExtension(_file));	
			bool result = false;
			byte[] iv;
			switch (_type)
			{
				case cryptype.DES:
					// TODO Implement
					break;
				case cryptype.THREEDES:
					// TODO Implement
					break;
				case cryptype.AES:
					iv = getIV(16);
					result = AESdec(path, iv);
					break;
			}

			// Make the file visible to usb connection etc.
		    MediaScannerConnection.ScanFile(Application.Context, new string[] {path}, null, null);

			// TODO make this optional
			if(System.Diagnostics.Debugger.IsAttached)
			{
				System.IO.File.Delete(_file);
			}
		    			
			return result;
		}


		// Get the IV from the end of the file & remove it from the file
		byte[] getIV(int length)
		{
			FileInfo fi = new FileInfo(_file);
			int filesize = (int)fi.Length - length;
			// Store the IV
			byte[] result = System.IO.File.ReadAllBytes(_file).Skip(filesize).Take(length).ToArray();
			// Remove it from the file
			using(FileStream fs = fi.Open(FileMode.Open))
			{
				fs.SetLength(Math.Max(0, fi.Length - length));
			}
			return result;
		}
		#endregion


		bool AESenc(string path, byte[] iv)
		{
			try
			{
				// Create a filestream and use it to write the outputfile
				using(FileStream output = new FileStream(path, FileMode.Create))
				{
					// Create a managed crypto object
					RijndaelManaged RMCrypto = new RijndaelManaged();

					// Create a cryptostream to write the encrypted data
					using(CryptoStream cs = new CryptoStream(output,
															RMCrypto.CreateEncryptor(_password, iv),
															CryptoStreamMode.Write))
					{
						// read in the input file to be encrypted
						using(FileStream input = new FileStream(_file, FileMode.Open))
						{
							// Cycle through the data and use the encryptor to write it
							int data;
							while((data = input.ReadByte()) != -1)
							{
								cs.WriteByte((byte)data);
							}
						}
					}
				}


				// if the application has reached this point
				// return true for successful encryption
				return true;
			}
			catch
			{
				// If the encryption fails return false for failed encryption
				return false;
			}
		}


		bool AESdec(string path, byte[] iv)
		{				
				try
				{
					using(FileStream input = new FileStream(_file, FileMode.Open))
					{

		        		RijndaelManaged RMCrypto = new RijndaelManaged();

				        using(CryptoStream cs = new CryptoStream(input,
				            								RMCrypto.CreateDecryptor(_password, iv),
				            								CryptoStreamMode.Read))
				        {
				        			        	
					        using(FileStream output = new FileStream(path, FileMode.Create))
					        {
					            int data;
						        while ((data = cs.ReadByte()) != -1)
						            output.WriteByte((byte)data);
					        }
				        }
			        }
					return true;
				}
				catch
				{
					return false;
				}
		}


		#region "hash password"
		/// <summary>
		/// Hash the password and return it as a byte array
		/// </summary>
		/// <param name="pwd">Pwd.</param>
		byte[] hash(String pwd)
		{
			// System.Text provides .GetBytes() this converts a string to the series of bytes
			// Convert the password string to an array of bytes
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pwd);
			// Create an instance of Managed SHA256
			SHA256Managed hashman = new SHA256Managed();
			// Hash the array of bytes
			byte[] hash = hashman.ComputeHash(bytes);
			return hash;
		}
        #endregion
	}
}

