#My Test Project
See this site in action @ myproject.savras.net

Here's my optimistic project to explore Angular2, LESS, Gulp, Yeoman, .NET Core, Azure Table Store/ MongoDB, & Geospatial mapping using D2.
*   Sample pages using ASP.NET MVC 6
*   [Gulp](http://go.microsoft.com/fwlink/?LinkId=518007) and [Bower](http://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries

## Cmd sheet
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "&{$Branch='dev';iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.ps1'))}"
where dnvm
dnvm upgrade
dnvm list
dnvm use 1.0.0-update1 –p
npm install -g yo grunt-cli generator-aspnet bower
yo aspnet
ctrl + shift + p -> dnu restore
dnx: Run Command
dnx ef migrations add InitialDatabase
bower install
https://docs.npmjs.com/misc/semver