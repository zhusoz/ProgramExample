svn update
cd /d %~dp0\DataSharing
dotnet build
dotnet publish  -c release -r linux-x64 -o D:\PublishDataSharing  --self-contained false
scp -r  D:\PublishDataSharing\*  root@81.68.132.58:/usr/local/nginx/html/dsjzxapi
pause