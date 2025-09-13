using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class CPHInline
{
	public bool Execute()
	{
		string tokenVarName = CPH.TryGetArg("tokenVarName", out tokenVarName) ? tokenVarName : null;
		string clientId = CPH.TryGetArg("twitchClientId", out clientId) ? clientId : null;
		string clientSecret = CPH.TryGetArg("twitchClientSecret", out clientSecret) ? clientSecret : null;
		string broadcasterId = CPH.TryGetArg("broadcastUserId", out broadcasterId) ? broadcasterId : null;
		string senderId = CPH.TryGetArg("senderId", out senderId) ? senderId : null;
		string message = CPH.TryGetArg("message", out message) ? message : null;
		bool forSourceOnly = CPH.TryGetArg("forSourceOnly", out forSourceOnly) ? forSourceOnly : false;
		using HttpClient client = new();
		SendMessage(client, tokenVarName, clientId, clientSecret, broadcasterId, senderId, message, forSourceOnly).Wait();
		return true;
	}

	public async Task SendMessage(HttpClient client, string tokenVarName, string clientId, string clientSecret, string broadcasterId, string senderId, string message, bool forSourceOnly)
	{
		Token token = await GetToken(client, tokenVarName, clientId, clientSecret);
		if (token is null) return;
		SetToken(token, tokenVarName);
		SendMessagePayload payload = new()
		{
			BroadcasterId = broadcasterId,
			SenderId = senderId,
			Message = message,
			ForSourceOnly = forSourceOnly
		};
		using HttpRequestMessage request = new(HttpMethod.Post, "https://api.twitch.tv/helix/chat/messages");
		request.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
		request.Headers.Add("Client-ID", clientId);
		string jsonPayload = JsonConvert.SerializeObject(payload);
		request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
		using HttpResponseMessage response = await client.SendAsync(request);
		var responseJson = await response.Content.ReadAsStringAsync();
		if (response is { StatusCode: HttpStatusCode.OK})
		{
			CPH.LogDebug($"SendMesssage (app access) response: {responseJson}");
		}
		else
		{
			CPH.LogWarn($"SendMesssage (app access) response: {responseJson}; status code: {response.StatusCode}");
		}
	}

	private class SendMessagePayload
	{
		[JsonProperty(PropertyName = "broadcaster_id")]
		public string BroadcasterId { get; set; }
		[JsonProperty(PropertyName = "sender_id")]
		public string SenderId { get; set; }
		[JsonProperty(PropertyName = "message")]
		public string Message { get; set; }
		[JsonProperty(PropertyName = "for_source_only")]
		public bool ForSourceOnly { get; set; }
	}

	private async Task<Token> GetToken(HttpClient client, string tokenVarName, string clientId, string clientSecret)
	{
		string token = CPH.GetGlobalVar<string>(tokenVarName, false);
		if (token is not null)
		{
			var tokenObj = JsonConvert.DeserializeObject<Token>(token);
			if (tokenObj.ExpiresAt > DateTimeOffset.UtcNow)
			{
				return tokenObj;
			}
		}
		using HttpResponseMessage response = await client.PostAsync($"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type=client_credentials", null);
		string jsonResponse = await response.Content.ReadAsStringAsync();
		if (response.StatusCode == HttpStatusCode.OK)
		{
			Token tokenObj = JsonConvert.DeserializeObject<Token>(jsonResponse);
			return tokenObj;
		}
		else
		{
			CPH.LogWarn($"Error getting app access token from twitch: {jsonResponse}");
			return null;
		}
	}

	private void SetToken(Token token, string tokenVarName)
	{
		CPH.SetArgument(tokenVarName, JsonConvert.SerializeObject(token));
	}

	private class Token
	{
		[JsonProperty(PropertyName = "access_token")]
		public string AccessToken { get; set; }
		[JsonProperty(PropertyName = "expires_in")]
		public long ExpiresIn { get; set; }
		private readonly DateTimeOffset _issuedAt = DateTimeOffset.UtcNow;
		public DateTimeOffset ExpiresAt => _issuedAt.AddSeconds(ExpiresIn);
		[JsonProperty(PropertyName = "token_type")]
		public string TokenType { get; set; }
	}
}
