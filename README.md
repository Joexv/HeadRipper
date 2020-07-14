# HeadRipper
Headspace Windows Downloader and Player

# Why
I'm not an a Headspace user, but I was dissapointed that you could not use a PC or even a web browser to listen to a majority of their library. 
This program will allow me to bring access to those who want to try Headspace but either don't have a mobile device or don't have access to their device during the night.

# Features
Currently supports all 'Night' audio. Regular meditation and relaxation audios to be supported in the future. Sleepcasts, voice and background are mixed using FFMPEG into a single MP3.
All other media is downloaded as AAC files.
Audio is pulled using Headspace official servers using an official Headspace paid account.
You CANNOT use this program for piracy as it requires a working current Bearer ID to authenticate with the server.
This Bearer ID is unique to each account, and can easily be pulled by signing into Headspace on your web browser 
and using web dev tools to view any JSON request sent to the server.
Without this authentication you cannot use the program period. It just won't work.

Currently, the program is contained in a single form, with a very basic audio player built in for ease of testing and checking media you download.

Headspace appears to change what voice/audio file Sleepcasts use on a daily basis. You can identify which audio you will get based on the Session ID.
Some Wind Downs have multiple variations to their audio based on length of time, and narrator. These can be selected via a drop down box. Currently they are labeled as just a numerical ID, in the future these will idealy be labeled properly with narrator information and length of the audio.

# Disclaimer
By making this program I am not promoting piracy or stealing of content from Headspace. 
They truely believe in what they create and that should be supported for making a service that helps
many many people every day.

# Bearer ID
In order to get your BearerID for authenticating with the headspace server follow the below instructions:

1: In a web browser that has developer tools (such as FireFox or Google Chrome), navigate to https://headspace.com/ and sign in with a valid account

2: Open up the network traffic view (In FireFox this is CRTL-Shift-E)

3: Find any GET method that has the url api.prod.headspace.com

4: Under the Request Headers table on the right hand side of the network sniffer find 'Authentication' and next to it you should see bearer and then a very long line of numbers and letters. Those numbers and letters are your BearerID. Simply copy and paste that into the corisponding box in HeadRipper and you're set!
