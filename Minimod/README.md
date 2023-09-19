# Streamer.Bot-Actions - Minimod

1. Download and Import [the Action](Minimod.sb)
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
* `!mod mod <user>` - Make a User a Mod
* `!mod unmod <user>` - Remove a User from being a Mod
* `!mod vip <user>` - Make a User a VIP
* `!mod unvip <user>` - Remove a User from being a VIP
* `!mod message <text>` - Send a message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod slashme <text>` - Send a /me message in Twitch Chat from the Broadcaster- or Bot-Account, depending on the configuration
* `!mod clear` - Clears chat
* `!mod delete <message-ID>` - Delete a single message in chat. Requires Third-Party-Software or Programming Skills to find the message id.
* `!mod soid <user-ID>` - Shoutout a User by User-ID
* `!mod shoutoutid <user-ID>` - Shoutout a User by User-ID
* `!mod so <username>` - Shoutout a User by Username
* `!mod shoutout <username>` - Shoutout a User by Username
* `!mod ban <username> [<reason>]` - Ban a User
* `!mod timeout <username> <seconds> [<reason>]` - Timeout a User for the given duration
* `!mod unban <username>` - Unbans/Untimeouts a User
* `!mod poll <title>|<options separated by semicolon>|<duration in seconds>|<channel points per vote>` - Create a poll
* `!mod prediction <title>|<options separated by semicolon>|<prediction window in seconds>` - Create a prediction
* `!mod commercial <duration>` - Run a commercial. Possible values for `duration` are 30, 60, 90, 120, 150, 180
* `!mod title <title>` - Change the current title
* `!mod game <game>` - Change the current category
* `!mod raidid <user-ID>` - Raid Channel by its User-ID
* `!mod raid <username>` - Raid Channel by its Username
* `!mod cancelraid` - Cancels a raid that is currently counting down

TODO: Make it possible to disable specific Sub-Commands
