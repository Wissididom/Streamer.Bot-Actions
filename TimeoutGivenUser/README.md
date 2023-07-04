# Streamer.Bot-Actions - TimeoutGivenUser

1. Download and Import [the Action](TimeoutGivenUser.sb)
2. Connect the Action to any kind of Trigger (Command, Channel Point Reward, etc.) - **It needs to have the rawInput Variable set, else it'll just send back `%rawInput%`.**
3. Run the Trigger
4. Streamer.Bot should now Timeout the User given in the `%rawInput%` variable for the duration that is given in the `%timeoutDuration%` argument in seconds
