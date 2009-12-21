using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SSC32Communication
{
	public class SerialPortHelper
	{
		public static string FindProlificPortName()
		{
			using (RegistryKey localMachineRegKey = Registry.LocalMachine)
			{
				using (RegistryKey hardwareRegKey = localMachineRegKey.OpenSubKey("HARDWARE"))
				{
					using (RegistryKey deviceMapRegKey = hardwareRegKey.OpenSubKey("DEVICEMAP"))
					{
						using (RegistryKey serialComRegKey = deviceMapRegKey.OpenSubKey("SERIALCOMM"))
						{
							foreach (string valueName in serialComRegKey.GetValueNames())
							{
								if (valueName.EndsWith("ProlificSerial0", StringComparison.Ordinal))
								{
									// we found the entry for our prolific USB to Serial device
									return (string)serialComRegKey.GetValue(valueName);
								}
							}
						}
					}
				}
			}
			return null;
		}
	}
}
