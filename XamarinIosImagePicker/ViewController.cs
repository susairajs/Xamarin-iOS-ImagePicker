using System;
using Foundation;
using UIKit;

namespace XamarinIosImagePicker
{
	
	public partial class ViewController : UIViewController
	{
		UIImagePickerController picker;
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void BtnPick_TouchUpInside(UIButton sender)
		{
			picker = new UIImagePickerController();
			picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
			picker.FinishedPickingMedia += Finished;
			picker.Canceled += Canceled;

			PresentViewController(picker, animated: true, completionHandler: null);
		}
		public void Finished(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					isImage = true;
					break;
				case "public.video":
					break;
			}
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null)
				Console.WriteLine("Url:" + referenceURL.ToString());
			if (isImage)
			{

				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null)
				{
					imgView.Image = originalImage;
				}
			}
			else
			{ 
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}
			picker.DismissModalViewController(true);
		}

		void Canceled(object sender, EventArgs e)
		{
			picker.DismissModalViewController(true);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
