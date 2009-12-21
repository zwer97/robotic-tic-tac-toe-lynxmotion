using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace Robot.Kinematics.UI.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		#region Properties

		protected bool ThrowOnInvalidPropertyName { get; private set; }

		#endregion

		#region Constructor

		protected ViewModelBase()
		{
			ThrowOnInvalidPropertyName = true;
		}

		#endregion

		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Event Handlers

		protected virtual void OnPropertyChanged(string propertyName)
		{
			VerifyPropertyName(propertyName);

			var handler = PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

		#endregion

		#region Public Methods

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				var msg = "Invalid property name: " + propertyName;

				if (ThrowOnInvalidPropertyName)
				{
					throw new Exception(msg);
				}

				Debug.Fail(msg);
			}
		}

		#endregion
	}
}
