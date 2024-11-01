# Streamer.Bot-Actions - Minimod

1. Download and Import [the Action](Minimod.sb) - You can optionally view the code behind the actions before importing [here](Minimod.cs)
2. Make sure both the Trigger and Action are enabled.

Below are the possible Sub-Commands for that Command (`<required parameter>`, `[<optional parameter>]`):
* `!mod subscribers` - Enable Subscribers-Only-Mode
* `!mod subscribersoff` - Disable Subscribers-Only-Mode
* `!mod emote` - Enable Emote-Only-Mode (Mods and VIPs are exempt by Twitch)
* `!mod emoteoff` - Disable Emote-Only-Mode
* `!mod slow <seconds>` - Enable Slow-Mode
* `!mod slowoff` - Disable Slow-Mode
* `!mod follower` - Enable Followers-Only-Mode
* `!mod followers` - Enable Followers-Only-Mode
* `!mod followeroff` - Disable Followers-Only-Mode
* `!mod followersoff` - Disable Followers-Only-Mode
* `!mod whisper <user> <text>` - Whisper User from Broadcaster- or Bot-Account, depending on the configuration
* `!mod w <user> <text>` - Whisper User from Broadcaster- or Bot-Account, depending on the configuration
* `!mod mod <user>` - Make a User a Mod
* `!mod unmod <user>` - Remove a User from being a Mod
* `!mod vip <user>` - Make a User a VIP
* `!mod unvip <user>` - Remove a User from being a VIP
* `!mod message <text>` - Send a message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod msg <text>` - Send a message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod /me <text>` - Send a /me message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod slashme <text>` - Send a /me message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod clear` - Clears chat
* `!mod delete <message-ID>` - Delete a single message in chat. Requires Third-Party-Software or Programming Skills to find the message id.
* `!mod blockterm <term>` - Adds an AutoMod blocked term
* `!mod approve <messageId>` - Approves an AutoMod held message
* `!mod settags <tags>` - Sets the Channel Tags
* `!mod addtag <tag>` - Adds a Channel Tag
* `!mod removetag <tag>` - Removes a Channel Tag
* `!mod cleartags` - Removes all Channel Tags
* `!mod soid <user-ID>` - Shoutout a User by User-ID
* `!mod shoutoutid <user-ID>` - Shoutout a User by User-ID
* `!mod so <username>` - Shoutout a User by Username
* `!mod shoutout <username>` - Shoutout a User by Username
* `!mod ban <username> [<reason>]` - Ban a User
* `!mod timeout <username> <seconds> [<reason>]` - Timeout a User for the given duration
* `!mod unban <username>` - Unbans/Untimeouts a User
* `!mod rewardtitles` - Sends the titles of all available rewards to chat
* `!mod rewardid <title>` - Searches for a rewardId by it's title and respond with the first found reward
* `!mod disablereward <rewardId>` - Disables a reward by its ID
* `!mod enablereward <rewardId>` - Enables a reward by its ID
* `!mod pausereward <rewardId>` - Pauses a reward by its ID
* `!mod unpausereward <rewardId>` - Unpauses a reward by its ID
* `!mod disablerewardgroup <groupName>` - Disables a reward group by its name
* `!mod enablerewardgroup <groupName>` - Enables a reward group by its name
* `!mod pauserewardgroup <groupName>` - Pauses a reward group by its name
* `!mod unpauserewardgroup <groupName>` - Unpauses a reward group by its name
* `!mod rewardtitle <rewardId> <title>` - Updates the title of a reward
* `!mod rewardprompt <rewardId> <prompt>` - Updates the prompt of a reward
* `!mod rewardcost <rewardId> <cost>` - Updates the cost of a reward
* `!mod rewardcooldown <rewardId> <cooldown>` - Updates the cooldown of a reward
* `!mod fulfillredemption <rewardId> <redemptionId>` - Fulfills a redemption, you'll need to get the redemption ID from somewhere else
* `!mod cancelredemption <rewardId> <redemptionId>` - Cancels a redemption, you'll need to get the redemption ID from somewhere else
* `!mod poll <title>|<options separated by semicolon>|<duration in seconds>|<channel points per vote>` - Create a poll
* `!mod terminatepoll <pollId>` - Terminate a poll
* `!mod archivepoll <pollId>` - Archive a poll
* `!mod prediction <title>|<options separated by semicolon>|<prediction window in seconds>` - Create a prediction
* `!mod cancelprediction <predictionId>` - Create a prediction
* `!mod lockprediction <predictionId>` - Create a prediction
* `!mod resolveprediction <predictionId> <winningId>` - Create a prediction
* `!mod clip` - Create a clip and send it's link to chat
* `!mod marker` - Creates a stream marker and confirm it's creation in chat
* `!mod commercial <duration>` - Run a commercial. Possible values for `duration` are 30, 60, 90, 120, 150, 180
* `!mod title <title>` - Change the current title
* `!mod game <game>` - Change the current category
* `!mod gameid <gameId>` - Change the current category by id
* `!mod raidid <user-ID>` - Raid Channel by its User-ID
* `!mod raid <username>` - Raid Channel by its Username
* `!mod cancelraid` - Cancels a raid that is currently counting down
* `!mod announce <message>` - Sends an announcement to chat in the default color
* `!mod announceblue <message>` - Sends an announcement to chat in blue
* `!mod announceorange <message>` - Sends an announcement to chat in orange
* `!mod announcegreen <message>` - Sends an announcement to chat in green
* `!mod announcepurple <message>` - Sends an announcement to chat in purple
* `!mod run <actionName>` - Run a Streamer.Bot Action by its name
* `!mod runid <actionId>` - Run a Streamer.Bot Action by its id
* `!mod cph <method> <parameters separated by a pipe>` - Run any kind of method that is member of the CPH object (if enabled)

TODO: Make it possible to disable specific Sub-Commands
