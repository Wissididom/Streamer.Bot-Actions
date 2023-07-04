# Streamer.Bot-Actions - OfflineChannelLock

## OBS

1. Download and Import [the Actions](OfflineChannelLock.sb)
2. Connect the `OBS Stream` Action to the StreamStateChanged Event of a OBS Connection
3. Streamer.Bot should now automatically disable Emote-Only-Mode, Follower-Only-Mode, Slow-Mode and Subscriber-Only-Mode when you start the Stream in OBS and enable Emote-Only-Mode, 43200 minutes Follower-Only-Mode, 30 seconds Slow-Mode and Subscriber-Only-Mode when you stop the Stream in OBS

## Streamer.Bot Event

1. Download and Import [the Actions](OfflineChannelLock.sb) (You do not have to Import OBS Stream for this approach)
2. Connect the `Disable Offline Channel Lock` Action to the `Platforms->Twitch->Events->Channel->Stream Online` Event
3. Connect the `Enable Offline Channel Lock` Action to the `Platforms->Twitch->Events->Channel->Stream Offline` Event
4. Streamer.Bot should now automatically disable Emote-Only-Mode, Follower-Only-Mode, Slow-Mode and Subscriber-Only-Mode when you start the Stream on Twitch and enable Emote-Only-Mode, 43200 minutes Follower-Only-Mode, 30 seconds Slow-Mode and Subscriber-Only-Mode when you stop the Stream on Twitch
