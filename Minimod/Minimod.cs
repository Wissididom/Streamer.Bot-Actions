using System;
using System.Collections.Generic;
using System.Reflection; // For cph action

public class CPHInline
{
	public bool Execute()
	{
		bool BOT = CPH.TryGetArg("useBot", out BOT) ? BOT : false;
		bool allowCphReflection = CPH.TryGetArg("allowCphReflection", out allowCphReflection) ? allowCphReflection : false;
		string command = CPH.TryGetArg("command", out command) ? command : "";
		string input = CPH.TryGetArg("rawInput", out input) ? input : "";
		string[] splitted = input.Split(' ');
		string action = splitted[0].Trim();
		bool skipSuccessOutput = CPH.TryGetArg("skipSuccessOutput", out skipSuccessOutput) ? skipSuccessOutput : false;
		bool skipErrorOutput = CPH.TryGetArg("skipErrorOutput", out skipErrorOutput) ? skipErrorOutput : false;
		bool runActionsImmediatly = CPH.TryGetArg("runActionsImmediatly", out runActionsImmediatly) ? runActionsImmediatly : true;
		switch (action) {
			case "subscribers": {
				CPH.TwitchSubscriberOnly(true);
				break;
			} case "subscribersoff": {
				CPH.TwitchSubscriberOnly(false);
				break;
			} case "emote": {
				CPH.TwitchEmoteOnly(true);
				break;
			} case "emoteoff": {
				CPH.TwitchEmoteOnly(false);
				break;
			} case "slow": {
				if (splitted.Length > 1)
					CPH.TwitchSlowMode(true, int.Parse(splitted[1]));
				else
					CPH.TwitchSlowMode(true);
				break;
			} case "slowoff": {
				CPH.TwitchSlowMode(false);
				break;
			} case "follower": {
				goto case "followers";
			} case "followers": {
				if (splitted.Length > 1)
					CPH.TwitchFollowMode(true, int.Parse(splitted[1]));
				else
					CPH.TwitchFollowMode(true);
				break;
			} case "followeroff": {
				goto case "followersoff";
			} case "followersoff": {
				CPH.TwitchFollowMode(false);
				break;
			} case "w": {
				goto case "whisper";
			} case "whisper": {
				if (splitted.Length > 2) {
					string message = "";
					for (int i = 2; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					var success = CPH.SendWhisper(splitted[1], message.Trim(), BOT);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully sent whisper", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to send whisper", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} whisper <userName> <message>", BOT);
				}
				break;
			} case "mod": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchAddModerator(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully added moderator", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to add moderator", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} mod <userName>", BOT);
				}
				break;
			} case "unmod": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchRemoveModerator(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully removed moderator", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to remove moderator", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} unmod <userName>", BOT);
				}
				break;
			} case "vip": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchAddVip(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully added vip", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to add vip", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} vip <userName>", BOT);
				}
				break;
			} case "unvip": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchRemoveVip(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully removed vip", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to remove vip", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} unvip <userName>", BOT);
				}
				break;
			} case "msg": {
				goto case "message";
			} case "message": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.SendMessage(message.Trim(), BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} message <message>", BOT);
				}
				break;
			} case "reply": {
				if (splitted.Length > 2) {
					string message = "";
					for (int i = 2; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchReplyToMessage(message.Trim(), splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} reply <messageId> <message>", BOT);
				}
				break;
			} case "/me": {
				goto case "slashme";
			} case "slashme": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.SendAction(message.Trim(), BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} slashme <message>", BOT);
				}
				break;
			} case "clear": {
				var success = CPH.TwitchClearChatMessages(BOT);
				if (!success) {
					if (!skipErrorOutput) CPH.SendMessage("Failed to clear chat", BOT);
				}
				break;
			} case "delete": {
				if (splitted.Length > 1) {
					var countSuccess = 0;
					var countFailed = 0;
					for (int i = 1; i < splitted.Length; i++) {
						if (CPH.TwitchDeleteChatMessage(splitted[i], BOT)) {
							countSuccess++;
						} else {
							countFailed++;
						}
					}
					if (countSuccess > 0) {
						if (countFailed > 0) {
							if (!skipSuccessOutput) CPH.SendMessage($"Successfully deleted {countSuccess} messages ({countFailed} failed)", BOT);
						} else {
							if (!skipSuccessOutput) CPH.SendMessage($"Successfully deleted all {countSuccess} messages", BOT);
						}
					} else {
						if (countFailed > 0) {
							if (!skipErrorOutput) CPH.SendMessage($"Failed to delete {countFailed} messages", BOT);
						} else {
							// Cannot happen, because if nothing is specified the invalid usage is called and else the for above is ran at least once
							if (!skipErrorOutput) CPH.SendMessage($"Deleting messages neither succeeded nor failed!", BOT);
						}
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} delete <msgId>", BOT);
				}
				break;
			} case "blockterm": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					var success = CPH.TwitchAddBlockedTerm(message.Trim());
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully added blocked term", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to add blocked term", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} blockterm <term>", BOT);
				}
				break;
			} case "approve": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchApproveAutoHeldMessage(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully approved", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to approve", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} approve <messageId>", BOT);
				}
				break;
			} case "settags": {
				if (splitted.Length > 1) {
					List<string> tags = new List<string>();
					for (int i = 1; i < splitted.Length; i++) {
						tags.Add(splitted[i]);
					}
					var success = CPH.TwitchSetChannelTags(tags);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully set tags", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to set tags", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} settags <tags>", BOT);
				}
				break;
			} case "addtag": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchAddChannelTag(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully added tag", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to add tag", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} addtag <tag>", BOT);
				}
				break;
			} case "removetag": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchRemoveChannelTag(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully removed tag", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to remove tag", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} removetag <tag>", BOT);
				}
				break;
			} case "cleartags": {
				var success = CPH.TwitchRemoveChannelTag(splitted[1]);
				if (success) {
					if (!skipSuccessOutput) CPH.SendMessage("Successfully cleared tags", BOT);
				} else {
					if (!skipErrorOutput) CPH.SendMessage("Failed to clear tags", BOT);
				}
				break;
			} case "shoutoutid": {
				goto case "soid";
			} case "soid": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchSendShoutoutById(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully shouted out user", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to shoutout user", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} soid <userId> or {command} shoutoutid <userId>", BOT);
				}
				break;
			} case "shoutout": {
				goto case "so";
			} case "so": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchSendShoutoutByLogin(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully shouted out user", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to shoutout user", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} so <userName> or {command} shoutout <userName>", BOT);
				}
				break;
			} case "ban": {
				bool success;
				if (splitted.Length > 2) {
					string reason = "";
					for (int i = 2; i < splitted.Length; i++) {
						reason += splitted[i] + " ";
					}
					success = CPH.TwitchBanUser(splitted[1], reason.Trim(), BOT);
				} else if (splitted.Length > 1) {
					success = CPH.TwitchBanUser(splitted[1], null, BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} ban <userName> [<reason>]", BOT);
					success = false;
					skipErrorOutput = true;
				}
				if (success) {
					if (!skipSuccessOutput) CPH.SendMessage("Successfully banned user", BOT);
				} else {
					if (!skipErrorOutput) CPH.SendMessage("Failed to ban user", BOT);
				}
				break;
			} case "timeout": {
				bool success;
				if (splitted.Length > 3) {
					string reason = "";
					for (int i = 3; i < splitted.Length; i++) {
						reason += splitted[i] + " ";
					}
					success = CPH.TwitchTimeoutUser(splitted[1], int.Parse(splitted[2]), reason.Trim(), BOT);
				} else if (splitted.Length > 2) {
					success = CPH.TwitchTimeoutUser(splitted[1], int.Parse(splitted[2]), null, BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} timeout <userName> <duration> [<reason>]", BOT);
					success = false;
					skipErrorOutput = true;
				}
				if (success) {
					if (!skipSuccessOutput) CPH.SendMessage("Successfully timed-out user", BOT);
				} else {
					if (!skipErrorOutput) CPH.SendMessage("Failed to timeout user", BOT);
				}
				break;
			} case "unban": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchUnbanUser(splitted[1], BOT);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully unbanned user", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed unban user", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} unban <userName>", BOT);
				}
				break;
			} case "rewardtitles": {
				List<string> rewardTitles = new List<string>();
				foreach (var reward in CPH.TwitchGetRewards()) {
					rewardTitles.Add(reward.Title);
				}
				string rewardTitlesStr = string.Join(", ", rewardTitles);
				CPH.SendMessage($"Reward Titles: {rewardTitlesStr}", BOT);
				break;
			} case "rewardid": {
				if (splitted.Length > 1) {
					bool rewardFound = false;
					foreach (var reward in CPH.TwitchGetRewards()) {
						if (reward.Title.Trim().ToLower() == splitted[1].Trim().ToLower()) {
							rewardFound = true;
							CPH.SendMessage($"Found Reward: {reward.Id}", BOT);
							break;
						}
					}
					if (!rewardFound && !skipErrorOutput) CPH.SendMessage($"Couldn't find a reward with this title. Use {command} rewardtitles if necessary.", BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} rewardid <title>", BOT);
				}
				break;
			} case "disablereward": {
				if (splitted.Length > 1) {
					CPH.DisableReward(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to disable reward " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} disablereward <rewardId>", BOT);
				}
				break;
			} case "enablereward": {
				if (splitted.Length > 1) {
					CPH.EnableReward(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to enable reward " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} enablereward <rewardId>", BOT);
				}
				break;
			} case "pausereward": {
				if (splitted.Length > 1) {
					CPH.PauseReward(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to pause reward " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} pausereward <rewardId>", BOT);
				}
				break;
			} case "unpausereward": {
				if (splitted.Length > 1) {
					CPH.UnPauseReward(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to unpause reward " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} unpausereward <rewardId>", BOT);
				}
				break;
			} case "enablerewardgroup": {
				if (splitted.Length > 1) {
					CPH.TwitchRewardGroupEnable(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to enable reward group " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} enablerewardgroup <groupName>", BOT);
				}
				break;
			} case "disablerewardgroup": {
				if (splitted.Length > 1) {
					CPH.TwitchRewardGroupDisable(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to disable reward group " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} disablerewardgroup <groupName>", BOT);
				}
				break;
			} case "pauserewardgroup": {
				if (splitted.Length > 1) {
					CPH.TwitchRewardGroupPause(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to pause reward group " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} pauserewardgroup <groupName>", BOT);
				}
				break;
			} case "unpauserewardgroup": {
				if (splitted.Length > 1) {
					CPH.TwitchRewardGroupUnPause(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to unpause reward group " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} unpauserewardgroup <groupName>", BOT);
				}
				break;
			} case "rewardtitle": {
				if (splitted.Length > 2) {
					var title = "";
					for (int i = 2; i < splitted.Length; i++) {
						title += splitted[i] + " ";
					}
					var success = CPH.UpdateRewardTitle(splitted[1], title);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully updated reward title", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed update reward title", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} rewardtitle <rewardId> <title>", BOT);
				}
				break;
			} case "rewardprompt": {
				if (splitted.Length > 2) {
					var prompt = "";
					for (int i = 2; i < splitted.Length; i++) {
						prompt += splitted[i] + " ";
					}
					var success = CPH.UpdateRewardPrompt(splitted[1], prompt);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully updated reward prompt", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed update reward prompt", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} rewardprompt <rewardId> <prompt>", BOT);
				}
				break;
			} case "rewardcost": {
				if (splitted.Length > 2) {
					var cost = int.Parse(splitted[2]);
					CPH.UpdateRewardCost(splitted[1], cost, false);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to update the reward cost of " + splitted[1] + " to " + cost, BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} rewardcost <rewardId> <cost>", BOT);
				}
				break;
			} case "rewardcooldown": {
				if (splitted.Length > 2) {
					var cooldown = int.Parse(splitted[2]);
					CPH.UpdateRewardCost(splitted[1], cooldown, false);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to update the reward cooldown of " + splitted[1] + " to " + cooldown, BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} rewardcooldown <rewardId> <cooldown>", BOT);
				}
				break;
			} case "fulfillredemption": {
				if (splitted.Length > 2) {
					var success = CPH.TwitchRedemptionFulfill(splitted[1], splitted[2]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully fulfilled redemption", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to fulfill redemption", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} fulfillredemption <rewardId> <redemptionId>", BOT);
				}
				break;
			} case "cancelredemption": {
				if (splitted.Length > 2) {
					var success = CPH.TwitchRedemptionCancel(splitted[1], splitted[2]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully cancelled redemption", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to cancel redemption", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} cancelredemption <rewardId> <redemptionId>", BOT);
				}
				break;
			} case "poll": {
				if (splitted.Length > 1) {
					string poll = "";
					for (int i = 1; i < splitted.Length; i++) {
						poll += splitted[i] + " ";
					}
					string[] pollParts = poll.Split('|');
					string title = null;
					string choices = null;
					uint duration = 0;
					uint channelPointsPerVote = 0;
					if (pollParts.Length > 2) {
						title = pollParts[0];
						if (title.Length > 60) {
							CPH.SendMessage($"Invalid Usage, title may only contain up to 60 characters", BOT);
							break;
						}
						choices = pollParts[1];
						duration = uint.Parse(pollParts[2]);
						if (duration < 15 || duration > 1800) {
							CPH.SendMessage($"Invalid Usage, the duration must be between 15 and 1800 seconds. You've specified {duration} seconds", BOT);
							break;
						}
					}
					if (pollParts.Length > 3) {
						channelPointsPerVote = uint.Parse(pollParts[3]);
						if (channelPointsPerVote > 1000000) {
							CPH.SendMessage($"Invalid Usage, channel points per vote must be between 0 and 1000000, where 0 means it is turned off.", BOT);
							break;
						}
					}
					if (title == null || choices == null || duration < 1) {
						CPH.SendMessage($"Invalid Usage, Usage: {command} poll <title>|<options separated by semicolon>|<duration in seconds>[|<channel points per vote>]", BOT);
					} else {
						string[] choicesArr = choices.Split(';');
						for (int i = 0; i < choicesArr.Length; i++) {
							if (choicesArr[i].Length > 25) {
								CPH.SendMessage($"Invalid Usage, an option may only contain up to 25 characters. Your {i + 1}. option contains more than that", BOT);
								break;
							}
						}
						List<string> choicesList = new List<string>();
						for (int i = 0; i < choicesArr.Length; i++) {
							choicesList.Add(choicesArr[i].Trim());
						}
						if (CPH.TwitchPollCreate(title, choicesList, (int)duration, (int)channelPointsPerVote)) {
							if (!skipSuccessOutput) CPH.SendMessage("Successfully created Poll", BOT);
						} else {
							if (!skipErrorOutput) CPH.SendMessage("Failed creating Poll", BOT);
						}
					}
				}
				break;
			} case "terminatepoll": {
				if (splitted.Length > 1) {
					CPH.TwitchPollTerminate(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to terminate poll " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} terminatepoll <pollId>", BOT);
				}
				break;
			} case "archivepoll": {
				if (splitted.Length > 1) {
					CPH.TwitchPollArchive(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to archive poll " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} archivepoll <pollId>", BOT);
				}
				break;
			} case "prediction": {
				if (splitted.Length > 1) {
					string prediction = "";
					for (int i = 1; i < splitted.Length; i++) {
						prediction += splitted[i] + " ";
					}
					string[] predictionParts = prediction.Split('|');
					string title = null;
					string options = null;
					uint duration = 0;
					if (predictionParts.Length > 2) {
						title = predictionParts[0];
						if (title.Length > 45) {
							CPH.SendMessage($"Invalid Usage, the title can only have 45 characters", BOT);
							break;
						}
						options = predictionParts[1];
						duration = uint.Parse(predictionParts[2]);
						if (duration < 30 || duration > 1800) {
							CPH.SendMessage($"Invalid Usage, the duration (prediction-window) must be between 30 and 1800 seconds. You've specified {duration} seconds", BOT);
							break;
						}
					}
					if (title == null || options == null || duration < 1) {
						CPH.SendMessage("Invalid Usage, Usage: {command} prediction <title>|<options separated by semicolon>|<prediction window in seconds>", BOT);
					} else {
						string[] optionsArr = options.Split(';');
						if (optionsArr.Length < 2 || optionsArr.Length > 10) {
							CPH.SendMessage($"Invalid Usage, you must have at least 2 and at most 10 outcomes. You've specified {optionsArr.Length} outcomes.", BOT);
							break;
						}
						List<string> optionsList = new List<string>();
						for (int i = 0; i < optionsArr.Length; i++) {
							optionsList.Add(optionsArr[i].Trim());
						}
						string predictionCreateResponse = CPH.TwitchPredictionCreate(title, optionsList, (int)duration);
						// Do not check for the skipping variables because the response is a string where I don't think I can find out if it is successful
						CPH.SendMessage($"Created Prediction with this response (StreamerBot doesn't tell if it was successful): {predictionCreateResponse}", BOT);
					}
				}
				break;
			} case "cancelprediction": {
				if (splitted.Length > 1) {
					CPH.TwitchPredictionCancel(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to cancel prediction " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} cancelprediction <predictionId>", BOT);
				}
				break;
			} case "lockprediction": {
				if (splitted.Length > 1) {
					CPH.TwitchPredictionLock(splitted[1]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to lock prediction " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} lockprediction <predictionId>", BOT);
				}
				break;
			} case "resolveprediction": {
				if (splitted.Length > 2) {
					CPH.TwitchPredictionResolve(splitted[1], splitted[2]);
					if (!skipSuccessOutput) CPH.SendMessage("Told Twitch to cancel prediction " + splitted[1], BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} resolveprediction <predictionId> <winningId>", BOT);
				}
				break;
			} case "clip": {
				var clip = CPH.CreateClip();
				if (clip is null) { // Assuming it returns null on failure
					if (!skipErrorOutput) CPH.SendMessage("Failed to create clip!", BOT);
				} else {
					// Do not check for skipSuccessOutput because creating a clip without returning it's url isn't really helpful
					// most likely you want to get the link when running `{command} clip`
					// Due to Twitch restrictions it is always 30 seconds long and the name matches the stream title
					CPH.SendMessage(clip.Url, BOT);
				}
				break;
			} case "marker": {
				if (splitted.Length > 1) {
					var description = "";
					for (int i = 1; i < splitted.Length; i++) {
						description += splitted[i] + " ";
					}
					var marker = CPH.CreateStreamMarker(description);
					if (marker is null) { // Assuming it returns null on failure
						if (!skipErrorOutput) CPH.SendMessage("Failed to create stream marker!", BOT);
					} else {
						if (!skipSuccessOutput) CPH.SendMessage($"{marker.Id} stream marker created at {marker.Position} seconds!", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} marker <description>", BOT);
				}
				break;
			} case "commericial": {
				if (splitted.Length > 1) {
					uint duration = uint.Parse(splitted[1]);
					if (duration > 180) {
						CPH.SendMessage($"Invalid Usage, the duration can only be up to 180 seconds", BOT);
						break;
					}
					var success = CPH.TwitchRunCommercial((int)duration);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully ran commercial", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to run commercial", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} commercial <duration>", BOT);
				}
				break;
			} case "title": {
				if (splitted.Length > 1) {
					var title = "";
					for (int i = 1; i < splitted.Length; i++) {
						title += splitted[i] + " ";
					}
					var success = CPH.SetChannelTitle(title);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully changed title", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to change title", BOT);
					}
				} else
					CPH.SendMessage($"Invalid Usage, Usage: {command} title <title>", BOT);
				break;
			} case "game": {
				if (splitted.Length > 1) {
					var game = "";
					for (int i = 1; i < splitted.Length; i++) {
						game += splitted[i] + " ";
					}
					var gameObj = CPH.SetChannelGame(splitted[1]);
					if (gameObj is null) { // Assuming it returns null on failure
						if (!skipErrorOutput) CPH.SendMessage("Failed to update game", BOT);
					} else {
						if (!skipSuccessOutput) CPH.SendMessage($"Game updated to: {gameObj.Name}", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} game <gameName>", BOT);
				}
				break;
			} case "gameid": {
				if (splitted.Length > 1) {
					var success = CPH.SetChannelGameById(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully changed game", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to change game", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} gameid <gameId>", BOT);
				}
				break;
			} case "raidid": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchStartRaidById(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully started raid", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to start raid", BOT);
					}
				} else
					CPH.SendMessage($"Invalid Usage, Usage: {command} raidid <userId>", BOT);
				break;
			} case "raid": {
				if (splitted.Length > 1) {
					var success = CPH.TwitchStartRaidByName(splitted[1]);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage("Successfully started raid", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to start raid", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} raid <userName>", BOT);
				}
				break;
			} case "cancelraid": {
				var success = CPH.TwitchCancelRaid();
				if (success) {
					if (!skipSuccessOutput) CPH.SendMessage("Successfully cancelled raid", BOT);
				} else {
					if (!skipErrorOutput) CPH.SendMessage("Failed to cancel raid", BOT);
				}
				break;
			} case "announce": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchAnnounce(message.Trim(), BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} announce <message>", BOT);
				}
				break;
			} case "announceblue": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchAnnounce(message.Trim(), BOT, "blue");
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} announceblue <message>", BOT);
				}
				break;
			} case "announceorange": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchAnnounce(message.Trim(), BOT, "orange");
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} announceorange <message>", BOT);
				}
				break;
			} case "announcegreen": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchAnnounce(message.Trim(), BOT, "green");
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} announcegreen <message>", BOT);
				}
				break;
			} case "announcepurple": {
				if (splitted.Length > 1) {
					string message = "";
					for (int i = 1; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					CPH.TwitchAnnounce(message.Trim(), BOT, "purple");
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} announcepurple <message>", BOT);
				}
				break;
			} case "run": {
				if (splitted.Length > 1) {
					string actionName = splitted[1].Trim();
					var success = CPH.RunAction(actionName, runActionsImmediatly);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage($"Successfully ran action {actionName}", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to run action {actionName}", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} run <actionName>", BOT);
				}
				break;
			} case "runid": {
				if (splitted.Length > 1) {
					string actionId = splitted[1].Trim();
					var success = CPH.RunActionById(actionId, runActionsImmediatly);
					if (success) {
						if (!skipSuccessOutput) CPH.SendMessage($"Successfully ran action {actionId}", BOT);
					} else {
						if (!skipErrorOutput) CPH.SendMessage("Failed to run action {actionId}", BOT);
					}
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} runid <actionId>", BOT);
				}
				break;
			} case "cph": {
				if (!allowCphReflection) {
					// Let's always give that error if CPH reflection is not allowed
					CPH.SendMessage("CPH Reflection not allowed!", BOT);
					break;
				}
				if (splitted.Length > 1) {
					string methodName = splitted[1];
					string message = "";
					for (int i = 2; i < splitted.Length; i++) {
						message += splitted[i] + " ";
					}
					string[] parameters = message.Split('|');
					if (parameters.Length < 1 || parameters[0].Trim() == string.Empty)
						CPH.SendMessage(ExecuteWithReflection(methodName, null).ToString(), BOT);
					else
						CPH.SendMessage(ExecuteWithReflection(methodName, parameters).ToString(), BOT);
				} else {
					CPH.SendMessage($"Invalid Usage, Usage: {command} cph <method> <parameters separated by a pipe>", BOT);
				}
				break;
			}
		}
		return true;
	}
	
	private object ExecuteWithReflection(string methodName, string[] parameters = null) {
		Type cphType = CPH.GetType();
		MethodInfo cphMethod = cphType.GetMethod(methodName);
		if (parameters == null) {
			return cphMethod.Invoke(CPH, null);
		}
		ParameterInfo[] methodParameters = cphMethod.GetParameters();
		object[] localParameters = new object[parameters.Length];
		for (int i = 0; i < methodParameters.Length; i++) {
			if (methodParameters[i].ParameterType == typeof(int)) {
				localParameters[i] = int.Parse(parameters[i]);
			}
			if (methodParameters[i].ParameterType == typeof(double)) {
				localParameters[i] = double.Parse(parameters[i]);
			}
			if (methodParameters[i].ParameterType == typeof(float)) {
				localParameters[i] = float.Parse(parameters[i]);
			}
			
			if (methodParameters[i].ParameterType == typeof(bool)) {
				localParameters[i] = bool.Parse(parameters[i]);
			}
		}
		return cphMethod.Invoke(CPH, localParameters);
	}
}
