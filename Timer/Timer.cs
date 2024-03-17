using System;
using System.Text.RegularExpressions;

public class CPHInline
{
	private string FormatTime(TimeSpan timeSpan) {
		int hours = timeSpan.Hours;
		int minutes = timeSpan.Minutes;
		int seconds = timeSpan.Seconds;
		string hoursString = hours == 1 ? "hour" : "hours";
		string minutesString = minutes == 1 ? "minute" : "minutes";
		string secondsString = seconds == 1 ? "second" : "seconds";
		string result = $"{hours} {hoursString} {minutes} {minutesString} {seconds} {secondsString}";
		result = Regex.Replace(result, $@"\b0 {hoursString}", "");
		result = Regex.Replace(result, $@"\b0 {minutesString}", "");
		result = Regex.Replace(result, $@"\b0 {secondsString}", "");
		result = result.Replace("  ", " ").Trim();
		return result;
	}
	
	public string GetHumanReadableTime(uint seconds) {
		if (seconds > TimeSpan.MaxValue.TotalSeconds)
			throw new ArgumentOutOfRangeException("seconds is more than TimeSpan.MaxValue");
		string res = FormatTime(TimeSpan.FromSeconds(seconds));
		return res;
	}
	
	public bool Execute()
	{
		string rawInput = args["rawInput"].ToString();
		uint countdownInSeconds = 0U;
		string temp = string.Empty;
		foreach (char c in rawInput.ToCharArray()) {
			if (Char.IsDigit(c)) {
				temp += c;
			} else {
				if (temp == string.Empty) continue; // Ignore empty numbers
				switch (c) {
					case 'h':
					case 'H':
						countdownInSeconds += uint.Parse(temp) * 60 * 60;
						temp = string.Empty;
						break;
					case 'm':
					case 'M':
						countdownInSeconds += uint.Parse(temp) * 60;
						temp = string.Empty;
						break;
					case 's':
					case 'S':
						countdownInSeconds += uint.Parse(temp);
						temp = string.Empty;
						break;
				}
			}
		}
		if (countdownInSeconds > 0) {
			CPH.SendMessage($"Timer for {GetHumanReadableTime(countdownInSeconds)} started!", true);
			System.Threading.Thread.Sleep(1000);
			for (uint i = countdownInSeconds - 1; i >= 0; i--) {
				switch (i) {
					case 1000 * 3600: // 1000 hours
					case 900 * 3600: // 900 hours
					case 800 * 3600: // 800 hours
					case 700 * 3600: // 700 hours
					case 600 * 3600: // 600 hours
					case 500 * 3600: // 500 hours
					case 400 * 3600: // 400 hours
					case 300 * 3600: // 300 hours
					case 200 * 3600: // 200 hours
					case 100 * 3600: // 100 hours
					case 50 * 3600: // 50 hours
					case 25 * 3600: // 25 hours
					case 10 * 3600: // 10 hours
					case 5 * 3600: // 5 hours
					case 3600: // 1 hour
					case 1800: // 30 minutes
					case 900: // 15 minutes
					case 300: // 5 minutes
					case 60: // 1 minute
					case 3: // 3 seconds
					case 2: // 2 seconds
					case 1: // 1 second
						CPH.SendMessage($"{GetHumanReadableTime(i)} left!", true);
						break;
					case 0:
						CPH.SendMessage("TIMER IS UP!", true);
						break;
				}
				CPH.WebsocketBroadcastJson("{\"time\":" + i + "}");
				System.Threading.Thread.Sleep(1000);
			}
		}
		return true;
	}
}
