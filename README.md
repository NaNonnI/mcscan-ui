# MCScan UI
Tool to discover minecraft serves on the internet all over the world and visualize them on a website.
Backend edited by Nonomi. based on SieBRUM minecraft-server-discovery

## Nonomi.
### Written in C# - Net Framework 4.8
In dev:
- Easy way to connect to MYSQL database
- Number of hit
- API and MassScan Helper directly in one EXE
- Download requirement (masscan & NPM)
- Easy website deployment (and port error fix)
- Import IP list

## SieBRUM

Website example: 
![image](https://user-images.githubusercontent.com/14212955/172442508-b72047e5-18b6-4932-9b73-94073c8d0cb7.png)

All data fields saved in database:
- IP address
- Port
- Messsage of the day (motd) (message visible when viewing server in server browser)
- Minecraft server version (including modded)
- Current amount of players
- Max amount of players
- Latency (from API location to minecraft server)
- First discovered date
- Last seen online date
- Sample of players (currently not working)
- Continent of server
- Country of server
- Region name of server
- Zipcode of server
- Latitude of server
- Longitude of server
- Isp of server
- Timezone of server

## Frontend
The frontend is written in Angular and is very basic with basic functionality. It has the option to refresh the data of a specific server and open the geo-location of the server on Google Maps. You are able to click on the 'players' header to sort based on active players.
To run the front-end, just install NPM, install packages (npm install) and serve the website (ng serve)


## Credits
Nonomi.

https://github.com/SieBRUM/minecraft-server-discovery

https://github.com/robertdavidgraham/masscan

https://github.com/FragLand/minestat
