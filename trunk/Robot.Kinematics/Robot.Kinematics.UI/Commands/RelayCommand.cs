using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

namespace Robot.Kinematics.UI.Commands
{
	public class RelayCommand : ICommand
	{
		#region Member Fields

		readonly Action<object> m_Execute;
		readonly Predicate<object> m_CanExecute;

		#endregion

		#region Constructors

		public RelayCommand(Action<object> execute)
			: this(null, execute)
		{
		}

		public RelayCommand(Predicate<object> canExecute, Action<object> execute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			m_Execute = execute;
			m_CanExecute = canExecute;
		}
		#endregion // Constructors

		#region ICommand Members

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			return m_CanExecute == null ? true : m_CanExecute(parameter);
		}

		public void Execute(object parameter)
		{
			m_Execute(parameter);
		}

		#endregion
	}
}
