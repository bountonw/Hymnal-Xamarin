// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Hymnal.Native.TvOS.Views
{
	[Register ("NumberViewController")]
	partial class NumberViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UITextField hymnNumberTextField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton openHymnButton { get; set; }

		[Action ("openHymnActionButton:")]
		partial void openHymnActionButton (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (hymnNumberTextField != null) {
				hymnNumberTextField.Dispose ();
				hymnNumberTextField = null;
			}

			if (openHymnButton != null) {
				openHymnButton.Dispose ();
				openHymnButton = null;
			}
		}
	}
}
