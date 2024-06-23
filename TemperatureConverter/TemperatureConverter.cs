using System;
using System.Globalization;

public class CPHInline
{
	public bool Execute()
	{
		CPH.TryGetArg("command", out string command);
		CPH.TryGetArg("rawInput", out string rawInput);
		string[] args = rawInput.Split(' ');
		if (args.Length < 3) {
			CPH.SendMessage($"Usage {command} <source:c|f|k|r|é|e> <target:c|f|k|r|é|e> <value>", true);
			return false;
		}
		char source = char.ToLower(args[0][0]);
		char target = char.ToLower(args[1][0]);
		bool valueParsed = double.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double value);
		if (!valueParsed) {
			CPH.SendMessage("Failed to parsed value to double", true);
			return false;
		}
		switch (source) {
			case 'c':
				switch (target) {
					case 'c':
						CPH.SendMessage($"{value} °C = {value} °C", true);
						break;
					case 'f':
						CPH.SendMessage($"{value} °C = {(9.0 / 5.0) * value + 32.0} °F", true);
						break;
					case 'k':
						CPH.SendMessage($"{value} °C = {value + 273.15} K", true);
						break;
					case 'r':
						CPH.SendMessage($"{value} °C = {value * (9.0 / 5.0) + 491.67} °R", true);
						break;
					case 'é':
					case 'e':
						CPH.SendMessage($"{value} °C = {value * 4.0 / 5.0} °Ré", true);
						break;
					default:
						CPH.SendMessage("Please specify a temperature unit to convert to", true);
						break;
				}
				break;
			case 'f':
				switch (target) {
					case 'c':
						CPH.SendMessage($"{value} °F = {(value - 32.0) / (9.0 / 5.0)} °C", true);
						break;
					case 'f':
						CPH.SendMessage($"{value} °F = {value} °F", true);
						break;
					case 'k':
						CPH.SendMessage($"{value} °F = {(value + 459.67) * 5.0 / 9.0} K", true);
						break;
					case 'r':
						CPH.SendMessage($"{value} °F = {value + 459.67} °R", true);
						break;
					case 'é':
					case 'e':
						CPH.SendMessage($"{value} °F = {(value - 32.0) * 4.0 / 9.0} °Ré", true);
						break;
					default:
						CPH.SendMessage("Please specify a temperature unit to convert to", true);
						break;
				}
				break;
			case 'k':
				switch (target) {
					case 'c':
						CPH.SendMessage($"{value} K = {value - 273.15} °C", true);
						break;
					case 'f':
						CPH.SendMessage($"{value} K = {value * 9.0 / 5.0 - 459.67} °F", true);
						break;
					case 'k':
						CPH.SendMessage($"{value} K = {value} K", true);
						break;
					case 'r':
						CPH.SendMessage($"{value} K = {value * 9.0 / 5.0} °R", true);
						break;
					case 'é':
					case 'e':
						CPH.SendMessage($"{value} °C = {(value - 273.15) * 4.0 / 5.0} °Ré", true);
						break;
					default:
						CPH.SendMessage("Please specify a temperature unit to convert to", true);
						break;
				}
				break;
			case 'r':
				switch (target) {
					case 'c':
						CPH.SendMessage($"{value} °R = {(value - 491.67) * 5.0 / 9.0} °C", true);
						break;
					case 'f':
						CPH.SendMessage($"{value} °R = {value - 459.67} °F", true);
						break;
					case 'k':
						CPH.SendMessage($"{value} °R = {value * 5.0 / 9.0} K", true);
						break;
					case 'r':
						CPH.SendMessage($"{value} °R = {value} °R", true);
						break;
					case 'é':
					case 'e':
						CPH.SendMessage($"{value} °R = {(value - 491.67) * 4.0 / 9.0} °Ré", true);
						break;
					default:
						CPH.SendMessage("Please specify a temperature unit to convert to", true);
						break;
				}
				break;
			case 'é':
			case 'e':
				switch (target) {
					case 'c':
						CPH.SendMessage($"{value} °Ré = {value * 5.0 / 4.0} °C", true);
						break;
					case 'f':
						CPH.SendMessage($"{value} °Ré = {value * 9.0 / 4.0 + 32.0} °F", true);
						break;
					case 'k':
						CPH.SendMessage($"{value} °Ré = {value * 5.0 / 4.0 + 273.15} K", true);
						break;
					case 'r':
						CPH.SendMessage($"{value} °Ré = {value * 9.0 / 4.0 + 491.67} °R", true);
						break;
					case 'é':
					case 'e':
						CPH.SendMessage($"{value} °Ré = {value} °Ré", true);
						break;
					default:
						CPH.SendMessage("Please specify a temperature unit to convert to", true);
						break;
				}
				break;
			default:
				CPH.SendMessage("Please specify a temperature unit to convert from", true);
				break;
		}
		return true;
	}
}
