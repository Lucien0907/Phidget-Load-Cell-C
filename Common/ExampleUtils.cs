using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using Phidget22;
using PhidgetsControlLibrary;

namespace Phidget22.ExampleUtils {
	public class CommandLineOpen {
		private Form form;
		public string[] commandLineOverride = null;
		public CommandLineOpen(Form form) {
			this.form = form;
		}
		public string[] commandLine {
			get {
				return commandLineOverride ?? Environment.GetCommandLineArgs();
			}
			set { commandLineOverride = value; }
		}
		public void openCmdLine(Phidget p) {
			string[] args = commandLine;
			String appName = System.AppDomain.CurrentDomain.FriendlyName;

			int channel = 0;
			int serialNumber = Phidget.AnySerialNumber;
			String label = Phidget.AnyLabel;
			int hubPort = Phidget.AnyHubPort;
			bool isHubPort = false;
			String networkServerName = null;
			String password = null;

			String logFile = null;

			try { //Parse the command line flags
				for (int i = 0; i < args.Length; i++) {
					if (args[i].EndsWith(appName))
						continue;

					if (args[i].StartsWith("-"))
						switch (args[i].Remove(0, 1).ToLower()) {
						case "v":
							hubPort = int.Parse(args[++i]);
							break;
						case "c":
							channel = int.Parse(args[++i]);
							break;
						case "l":
							logFile = (args[++i]);
							break;
						case "L":
							label = args[++i];
							break;
						case "n":
							serialNumber = int.Parse(args[++i]);
							break;
						case "h":
							isHubPort = true;
							break;
						case "s":
							p.IsRemote = true; //force open to look for "remote" devices only
							if (i == args.Length - 1) //check if we are at the end of the args list implying no ssid provided
								break;
							if (args[++i] != null && !args[i].StartsWith("-")) //check for an ssid assuming we aren't at the end of the args list
								networkServerName = args[i];
							break;
						case "p":
							password = args[++i];
							break;
						default:
							goto usage;
						} else
						goto usage;
				}
				if (logFile != null)
					Phidget22.Log.Enable(LogLevel.Info, logFile);

				p.Channel = channel;
				p.DeviceSerialNumber = serialNumber;
				p.DeviceLabel = label;
				p.HubPort = hubPort;
				p.IsHubPortDevice = isHubPort;
				p.ServerName = networkServerName;

				if (p.IsRemote) {
					Net.EnableServerDiscovery(ServerType.Device); //turn on network scan
					if (password != null && networkServerName != null)
						Net.SetServerPassword(networkServerName, password); //set the password if there is one
				} else
					p.IsLocal = true; //force open to search local devices only

				p.Open();

				return; //success
			} catch (PhidgetException ex) {
				if (ex.ErrorCode == ErrorCode.Busy)
					MessageBox.Show("This hub port is in use - hub ports can only be opened in one mode at a time.",
						"Port In Use", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else if (ex.ErrorCode == ErrorCode.Duplicate)
					MessageBox.Show("This channel is already open - channels can only be opened once.",
						"Already Opened", MessageBoxButtons.OK, MessageBoxIcon.Information);
				else
					MessageBox.Show("Phidget Exception: " + ex.Message, "Phidget Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				form.Close();
				return;
			} catch { }
			usage:
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Invalid Command line arguments." + Environment.NewLine);
			sb.AppendLine("Usage: " + appName + " [Flags...]");
			sb.AppendLine("Flags:\t-n   serialNumber: Serial Number, omit for any serial");
			sb.AppendLine("\t-l   logFile: Enable phidget22 logging to logFile.");
			sb.AppendLine("\t-v   Port: Select the  Port that the device is connected to. 0 by default");
			sb.AppendLine("\t-c   deviceChannel: Select the specific channel of the device you want.  0 by default.");
			sb.AppendLine("\t-h   HubPort?: The device is connected to a hub port.");
			sb.AppendLine("\t-s   serverID\tServer Name, omit for any server");
			sb.AppendLine("\t-i   ipAddress:port\tIp Address and Port. Port is optional, defaults to 5661");
			sb.AppendLine("\t-p   password\tPassword, omit for no password" + Environment.NewLine);
			sb.AppendLine("Examples: ");
			sb.AppendLine(appName + " -n 50098 -h");
			sb.AppendLine(appName + " -n 1234567 -v 1 -c 0");
			sb.AppendLine(appName + " -r");
			sb.AppendLine(appName + " -s myphidgetserver");
			sb.AppendLine(appName + " -n 45670 -i 127.0.0.1:5661 -p password");
			MessageBox.Show(sb.ToString(), "Argument Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

			Application.Exit();
		}
	}
	/// <span class="code-SummaryComment"><summary></span>
	/// Provides a static utility object of methods and properties to interact
	/// with enumerated types.
	/// <span class="code-SummaryComment"></summary></span>
	public static class EnumHelper {
		/// <span class="code-SummaryComment"><summary></span>
		/// Gets the <span class="code-SummaryComment"><see cref="DescriptionAttribute" /> of an <see cref="Enum" /> </span>
		/// type value.
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="value">The <see cref="Enum" /> type value.</param></span>
		/// <span class="code-SummaryComment"><returns>A string containing the text of the</span>
		/// <span class="code-SummaryComment"><see cref="DescriptionAttribute"/>.</returns></span>
		public static string GetDescription(this Enum value) {
			if (value == null) {
				throw new ArgumentNullException("value");
			}

			string description = value.ToString();
			FieldInfo fieldInfo = value.GetType().GetField(description);
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0) {
				description = attributes[0].Description;
			}
			return description;
		}

		/// <span class="code-SummaryComment"><summary></span>
		/// Converts the <span class="code-SummaryComment"><see cref="Enum" /> type to an <see cref="IList" /> </span>
		/// compatible object.
		/// <span class="code-SummaryComment"></summary></span>
		/// <span class="code-SummaryComment"><param name="type">The <see cref="Enum"/> type.</param></span>
		/// <span class="code-SummaryComment"><returns>An <see cref="IList"/> containing the enumerated</span>
		/// type value and description.<span class="code-SummaryComment"></returns></span>
		public static IList ToList(this Type type) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}

			ArrayList list = new ArrayList();
			Array enumValues = Enum.GetValues(type);

			foreach (Enum value in enumValues) {
				list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
			}

			return list;
		}

		public static IList ToList(this Enum[] enumValues) {
			if (enumValues == null) {
				throw new ArgumentNullException("type");
			}

			ArrayList list = new ArrayList();

			foreach (Enum value in enumValues) {
				list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
			}

			return list;
		}
	}

	public class formCleaner {
		private Form dirty;
		public formCleaner(Form form) {
			this.dirty = form;
		}
		public void clean() {
			blankTextFields(dirty);
		}

		public void blankTextFields(Control control) {
			foreach (Control c in control.Controls) {
				if (c is TextBox) ((TextBox)c).Text = string.Empty;
				if (c is PhidgetsControlLibrary.CustomScroll) ((PhidgetsControlLibrary.CustomScroll)c).Text = string.Empty;
				blankTextFields(c);
			}
		}
	}
}