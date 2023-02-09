svn update
cd /d %~dp0\DataSharing
dotnet build
dotnet publish  -c debug -r win-x64 -o D:\LocalPublishDataSharing  --self-contained false
cd /d D:\LocalPublishDataSharing
dotnet DataSharing.dll urls=http://*:8098
pause