# mkvSubFixer

## Why create this program:
The reason for this project is to prevent "Plex Media Server" from transcoding .mkv video files when .ssa or .ass subtitles are enabled.

The Plex transcoder uses lots of unnecessary CPU power for just rendering subtitles. This program will keep energy consumption down and prevent pc fans from acting like a jet engine.

## The goal of this project:
create a program that extracts the .ssa .ass subs from the .mkv video files.

convert the subs to .srt

save the .srt at the same location as the .mkv file.

## Dependencies:
The program uses the famous FFmpeg converter to achieve the extraction and conversion process.

It depends on the ffmpeg.exe file.
