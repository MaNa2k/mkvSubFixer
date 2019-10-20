# mkvSubFixer


## Why create this program:
The reason for this project is to prevent "Plex Media Server" from transcoding .mkv video files when .ssa or .ass subtitles are enabled.

The Plex transcoder uses lots of unnecessary CPU power for just rendering subtitles. This program will fix the issue. The energy consumption will go down and prevent pc fans from acting like a jet engine.

## The goal of this project:
create a program that extracts the .ssa .ass subs from the .mkv video files.

convert the subs to .srt

save the .srt at the same location as the .mkv file.

## Dependencies:
The program uses the famous FFmpeg converter to achieve the extraction and conversion process.

I have included the ffmpeg.exe with the other binaries. You can get the latest version from here https://www.ffmpeg.org/



## Feuture plans
- Automatically embedding .srt inside .mkv

- multithreded conversion prosessing.

- Better GUI style.

- Better readme, with screenshots.
