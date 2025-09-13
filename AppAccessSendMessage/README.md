# Streamer.Bot-Actions - App Access Send Message

1. Download and Import [the Action](AppAccessSendMessage.sb)
2. Fill in the arguments (You can create the application [here](https://dev.twitch.tv/console)).
3. Make sure the bot is allowed to send messages by making sure the bot is either moderator or the broadcaster has authorized the application with the `channel:bot` scope. Additionally you need to grant the bot user the scopes `user:write:chat` and `user:bot`. You can use for example [my tool](https://wissididom.github.io/Twitch-Device-Code-Flow-Generator/) (it doesn't store any of your information, for multiple scopes you can just use spaces).
4. The action is intended to be run with the `Run Action` Sub-Action from another Action. You have to at least specify the %message% argument before using the `Run Action` Sub-Action.
5. when the action actually gets run it should post to chat and if it doesn't you should check your log file.
