
## PROJECT IS NOT BEING MAINTAINED. Still works as of April 2018 as long as the Spotify API & Newtonsoft.Json packages are updated

# Spotify Lyrics Grabber

Spotify used to provide lyrics (through MusixMatch). One day they stopped doing that. As an alternative, this WinForms application will see the currently playing song on your Spotify (non-web) client. It will try to find the right lyrics for that and show that on the screen.

To find the lyrics, it uses the lyrics on lyrics.wikia.com. As they don't provide a proper REST api for whatever reason (the lyrics in their api are incomplete), this program just copies the HTML source code of lyric pages.


